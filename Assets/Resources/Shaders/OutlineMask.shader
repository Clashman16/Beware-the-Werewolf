Shader "BWW/OutlineMask"
{
    Properties
    {
        _MaskColor(
            "Mask Color",
            Color
        ) = (1, 0, 0, 1)

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
            ZWrite Off
            ZTest Always

            HLSLPROGRAM

            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _MaskColor;
                float _DepthTolerance;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
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

                output.viewDepth = -positionVS.z;

                return output;
            }

            half4 Frag(Varyings input) : SV_Target
            {
                float2 screenUV =
                    input.positionHCS.xy /
                    _ScaledScreenParams.xy;

                float sceneRawDepth =
                    SampleSceneDepth(screenUV);

                float sceneDepth =
                    LinearEyeDepth(
                        sceneRawDepth,
                        _ZBufferParams
                    );

                // Élimine les parties masquées par la scène.
                if (input.viewDepth >
                    sceneDepth + _DepthTolerance)
                {
                    discard;
                }

                return _MaskColor;
            }

            ENDHLSL
        }
    }
}