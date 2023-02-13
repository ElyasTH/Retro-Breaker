Shader "Hidden/BloomCombineShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
        _FilteredTex("Filtered scene", 2D) = "white" {}
	}
	SubShader 
	{
		Cull Off ZWrite Off ZTest Always
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			half4 _MainTex_ST;

            sampler2D _FilteredTex;
			half4 _FilteredTex_ST;

			float4 frag(v2f_img i) : COLOR
			{
				float4 color = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv, _MainTex_ST));
				float4 filteredColor = tex2D(_FilteredTex, UnityStereoScreenSpaceUVAdjust(i.uv, _FilteredTex_ST));

                return color + filteredColor; //additive blend normal scene image with blurred one
			}

			ENDCG
		} 

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _FilteredTex_LWRP;

			float4 frag(v2f_img i) : COLOR
			{
				float4 color = tex2D(_MainTex, i.uv);
				float4 filteredColor = tex2D(_FilteredTex_LWRP, i.uv);

				return color + filteredColor; //additive blend normal scene image with blurred one
			}

			ENDCG
		}
	}
}    