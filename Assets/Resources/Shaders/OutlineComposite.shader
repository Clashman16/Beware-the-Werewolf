Shader "BWW/OutlineCompositeMulticolor"
{
    Properties
    {
        _YellowOutlineColor(
            "Yellow Outline Color",
            Color
        ) = (1, 1, 0, 1)

        _BlueOutlineColor(
            "Blue Outline Color",
            Color
        ) = (0, 0.5, 1, 1)

        _YellowThickness(
            "Yellow Thickness",
            Range(1, 10)
        ) = 2

        _BlueThickness(
            "Blue Thickness",
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

            CBUFFER_START(UnityPerMaterial)
                float4 _YellowOutlineColor;
                float4 _BlueOutlineColor;

                float _YellowThickness;
                float _BlueThickness;
                float _Threshold;
            CBUFFER_END

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
                    GetFullScreenTriangleVertexPosition(
                        input.vertexID
                    );

                output.uv =
                    GetFullScreenTriangleTexCoord(
                        input.vertexID
                    );

                return output;
            }

            float4 SampleMask(float2 uv)
            {
                return SAMPLE_TEXTURE2D(
                    _OutlineMaskTexture,
                    sampler_OutlineMaskTexture,
                    uv
                );
            }

            float FindChannelEdge(
                float2 uv,
                float thickness,
                int channel)
            {
                float2 texel =
                    _OutlineMaskTexture_TexelSize.xy
                    * thickness;

                float4 centerSample =
                    SampleMask(uv);

                float center =
                    centerSample[channel];

                float edge = 0;

                edge = max(
                    edge,
                    abs(
                        center -
                        SampleMask(
                            uv + float2(-texel.x, 0)
                        )[channel]
                    )
                );

                edge = max(
                    edge,
                    abs(
                        center -
                        SampleMask(
                            uv + float2(texel.x, 0)
                        )[channel]
                    )
                );

                edge = max(
                    edge,
                    abs(
                        center -
                        SampleMask(
                            uv + float2(0, texel.y)
                        )[channel]
                    )
                );

                edge = max(
                    edge,
                    abs(
                        center -
                        SampleMask(
                            uv + float2(0, -texel.y)
                        )[channel]
                    )
                );

                edge = max(
                    edge,
                    abs(
                        center -
                        SampleMask(
                            uv + float2(-texel.x, texel.y)
                        )[channel]
                    )
                );

                edge = max(
                    edge,
                    abs(
                        center -
                        SampleMask(
                            uv + float2(texel.x, texel.y)
                        )[channel]
                    )
                );

                edge = max(
                    edge,
                    abs(
                        center -
                        SampleMask(
                            uv + float2(-texel.x, -texel.y)
                        )[channel]
                    )
                );

                edge = max(
                    edge,
                    abs(
                        center -
                        SampleMask(
                            uv + float2(texel.x, -texel.y)
                        )[channel]
                    )
                );

                edge = step(
                    _Threshold,
                    edge
                );

                // Ne conserve que le bord extérieur.
                edge *= 1.0 - center;

                return edge;
            }

            half4 Frag(Varyings input) : SV_Target
            {
                // Canal R = jaune.
                float yellowEdge =
                    FindChannelEdge(
                        input.uv,
                        _YellowThickness,
                        0
                    );

                // Canal G = bleu.
                float blueEdge =
                    FindChannelEdge(
                        input.uv,
                        _BlueThickness,
                        1
                    );

                float edgeSum =
                    yellowEdge + blueEdge;

                if (edgeSum <= 0.0001)
                {
                    return half4(0, 0, 0, 0);
                }

                /*
                 * Si les deux contours se superposent,
                 * on calcule une moyenne des couleurs.
                 */
                float3 outlineColor =
                    (
                        _YellowOutlineColor.rgb
                        * yellowEdge
                        +
                        _BlueOutlineColor.rgb
                        * blueEdge
                    )
                    / max(edgeSum, 0.0001);

                float alpha = max(
                    _YellowOutlineColor.a
                    * yellowEdge,
                    _BlueOutlineColor.a
                    * blueEdge
                );

                return half4(
                    outlineColor,
                    alpha
                );
            }

            ENDHLSL
        }
    }
}