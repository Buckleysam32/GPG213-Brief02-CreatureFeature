// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/JordanMonochromeShader"
{
    Properties
    {
    }
    SubShader
    {
        Tags{ "Queue" = "Overlay" "RenderType" = "Opaque" }
        GrabPass
    {
        Name "BASE"
        Tags{ "LightMode" = "Always" }
    }
        Stencil
        {
            Ref 1
            Comp NotEqual
            Pass Keep
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 UV : TEXCOORD0;
            };
 
            struct v2f
            {
                float4 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _GrabTexture;
                        
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv.xy = (float2(o.vertex.x, -o.vertex.y) + o.vertex.w) * 0.5;
                o.uv.zw = o.vertex.zw;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                float4 scenePixel = tex2Dproj(_GrabTexture, i.uv);
                float average = (scenePixel.r + scenePixel.g + scenePixel.b) / 3;
 
                return float4(average, average, average, 1);                
            }
            ENDCG
        }
    }
}
