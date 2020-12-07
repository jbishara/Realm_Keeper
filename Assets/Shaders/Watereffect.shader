Shader "Custome/Waves"
{
	Properties
	{
		//_MainTexture("Texture", 2D) = "white" {}
		_Color("Colour", Color) = (0, 0, 0, 1)
		_Strength("Strength", Range(0,2)) = 1.0
		_Speed("Speed", Range(-200,200)) = 100
	}
	SubShader
	{
		Tags
		{"RenderType" = "transparent"}
		Pass
		{



		Cull off

		CGPROGRAM

		#pragma vertex vertexFunc
		#pragma fragment fragmentFunc

		float4 _Color;
		float _Strength;
		float _Speed;
		//sampler2D _MainTexture;

			struct vertexInput 
			{
				float4 vertex : POSITION;
			};

			struct vertexOutput 
			{
				float4 pos : SV_POSITION;
			};

			vertexOutput vertexFunc(vertexInput IN) 
			{
				vertexOutput o;

				float4 worldPos = mul(unity_ObjectToWorld, IN.vertex);

				// Takes the vertex and moves it (Makes the wave effect)
				float displacement = (cos(worldPos.y) + cos(worldPos.x + (_Speed * _Time)));
				worldPos.y = worldPos.y + (displacement * _Strength);

				o.pos = mul(UNITY_MATRIX_VP, worldPos);
				return o;
			}

			float4 fragmentFunc(vertexOutput IN) : COLOR
			{
				return _Color;

			}
		ENDCG
		}
	}
}