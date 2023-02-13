Shader "Hidden/ChromaticAbberationShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}

		_ColorShiftFactor ("Color shift factor", Range(0.0,6.0)) = 1.0
		_FishEyeEffectFactor ("Fish eye effect factor", Range(0.0, 1.0)) = 1.0
		_FishEyeEffectStart ("Fish eye effect start", Range(0.0, 1.0)) = 0.0
		_FishEyeEffectEnd("Fish eye effect end", Range(0.0, 1.0)) = 1.0
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
			fixed4 _MainTex_TexelSize;

			fixed _ColorShiftFactor;
			fixed _FishEyeEffectFactor;
			fixed _FishEyeEffectStart;
			fixed _FishEyeEffectEnd;
			
			float4 frag(v2f_img i) : COLOR
			{
				float2 shiftFactor = float2(_ColorShiftFactor, _ColorShiftFactor);
                
				fixed2 currentPosition = (i.pos.xy * _MainTex_TexelSize.xy) - fixed2(0.5, 0.5);
				
				fixed distance = length(currentPosition) * 1.65;
				
				fixed fishEye = smoothstep(_FishEyeEffectEnd, _FishEyeEffectStart, distance);

				//apply fish eye effect
				shiftFactor *= lerp(1.0 - fishEye, 1, 1.0 - _FishEyeEffectFactor); 

				fixed2 dirFromCenter = normalize(currentPosition - fixed2(0.5, 0.5));

                float4 rValue = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv - (shiftFactor * dirFromCenter), _MainTex_ST));
                float4 gValue = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv, _MainTex_ST));
                float4 bValue = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv + (shiftFactor * dirFromCenter), _MainTex_ST));

                return float4(rValue.r, gValue.g, bValue.b, 1.0);
			}

			ENDCG
		} 
	}
}    