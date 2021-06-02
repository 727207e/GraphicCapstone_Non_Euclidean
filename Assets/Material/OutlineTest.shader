Shader "Unlit/OutlineTest"
{
    Properties
    {
        _Outline_Bold("Outline Bold", Range(0, 1)) = 0.1
        _Color("Color",Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
    }

        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        cull front
        Pass
        {
            CGPROGRAM
            #pragma vertex _Ver
            #pragma fragment _Fra
            #include "UnityCG.cginc"

            void _Ver() {

            }
            void _Fra() {

            }

            struct ST_VerIn {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct ST_VerOut {
                float4 vertex : SV_POSITION;
              };

            float _Outline_Bold;

            ST_VerOut _Ver(ST_VerIn stinput) {
                ST_VerOut stOutput;

                float3 fNormalized_Normal = normalize(stinput.normal); // 로컬 노말 벡터를 정규화

                 // 버텍스 좌표에 노말 방향으로 더한다
                float3 fOutline_Position = stinput.vertex + fNormalized_Normal * (_Outline_Bold * 0.1f);

                // 노말 방향으로 더해진 버텍스 좌표를 카메라 클립 공간으로 변환
                stOutput.vertex = UnityObjectToClipPos(fOutline_Position);
                return stOutput;
            }

            float4 _Fra(ST_VerOut i) : SV_Target
            {
                return 0.0f;
            }

            ENDCG
        }
    }
}