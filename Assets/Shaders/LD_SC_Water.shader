Shader "Water/LD_SC_Water"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _EdgeColor("Edge Color", Color) = (1,1,1,1)
        _DepthFactor("Depth Factor",  float) = 1.0
        _WaveSpeed("Wave Speed", float) = 1.0
        _WaveAmp("Wave Amp", float) =  0.2
        _DepthRampTex("Depth Ramp", 2D) = "white" {}
        _NoiseTex("Noise Texture", 2D) = "white" {}
        _MainTex ("Texture", 2D) = "white" {}
        _DistortStrength("Distort Strength", float) = 1.0
        _ExtraHeight("Extra Height", float) = 0.0

    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }

        GrabPass
        {
            "_BackgroundTexture"
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
