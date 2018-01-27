Shader "Custom/VerticalHoldEffect" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

		SubShader{
		Pass{
		ZTest Always Cull Off ZWrite Off
		Fog{ Mode off }

		CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

	uniform sampler2D _MainTex;
	uniform half _xRes;
	uniform half _yRes;
	uniform half _time;

	fixed4 frag(v2f_img i) : COLOR
	{
		fixed xPixel = 1.0f / _xRes;
	fixed yPixel = 1.0f / _yRes;

	fixed2 uv = i.uv;
	fixed desiredX = uv.x;
	fixed desiredY = uv.y;

	// offset the y pixel and wrap
	desiredY = uv.y - ((_time * _yRes) * yPixel);
	if (desiredY > 1.0f) {
		desiredY = (desiredY - 1.0) * yPixel;
	}
	fixed2 desiredUV = fixed2(desiredX, desiredY);
	fixed4 main = tex2D(_MainTex, desiredUV);
	return main;
	}
		ENDCG
	}
	}
		Fallback off
}