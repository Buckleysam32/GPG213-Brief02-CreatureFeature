// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/JordanNewMonochromeShader"
{
    Properties
    {
        // Color property for material inspector, default to white
        _Color ("Colour", Color) = (.5,.5,.5,1)
        _Transparency ("Transparency", Range(0.0, 1)) = 0.25
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcColor
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

             struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };
            
            float4 _Color;
            float _Transparency;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float intensity = (col.x * 0.299 + col.y * 0.587 + col.z * 0.114) /3;
                fixed4 monochrome = fixed4(intensity,intensity,intensity,_Transparency);
                return col;
            }
            ENDCG
        }
    }
}
