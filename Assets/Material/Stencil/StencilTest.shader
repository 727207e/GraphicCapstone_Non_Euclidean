﻿Shader "Unlit/StencilTest"
{
    Properties{
         _Color("Color", Color) = (1,1,1,1)
         _MainTex("Albedo (RGB)", 2D) = "white" {}
         _Glossiness("Smoothness", Range(0,1)) = 0.5
         _Metallic("Metallic", Range(0,1)) = 0.0
         _StencilMask("Stencil Mask", Range(0, 255)) = 1
          
    }
        SubShader{
            Tags { "RenderType" = "Opaque" "Queue" = "Geometry+1" }
            LOD 200
            Stencil
            {
                Ref[_StencilMask]
                Comp Equal
                Pass keep
            }

            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows
            #pragma target 3.0

            sampler2D _MainTex;

            struct Input {
                float2 uv_MainTex;
            };

            half _Glossiness;
            half _Metallic;
            fixed4 _Color;

            void surf(Input IN, inout SurfaceOutputStandard o) {

                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;

                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = c.a;
            }
            ENDCG
         }
             FallBack "Diffuse"
}