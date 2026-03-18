Shader "Custom/2D/Test001Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _ScrollSpeedX ("水平滚动速度", Range(-2, 2)) = 0.5
        _ScrollSpeedY ("垂直滚动速度", Range(-2, 2)) = 0.0
    }
    
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        
        Pass
        {
            HLSLPROGRAM
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
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            sampler2D _MainTex;
            float4 _Color;
            float _ScrollSpeedX;
            float _ScrollSpeedY;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // 核心：让UV随时间偏移
                float2 uv = i.uv + float2(_ScrollSpeedX, _ScrollSpeedY) * _Time.y;
                
                fixed4 col = tex2D(_MainTex, uv) * _Color;
                return col;
            }
            ENDHLSL
        }
    }
}