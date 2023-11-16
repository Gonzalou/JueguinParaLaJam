// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "New Amplify Shader"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		_PERSONA_RANDOM_1__SPRITESHEET("PERSONA_RANDOM_1_-_SPRITESHEET", 2D) = "white" {}
		_Pelo("Pelo", Color) = (0.4150943,0.4150943,0.4150943,1)
		_Ropa("Ropa", Color) = (0,0.153555,0.8301887,1)
		_Piel("Piel", Color) = (0.7830189,0.7830189,0.7830189,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		
		
		Pass
		{
		CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"
			

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				
			};
			
			uniform fixed4 _Color;
			uniform float _EnableExternalAlpha;
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			uniform half4 _Piel;
			uniform sampler2D _PERSONA_RANDOM_1__SPRITESHEET;
			uniform float4 _PERSONA_RANDOM_1__SPRITESHEET_ST;
			uniform float4 _Pelo;
			uniform float4 _Ropa;

			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				
				
				IN.vertex.xyz +=  float3(0,0,0) ; 
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				fixed4 alpha = tex2D (_AlphaTex, uv);
				color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
#endif //ETC1_EXTERNAL_ALPHA

				return color;
			}
			
			fixed4 frag(v2f IN  ) : SV_Target
			{
				float2 uv_PERSONA_RANDOM_1__SPRITESHEET = IN.texcoord.xy * _PERSONA_RANDOM_1__SPRITESHEET_ST.xy + _PERSONA_RANDOM_1__SPRITESHEET_ST.zw;
				float4 tex2DNode1 = tex2D( _PERSONA_RANDOM_1__SPRITESHEET, uv_PERSONA_RANDOM_1__SPRITESHEET );
				float4 lerpResult4 = lerp( float4( 0,0,0,0 ) , _Piel , tex2DNode1.r);
				float4 lerpResult5 = lerp( float4( 0,0,0,0 ) , _Pelo , tex2DNode1.g);
				float4 lerpResult6 = lerp( float4( 0,0,0,0 ) , _Ropa , tex2DNode1.b);
				float4 color31 = IsGammaSpace() ? float4(0,0,0,1) : float4(0,0,0,1);
				float grayscale28 = Luminance(tex2DNode1.rgb);
				float temp_output_3_0_g4 = ( grayscale28 - 0.89 );
				float4 lerpResult11 = lerp( float4( 0,0,0,0 ) , ( lerpResult4 + lerpResult5 + lerpResult6 + color31 + saturate( ( temp_output_3_0_g4 / fwidth( temp_output_3_0_g4 ) ) ) ) , tex2DNode1.a);
				float4 clampResult32 = clamp( lerpResult11 , float4( 0,0,0,0 ) , float4( 1,1,1,1 ) );
				
				fixed4 c = clampResult32;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17200
258;1080;1565;978;1516.501;1263.76;2.045263;True;True
Node;AmplifyShaderEditor.SamplerNode;1;-1324.286,64.49249;Inherit;True;Property;_PERSONA_RANDOM_1__SPRITESHEET;PERSONA_RANDOM_1_-_SPRITESHEET;0;0;Create;True;0;0;False;0;-1;034877212b5d86044a9a8b9210988c1c;034877212b5d86044a9a8b9210988c1c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCGrayscale;28;-1045.048,-251.3227;Inherit;True;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;10;-609.6912,328.6933;Inherit;False;Property;_Ropa;Ropa;2;0;Create;True;0;0;False;0;0,0.153555,0.8301887,1;1,0.8378445,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;9;-608.5607,98.29979;Inherit;False;Property;_Pelo;Pelo;1;0;Create;True;0;0;False;0;0.4150943,0.4150943,0.4150943,1;0,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;8;-531.5592,-143.3475;Half;False;Property;_Piel;Piel;3;0;Create;True;0;0;False;0;0.7830189,0.7830189,0.7830189,1;0.8207547,0.6,0.375534,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;6;-226.7579,337.3119;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;4;-261.7801,-169.3233;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;31;-1059.61,-456.5738;Inherit;False;Constant;_Color3;Color 3;4;0;Create;True;0;0;False;0;0,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;5;-241.2354,83.00232;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;35;-746.2973,-500.1257;Inherit;True;Step Antialiasing;-1;;4;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0.89;False;2;FLOAT;0.66;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;48.20977,-37.6922;Inherit;True;5;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;11;331.2585,-44.84443;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;30;-536.36,-337.2647;Inherit;False;3;0;COLOR;4,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;32;645.8604,-68.56871;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,1;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;34;-792.0302,-254.354;Inherit;True;Step Antialiasing;-1;;5;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0.17;False;2;FLOAT;0.66;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;848.6289,-117.0455;Float;False;True;2;ASEMaterialInspector;0;11;New Amplify Shader;0f8ba0101102bb14ebf021ddadce9b49;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;3;1;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;False;False;True;2;False;-1;False;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;0
WireConnection;28;0;1;0
WireConnection;6;1;10;0
WireConnection;6;2;1;3
WireConnection;4;1;8;0
WireConnection;4;2;1;1
WireConnection;5;1;9;0
WireConnection;5;2;1;2
WireConnection;35;2;28;0
WireConnection;7;0;4;0
WireConnection;7;1;5;0
WireConnection;7;2;6;0
WireConnection;7;3;31;0
WireConnection;7;4;35;0
WireConnection;11;1;7;0
WireConnection;11;2;1;4
WireConnection;30;0;31;0
WireConnection;30;2;34;0
WireConnection;32;0;11;0
WireConnection;34;2;28;0
WireConnection;0;0;32;0
ASEEND*/
//CHKSM=BF7ACDF1D80B4C59762C275A9ACE38D79E530CFC