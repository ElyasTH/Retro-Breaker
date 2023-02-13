// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/VignetteShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_VignettePower ("VignettePower", Range(0.0,6.0)) = 5.5
        _VignetteCenter ("VignetteCenter", Vector) = (0.5, 0.5, 0.0, 0.0)
		_VignetteYTexCoordMod("Vignette Y tex mod", Float) = 0.0
	}
	SubShader 
	{
		Cull Off ZWrite Off ZTest Always 

		//SRP pass
		Pass
		{
			CGPROGRAM

			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			half4 _MainTex_ST;

			uniform fixed _VignettePower;
			uniform fixed2 _VignetteCenter = fixed2(0.5, 0.5);
			
			float4 frag(v2f_img i) : COLOR
			{

				float4 color = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv, _MainTex_ST));
				
				fixed2 dist = UnityStereoScreenSpaceUVAdjust(((i.uv + _VignetteCenter) - 0.5f) * 1.25f, _MainTex_ST);
				dist.x = 1 - dot(dist, dist) * _VignettePower;
				color *= dist.x;

				return color;
			}

			ENDCG
		} 

		//LWRP pass
		/*Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			uniform fixed _VignettePower;
			uniform fixed2 _VignetteCenter;
			
			v2f_img vert( appdata_img v )
			{
				v2f_img o;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = v.texcoord;

				#if UNITY_UV_STARTS_AT_TOP
					o.uv.y = 1 - o.uv.y;
				#endif

				return o;
			}

			float4 frag(v2f_img i) : COLOR
			{
				float4 color = tex2D(_MainTex, i.uv);
				
				fixed2 dist = ((i.uv + _VignetteCenter) - 0.5f) * 1.25f;
				dist.x = 1 - dot(dist, dist) * _VignettePower;
				color *= dist.x;
				
				return color;
			}

			ENDCG
		} */
	}
}    