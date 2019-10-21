// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SpriteFlash" 
{
	Properties
	{
		[HideInInspector]
		_MainTex ("Texture", 2D) = "white" {}
		_FlashColor ("Flash Color", Color) = (1,1,1,1)
		_TintColor ("Tint Color", Color) = (1,1,1,1)
		_Intensity ("Intensity", Range(0,1)) = 0.0
	}
	SubShader 
	{
		Tags
		{
			"Queue" = "Transparent"
		}

		Pass
		{
			Cull Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex simplevert
			#pragma fragment simplefrag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f simplevert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 _FlashColor;
			fixed4 _TintColor;
			float _Intensity;
			sampler2D _MainTex;

			float4 simplefrag(v2f i) : SV_Target
			{
				float4 color = tex2D(_MainTex, i.uv);

				float4 colorDifference = _FlashColor - color;

				return color * (_TintColor * color.a) + (colorDifference * _Intensity * color.a);
			}

			ENDCG
		}
	}

	Fallback "Sprites/Diffuse"
}
