Shader "Hidden/LUTShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}

		_LUT("LUT", 2D) = "white" {}
        _LUTIntensity("Contribution", Range(0, 1)) = 1
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

			sampler2D _LUT;
            fixed4 _LUT_TexelSize;
            fixed _LUTIntensity;
			fixed _ColorsAmount;

			float4 applyLUT(float4 color) : COLOR
			{
				float colorsAmount = _ColorsAmount - 1.0;

                float texelHalfX = 0.5 / _LUT_TexelSize.z;
                float texelHalfY = 0.5 / _LUT_TexelSize.w;
                
				float threshold = colorsAmount / _ColorsAmount;
 
                float offsetX = texelHalfX + color.r * threshold / _ColorsAmount;
                float offsetY = texelHalfY + color.g * threshold;

                float cell = floor(color.b * colorsAmount);
 
                float2 lutPos = float2(cell / _ColorsAmount + offsetX, offsetY);
                float4 gradedCol = tex2D(_LUT, lutPos);
                 
                return lerp(color, gradedCol, _LUTIntensity);
			}

			float4 frag(v2f_img i) : COLOR
			{
				float4 color = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv, _MainTex_ST));
				return applyLUT(color);
			}

			ENDCG
		} 
	}
}    