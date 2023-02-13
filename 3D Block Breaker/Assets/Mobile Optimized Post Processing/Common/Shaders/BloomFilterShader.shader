Shader "Hidden/BloomFilterShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
        _Intensity("Intensity", Float) = 10
		_ValuesCombined("Bloom properties combined into one vector", Vector) = (0,0,0,0)
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

            float _Intensity;
			float4 _ValuesCombined;

			float4 frag(v2f_img i) : COLOR
			{
				float4 color = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv, _MainTex_ST));
				half brightness = max(color.r, max(color.g, color.b));
				
				half soft = brightness - _ValuesCombined.y;
				soft = clamp(soft, 0, _ValuesCombined.z);
				soft = soft * soft * _ValuesCombined.w;
				half contribution = max(soft, brightness - _ValuesCombined.x);
				contribution /= max(brightness, 0.00001);
				
				half4 colorFiltered = color * contribution;
				
				return colorFiltered * _Intensity;
			}

			ENDCG
		} 
	}
}    