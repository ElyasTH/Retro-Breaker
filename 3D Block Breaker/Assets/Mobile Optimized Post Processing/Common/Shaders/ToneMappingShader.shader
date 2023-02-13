Shader "Hidden/ToneMappingShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}

		_Gamma ("Gamma", Range(0.0,6.0)) = 1.0
        _Exposure ("Exposure", Float) = 1.0
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
			fixed _Gamma;
			fixed _Exposure;
			
			float4 toneMap(float4 color) : COLOR 
			{
				//exposure tone mapping
                fixed4 expo = exp(-color * _Exposure);
                fixed3 mapped = fixed3(1.0 - expo.r, 1.0 - expo.g, 1.0 - expo.b);
                
                //gamma correcting 
                mapped = pow(mapped, 1.0 / _Gamma);
				return float4(mapped, 1.0);
			}

			float4 frag(v2f_img i) : COLOR
			{
				float4 color = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv, _MainTex_ST));
				return toneMap(color);
			}

			ENDCG
		} 
	}
}    