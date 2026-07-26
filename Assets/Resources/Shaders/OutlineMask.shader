Shader "BWW/OutlineMask"
{
    Properties
    {
        _DepthTolerance(
            "Depth Tolerance",
            Range(0.0001, 0.1)
        ) = 0.01
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Opaque"
            "Queue" = "Geometry"
        }

        Pass
        {
            Name "OutlineMask"

            Cull Back

            // Le masque n'écrit pas dans la profondeur.
            ZWrite Off

            // On fait nous-mêmes la comparaison dans le fragment shader.
            ZTest Always

            HLSLPROGRAM

            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float _DepthTolerance;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;

                // Profondeur du ResourceItem en espace vue.
                float viewDepth : TEXCOORD0;
            };

            Varyings Vert(Attributes input)
            {
                Varyings output;

                float3 positionWS =
                    TransformObjectToWorld(input.positionOS.xyz);

                float3 positionVS =
                    TransformWorldToView(positionWS);

                output.positionHCS =
                    TransformWorldToHClip(positionWS);

                // Devant la caméra, Z est négatif dans l'espace vue.
                // On transforme donc la distance en valeur positive.
                output.viewDepth = -positionVS.z;

                return output;
            }

            half4 Frag(Varyings input) : SV_Target
            {
                // SV_POSITION est exprimée en pixels dans le fragment shader.
                float2 screenUV =
                    input.positionHCS.xy / _ScaledScreenParams.xy;

                // Profondeur de la scène à cet endroit de l'écran.
                float sceneRawDepth =
                    SampleSceneDepth(screenUV);

                float sceneDepth =
                    LinearEyeDepth(
                        sceneRawDepth,
                        _ZBufferParams
                    );

                /*
                 * Si le ResourceItem est plus loin que ce qui est déjà
                 * visible à cet endroit, cela signifie qu'un autre objet
                 * le cache.
                 */
                if (input.viewDepth >
                    sceneDepth + _DepthTolerance)
                {
                    discard;
                }

                // Pixel visible du ResourceItem :
                // il participe au masque.
                return half4(1, 1, 1, 1);
            }

            ENDHLSL
        }
    }
}