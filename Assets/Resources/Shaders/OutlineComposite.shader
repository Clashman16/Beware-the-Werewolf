Shader "BWW/OutlineComposite"
{
    Properties
    {
        _OutlineColor(
            "Outline Color",
            Color
        ) = (1, 1, 0, 1)

        _OutlineThickness(
            "Outline Thickness",
            Range(1, 10)
        ) = 2

        _Threshold(
            "Threshold",
            Range(0, 1)
        ) = 0.1
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Transparent"
            "Queue" = "Overlay"
        }

        Pass
        {
            Name "OutlineComposite"

            ZWrite Off
            ZTest Always
            Cull Off

            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM

            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_OutlineMaskTexture);
            SAMPLER(sampler_OutlineMaskTexture);

            float4 _OutlineMaskTexture_TexelSize;

            float4 _OutlineColor;
            float _OutlineThickness;
            float _Threshold;

            struct Attributes
            {
                uint vertexID : SV_VertexID;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings Vert(Attributes input)
            {
                Varyings output;

                output.positionHCS =
                    GetFullScreenTriangleVertexPosition(input.vertexID);

                output.uv =
                    GetFullScreenTriangleTexCoord(input.vertexID);

                return output;
            }

            float SampleMask(float2 uv)
            {
                return SAMPLE_TEXTURE2D(
                    _OutlineMaskTexture,
                    sampler_OutlineMaskTexture,
                    uv
                ).r;
            }

            half4 Frag(Varyings input) : SV_Target
            {
                float2 texel =
                    _OutlineMaskTexture_TexelSize.xy
                    * _OutlineThickness;

                float center = SampleMask(input.uv);

                float left =
                    SampleMask(input.uv + float2(-texel.x, 0));

                float right =
                    SampleMask(input.uv + float2(texel.x, 0));

                float up =
                    SampleMask(input.uv + float2(0, texel.y));

                float down =
                    SampleMask(input.uv + float2(0, -texel.y));

                float topLeft =
                    SampleMask(input.uv + float2(-texel.x, texel.y));

                float topRight =
                    SampleMask(input.uv + float2(texel.x, texel.y));

                float bottomLeft =
                    SampleMask(input.uv + float2(-texel.x, -texel.y));

                float bottomRight =
                    SampleMask(input.uv + float2(texel.x, -texel.y));

                float edge = 0;

                edge = max(edge, abs(center - left));
                edge = max(edge, abs(center - right));
                edge = max(edge, abs(center - up));
                edge = max(edge, abs(center - down));

                edge = max(edge, abs(center - topLeft));
                edge = max(edge, abs(center - topRight));
                edge = max(edge, abs(center - bottomLeft));
                edge = max(edge, abs(center - bottomRight));

                edge = step(_Threshold, edge);

                // Seulement à l'extérieur de la silhouette.
                edge *= 1.0 - center;

                return half4(
                    _OutlineColor.rgb,
                    _OutlineColor.a * edge
                );
            }

            ENDHLSL
        }
    }
}