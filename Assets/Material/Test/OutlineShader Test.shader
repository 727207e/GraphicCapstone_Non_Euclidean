Shader "Unlit/OutlineShader"
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
                float3 fOutline_Position = stinput.vertex + fNormalized_Normal * (_Outline_Bold * 0.1f); // 버텍스 좌표에 노말 방향으로 더한다

                stOutput.vertex = UnityObjectToClipPos(fOutline_Position); // 노말 방향으로 더해진 버텍스 좌표를 카메라 클립 공간으로 변환
                return stOutput;
            }
        
            float4 _Fra(ST_VerOut i) : SV_Target
            {
                return 0.0f;
            }
           
            ENDCG
        }

        cull back
        CGPROGRAM

        #pragma surface surf _BandedLighting    //! 커스텀 라이트 사용

        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        fixed4 _Color;



        void surf(Input IN, inout SurfaceOutput o)
        {
            float4 fMainTex = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = fMainTex.rgb;
            o.Alpha = 1.0f;
        }

        //! 커스텀 라이트 함수
        float4 Lighting_BandedLighting(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
        {
            //! BandedDiffuse 조명 처리 연산
            float3 fBandedDiffuse;
            float fNDotL = dot(s.Normal, lightDir) * 0.1f + 0.1f;    //! Half Lambert 공식

            //! 0~1로 이루어진 fNDotL값을 2개의 값으로 고정함 <- Banded Lighting 작업
            float fBandNum = 2.0f;
            fBandedDiffuse = ceil(fNDotL * fBandNum) / fBandNum;


            //! 최종 컬러 출력
            float4 fFinalColor;
            fFinalColor.rgb = (s.Albedo) *
                                 fBandedDiffuse * _LightColor0.rgb * atten;
            fFinalColor.a = s.Alpha;

            return fFinalColor;
        }

        ENDCG

    }
}
