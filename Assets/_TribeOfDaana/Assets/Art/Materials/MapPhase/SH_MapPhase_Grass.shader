// Made with Amplify Shader Editor v1.9.8.1
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SH_MapPhase_Grass"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0

        _Grasstexture("Grass texture", 2D) = "white" {}
        _NoiseIntensity("Noise Intensity", Float) = 0.33
        _ColorGrassShadow("Color Grass Shadow", Color) = (0.4867724,0.7075472,0.2569865,1)
        _ColorGrassLight("Color Grass Light", Color) = (0.5568628,0.7686275,0.3647059,1)
        _WindColorIntensity("Wind Color Intensity", Float) = 3
        _WindNoiseScale("Wind NoiseScale", Float) = 0.5

    }

    SubShader
    {
		LOD 0

        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

        Stencil
        {
        	Ref [_Stencil]
        	ReadMask [_StencilReadMask]
        	WriteMask [_StencilWriteMask]
        	Comp [_StencilComp]
        	Pass [_StencilOp]
        }


        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend One OneMinusSrcAlpha
        ColorMask [_ColorMask]

        
        Pass
        {
            Name "Default"
        CGPROGRAM
            #define ASE_VERSION 19801

            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.5

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            #include "UnityShaderVariables.cginc"


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
                float4 worldPosition : TEXCOORD1;
                float4  mask : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
                
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float _UIMaskSoftnessX;
            float _UIMaskSoftnessY;

            uniform float4 _ColorGrassLight;
            uniform float4 _ColorGrassShadow;
            uniform float _WindNoiseScale;
            uniform float _WindColorIntensity;
            uniform sampler2D _Grasstexture;
            uniform float4 _Grasstexture_ST;
            uniform float _NoiseIntensity;
            		float2 voronoihash3( float2 p )
            		{
            			
            			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
            			return frac( sin( p ) *43758.5453);
            		}
            
            		float voronoi3( float2 v, float time, inout float2 id, inout float2 mr, float smoothness, inout float2 smoothId )
            		{
            			float2 n = floor( v );
            			float2 f = frac( v );
            			float F1 = 8.0;
            			float F2 = 8.0; float2 mg = 0;
            			for ( int j = -1; j <= 1; j++ )
            			{
            				for ( int i = -1; i <= 1; i++ )
            			 	{
            			 		float2 g = float2( i, j );
            			 		float2 o = voronoihash3( n + g );
            					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
            					float d = 0.758 * pow( ( pow( abs( r.x ), 2.5 ) + pow( abs( r.y ), 2.5 ) ), 0.400 );
            			 		if( d<F1 ) {
            			 			F2 = F1;
            			 			F1 = d; mg = g; mr = r; id = o;
            			 		} else if( d<F2 ) {
            			 			F2 = d;
            			
            			 		}
            			 	}
            			}
            			return F2 - F1;
            		}
            


            v2f vert(appdata_t v )
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                

                v.vertex.xyz +=  float3( 0, 0, 0 ) ;

                float4 vPosition = UnityObjectToClipPos(v.vertex);
                OUT.worldPosition = v.vertex;
                OUT.vertex = vPosition;

                float2 pixelSize = vPosition.w;
                pixelSize /= float2(1, 1) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));

                float4 clampedRect = clamp(_ClipRect, -2e10, 2e10);
                float2 maskUV = (v.vertex.xy - clampedRect.xy) / (clampedRect.zw - clampedRect.xy);
                OUT.texcoord = v.texcoord;
                OUT.mask = float4(v.vertex.xy * 2 - clampedRect.xy - clampedRect.zw, 0.25 / (0.25 * half2(_UIMaskSoftnessX, _UIMaskSoftnessY) + abs(pixelSize.xy)));

                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN ) : SV_Target
            {
                //Round up the alpha color coming from the interpolator (to 1.0/256.0 steps)
                //The incoming alpha could have numerical instability, which makes it very sensible to
                //HDR color transparency blend, when it blends with the world's texture.
                const half alphaPrecision = half(0xff);
                const half invAlphaPrecision = half(1.0/alphaPrecision);
                IN.color.a = round(IN.color.a * alphaPrecision)*invAlphaPrecision;

                float2 texCoord29 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
                float2 panner11 = ( 1.0 * _Time.y * float2( 0.4,-0.4 ) + texCoord29);
                float time3 = panner11.x;
                float2 voronoiSmoothId3 = 0;
                float2 coords3 = panner11 * _WindNoiseScale;
                float2 id3 = 0;
                float2 uv3 = 0;
                float fade3 = 0.5;
                float voroi3 = 0;
                float rest3 = 0;
                for( int it3 = 0; it3 <3; it3++ ){
                voroi3 += fade3 * voronoi3( coords3, time3, id3, uv3, 0,voronoiSmoothId3 );
                rest3 += fade3;
                coords3 *= 2;
                fade3 *= 0.5;
                }//Voronoi3
                voroi3 /= rest3;
                float3 lerpResult49 = lerp( _ColorGrassLight.rgb , _ColorGrassShadow.rgb , ( max( voroi3 , 0.1 ) * _WindColorIntensity ));
                float3 break53 = lerpResult49;
                float2 uv_Grasstexture = IN.texcoord.xy * _Grasstexture_ST.xy + _Grasstexture_ST.zw;
                float2 texCoord15 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
                float2 lerpResult14 = lerp( ( uv_Grasstexture * voroi3 ) , texCoord15 , _NoiseIntensity);
                float4 appendResult50 = (float4(break53.x , break53.y , break53.z , tex2D( _Grasstexture, lerpResult14 ).a));
                

                half4 color = appendResult50;

                #ifdef UNITY_UI_CLIP_RECT
                half2 m = saturate((_ClipRect.zw - _ClipRect.xy - abs(IN.mask.xy)) * IN.mask.zw);
                color.a *= m.x * m.y;
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                color.rgb *= color.a;

                return color;
            }
        ENDCG
        }
    }
    CustomEditor "AmplifyShaderEditor.MaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19801
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-2128,480;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;11;-1776,528;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.4,-0.4;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;66;-1791.644,748.0573;Inherit;False;Property;_WindNoiseScale;Wind NoiseScale;5;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;6;-1680,-368;Inherit;True;Property;_Grasstexture;Grass texture;0;0;Create;True;0;0;0;False;0;False;eca68205e9764974eb6eba6ee6b0bf47;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.VoronoiNode;3;-1536,464;Inherit;True;0;4;2.5;2;3;False;5;False;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;3.16;False;2;FLOAT;0.35;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-1408,-288;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;62;-1104,624;Inherit;False;Property;_WindColorIntensity;Wind Color Intensity;4;0;Create;True;0;0;0;False;0;False;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;64;-1216,528;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-1136,-240;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-1136,320;Inherit;False;Property;_NoiseIntensity;Noise Intensity;1;0;Create;True;0;0;0;False;0;False;0.33;0.33;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-1184,32;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;51;-704,16;Inherit;False;Property;_ColorGrassLight;Color Grass Light;3;0;Create;True;0;0;0;False;0;False;0.5568628,0.7686275,0.3647059,1;0.5568628,0.7686275,0.3647059,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;52;-688,208;Inherit;False;Property;_ColorGrassShadow;Color Grass Shadow;2;0;Create;True;0;0;0;False;0;False;0.4867724,0.7075472,0.2569865,1;0.4867724,0.7075472,0.2569865,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;-848,544;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;3.55;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;14;-896,-240;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;49;-352,112;Inherit;True;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;1;-640,-364;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.BreakToComponentsNode;53;-96,160;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMinOpNode;45;-672,432;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;50;80,128;Inherit;True;COLOR;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;26;352,32;Float;False;True;-1;3;AmplifyShaderEditor.MaterialInspector;0;3;SH_MapPhase_Grass;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;Default;2;False;True;3;1;False;;10;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;True;True;True;True;True;0;True;_ColorMask;False;False;False;False;False;False;False;True;True;0;True;_Stencil;255;True;_StencilReadMask;255;True;_StencilWriteMask;0;True;_StencilComp;0;True;_StencilOp;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;0;True;unity_GUIZTestMode;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;False;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;11;0;29;0
WireConnection;3;0;11;0
WireConnection;3;1;11;0
WireConnection;3;2;66;0
WireConnection;5;2;6;0
WireConnection;64;0;3;0
WireConnection;8;0;5;0
WireConnection;8;1;3;0
WireConnection;61;0;64;0
WireConnection;61;1;62;0
WireConnection;14;0;8;0
WireConnection;14;1;15;0
WireConnection;14;2;19;0
WireConnection;49;0;51;5
WireConnection;49;1;52;5
WireConnection;49;2;61;0
WireConnection;1;0;6;0
WireConnection;1;1;14;0
WireConnection;53;0;49;0
WireConnection;45;0;3;0
WireConnection;50;0;53;0
WireConnection;50;1;53;1
WireConnection;50;2;53;2
WireConnection;50;3;1;4
WireConnection;26;0;50;0
ASEEND*/
//CHKSM=526A7AAB9D03D97D2D442D1CCEEA2CB380718C04