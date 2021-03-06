Shader "Unreal/M_Sky_Panning_Clouds2(MaterialInstanceDynamic_0)"
{
	Properties
	{
		_MainTex("MainTex (RGB)", 2D) = "white" {}				Material_Texture2D_0( "None", 2D ) = "white" {}
		Material_Texture2D_1( "None", 2D ) = "white" {}
		Material_Texture2D_2( "None", 2D ) = "white" {}


		View_BufferSizeAndInvSize( "View_BufferSizeAndInvSize", Vector ) = ( 1920,1080,0.00052, 0.00092 )//1920,1080,1/1920, 1/1080
	}
	SubShader 
	{
		 Tags { "RenderType" = "Opaque" }
		//BLEND_ON Tags { "RenderType" = "Transparent"  "Queue" = "Transparent" }
		
		//Blend SrcAlpha OneMinusSrcAlpha
		//Cull Off

		CGPROGRAM

		#include "UnityPBSLighting.cginc"
		 #pragma surface surf Standard vertex:vert addshadow
		//BLEND_ON #pragma surface surf Standard vertex:vert alpha:fade addshadow
		
		#pragma target 5.0

		#define NUM_TEX_COORD_INTERPOLATORS 2
		#define NUM_CUSTOM_VERTEX_INTERPOLATORS 0

		struct Input
		{
			//float3 Normal;
			float2 uv_MainTex : TEXCOORD0;
			float2 uv2_Material_Texture2D_0 : TEXCOORD1;
			float4 color : COLOR;
			float4 tangent;
			//float4 normal;
			float3 viewDir;
			float4 screenPos;
			float3 worldPos;
			//float3 worldNormal;
			float3 normal2;
			INTERNAL_DATA
		};
		void vert( inout appdata_full i, out Input o )
		{
			float3 p_normal = mul( float4( i.normal, 0.0f ), unity_WorldToObject );
			//half4 p_tangent = mul( unity_ObjectToWorld,i.tangent );

			//half3 normal_input = normalize( p_normal.xyz );
			//half3 tangent_input = normalize( p_tangent.xyz );
			//half3 binormal_input = cross( p_normal.xyz,tangent_input.xyz ) * i.tangent.w;
			UNITY_INITIALIZE_OUTPUT( Input, o );

			//o.worldNormal = p_normal;
			o.normal2 = p_normal;
			o.tangent = i.tangent;
			//o.binormal_input = binormal_input;
		}
		uniform sampler2D _MainTex;
		/*
		struct SurfaceOutputStandard
		{
		fixed3 Albedo;		// base (diffuse or specular) color
		fixed3 Normal;		// tangent space normal, if written
		half3 Emission;
		half Metallic;		// 0=non-metal, 1=metal
		// Smoothness is the user facing name, it should be perceptual smoothness but user should not have to deal with it.
		// Everywhere in the code you meet smoothness it is perceptual smoothness
		half Smoothness;	// 0=rough, 1=smooth
		half Occlusion;		// occlusion (default 1)
		fixed Alpha;		// alpha for transparencies
		};
		*/


		#define Texture2D sampler2D
		#define TextureCube samplerCUBE
		#define SamplerState int
		//struct Material
		//{
			//samplers start
			uniform sampler2D    Material_Texture2D_0;
			uniform SamplerState Material_Texture2D_0Sampler;
			uniform sampler2D    Material_Texture2D_1;
			uniform SamplerState Material_Texture2D_1Sampler;
			uniform sampler2D    Material_Texture2D_2;
			uniform SamplerState Material_Texture2D_2Sampler;
			
		//};
		struct MaterialStruct
		{
			float4 VectorExpressions[16];
			float4 ScalarExpressions[4];
		};
		struct ViewStruct
		{
			float GameTime;
			float MaterialTextureMipBias;
			SamplerState MaterialTextureBilinearWrapedSampler;
			SamplerState MaterialTextureBilinearClampedSampler;
			float4 PrimitiveSceneData[ 40 ];
			float2 TemporalAAParams;
			float2 ViewRectMin;
			float4 ViewSizeAndInvSize;
			float MaterialTextureDerivativeMultiply;
		};
		struct ResolvedViewStruct
		{
			float3 WorldCameraOrigin;
			float4 ScreenPositionScaleBias;
			float4x4 TranslatedWorldToView;
			float4x4 TranslatedWorldToCameraView;
			float4x4 ViewToTranslatedWorld;
			float4x4 CameraViewToTranslatedWorld;
		};
		struct PrimitiveStruct
		{
			float4x4 WorldToLocal;
			float4x4 LocalToWorld;
		};

		ViewStruct View;
		ResolvedViewStruct ResolvedView;
		PrimitiveStruct Primitive;
		uniform float4 View_BufferSizeAndInvSize;
		uniform int Material_Wrap_WorldGroupSettings;

		//#define Primitive_WorldToLocal unity_WorldToObject

		#include "UnrealCommon.cginc"

		MaterialStruct Material;void InitializeExpressions()
{
	Material.VectorExpressions[0] = float4(0.000000,0.000000,0.000000,0.000000);//
	Material.VectorExpressions[1] = float4(0.270833,0.149853,0.060655,1.000000);//
	Material.VectorExpressions[2] = float4(0.270833,0.149853,0.060655,0.000000);//
	Material.VectorExpressions[3] = float4(0.130208,0.126108,0.106472,1.000000);//
	Material.VectorExpressions[4] = float4(0.130208,0.126108,0.106472,0.000000);//
	Material.VectorExpressions[5] = float4(-0.000000,-0.000000,-1.000000,1.000000);//
	Material.VectorExpressions[6] = float4(-0.000000,-0.000000,-1.000000,0.000000);//
	Material.VectorExpressions[7] = float4(-0.000000,-0.000000,-1.000000,0.000000);//
	Material.VectorExpressions[8] = float4(0.954000,0.901000,0.744120,1.000000);//
	Material.VectorExpressions[9] = float4(0.954000,0.901000,0.744120,0.000000);//
	Material.VectorExpressions[10] = float4(47.700001,45.050003,37.206001,0.000000);//
	Material.VectorExpressions[11] = float4(0.125000,0.054338,0.016184,1.000000);//
	Material.VectorExpressions[12] = float4(0.125000,0.054338,0.016184,0.000000);//
	Material.VectorExpressions[13] = float4(0.026042,0.026042,0.026042,1.000000);//
	Material.VectorExpressions[14] = float4(0.026042,0.026042,0.026042,0.000000);//
	Material.VectorExpressions[15] = float4(0.000000,0.000000,0.000000,0.000000);//
	Material.ScalarExpressions[0] = float4(0.100000,0.000000,5.018462,1.000000);
	Material.ScalarExpressions[1] = float4(1.000000,0.000300,0.000300,3333.333252);
	Material.ScalarExpressions[2] = float4(50.000000,2.000000,14.431911,4.000000);
	Material.ScalarExpressions[3] = float4(1.000000,0.000000,0.000000,0.000000);
}void CalcPixelMaterialInputs(in out FMaterialPixelParameters Parameters, in out FPixelMaterialInputs PixelMaterialInputs)
{
	// Initial calculations (required for Normal)

	// The Normal is a special case as it might have its own expressions and also be used to calculate other inputs, so perform the assignment here
	PixelMaterialInputs.Normal = MaterialFloat3(0.00000000,0.00000000,1.00000000);


	// Note that here MaterialNormal can be in world space or tangent space
	float3 MaterialNormal = GetMaterialNormal(Parameters, PixelMaterialInputs);

#if MATERIAL_TANGENTSPACENORMAL
#if SIMPLE_FORWARD_SHADING
	Parameters.WorldNormal = float3(0, 0, 1);
#endif

#if FEATURE_LEVEL >= FEATURE_LEVEL_SM4
	// Mobile will rely on only the final normalize for performance
	MaterialNormal = normalize(MaterialNormal);
#endif

	// normalizing after the tangent space to world space conversion improves quality with sheared bases (UV layout to WS causes shrearing)
	// use full precision normalize to avoid overflows
	Parameters.WorldNormal = TransformTangentNormalToWorld(Parameters.TangentToWorld, MaterialNormal);

#else //MATERIAL_TANGENTSPACENORMAL

	Parameters.WorldNormal = normalize(MaterialNormal);

#endif //MATERIAL_TANGENTSPACENORMAL

#if MATERIAL_TANGENTSPACENORMAL
	// flip the normal for backfaces being rendered with a two-sided material
	Parameters.WorldNormal *= Parameters.TwoSidedSign;
#endif

	Parameters.ReflectionVector = ReflectionAboutCustomWorldNormal(Parameters, Parameters.WorldNormal, false);

#if !PARTICLE_SPRITE_FACTORY
	Parameters.Particle.MotionBlurFade = 1.0f;
#endif // !PARTICLE_SPRITE_FACTORY

	// Now the rest of the inputs
	MaterialFloat2 Local0 = (Parameters.TexCoords[1].xy * 12.00000000);
	MaterialFloat4 Local1 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_0, Material_Texture2D_0Sampler,Local0,View.MaterialTextureMipBias));
	MaterialFloat3 Local2 = (Local1.rgb * Material.ScalarExpressions[0].x);
	MaterialFloat3 Local3 = (Local2 * Material.ScalarExpressions[0].y);
	MaterialFloat3 Local4 = (Material.VectorExpressions[2].rgb + Local3);
	MaterialFloat Local5 = dot(Parameters.CameraVector, MaterialFloat3(0.00000000,0.00000000,-1.00000000));
	MaterialFloat Local6 = min(max(Local5,0.00000000),1.00000000);
	MaterialFloat Local7 = (1.00000000 - Local6);
	MaterialFloat Local8 = PositiveClampedPow(Local7,Material.ScalarExpressions[0].z);
	MaterialFloat Local9 = min(max(Local8,0.00000000),1.00000000);
	MaterialFloat3 Local10 = lerp(Local4,Material.VectorExpressions[4].rgb,MaterialFloat(Local9));
	MaterialFloat Local11 = dot(Parameters.CameraVector, Material.VectorExpressions[7].rgb);
	MaterialFloat Local12 = (Local11 - 1.00000000);
	MaterialFloat Local13 = abs(Local12);
	MaterialFloat Local14 = (Local13 * Material.ScalarExpressions[1].w);
	MaterialFloat Local15 = (1.00000000 - Local14);
	MaterialFloat Local16 = (Local15 * 1.00000000);
	MaterialFloat Local17 = min(max(Local16,0.00000000),1.00000000);
	MaterialFloat3 Local18 = (Local17 * Material.VectorExpressions[10].rgb);
	MaterialFloat3 Local19 = (Local10 + Local18);
	MaterialFloat Local20 = (View.GameTime * Material.ScalarExpressions[2].y);
	MaterialFloat Local21 = (Local20 * 0.00020000);
	MaterialFloat Local22 = (Local20 * 0.00000000);
	MaterialFloat2 Local23 = (MaterialFloat2(Local21,Local22) + Parameters.TexCoords[0].xy);
	MaterialFloat4 Local24 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1, Material_Texture2D_1Sampler,Local23,View.MaterialTextureMipBias));
	MaterialFloat Local25 = (Local20 * 0.00100000);
	MaterialFloat2 Local26 = (MaterialFloat2(Local25,Local22) + Parameters.TexCoords[0].xy);
	MaterialFloat4 Local27 = ProcessMaterialGreyscaleTextureLookup((Texture2DSampleBias(Material_Texture2D_2, Material_Texture2D_2Sampler,Local26,View.MaterialTextureMipBias)).r).rrrr;
	MaterialFloat Local28 = lerp(Local24.r,Local27.r,Local6);
	MaterialFloat3 Local29 = (GetWorldPosition(Parameters) - GetObjectWorldPosition(Parameters));
	MaterialFloat Local30 = (GetPrimitiveData(Parameters.PrimitiveId).ObjectWorldPositionAndRadius.w * -0.10000000);
	MaterialFloat3 Local31 = (Local29 / Local30);
	MaterialFloat Local32 = min(max(Local31.b,0.00000000),1.00000000);
	MaterialFloat Local33 = (1.00000000 - Local32);
	MaterialFloat Local34 = (Local33 * Material.ScalarExpressions[2].z);
	MaterialFloat Local35 = lerp(0.00000000,Local28,Local34);
	MaterialFloat2 Local36 = (Parameters.TexCoords[0].xy * 0.50000000);
	MaterialFloat4 Local37 = ProcessMaterialGreyscaleTextureLookup((Texture2DSampleBias(Material_Texture2D_2, Material_Texture2D_2Sampler,Local36,View.MaterialTextureMipBias)).r).rrrr;
	MaterialFloat Local38 = lerp(Material.ScalarExpressions[3].x,Material.ScalarExpressions[2].w,Local37.r);
	MaterialFloat Local39 = PositiveClampedPow(Local35,Local38);
	MaterialFloat3 Local40 = (Material.VectorExpressions[12].rgb * Local39);
	MaterialFloat Local41 = (Local13 * (1.00000000 / max(0.00001000,1.29999995)));
	MaterialFloat Local42 = (1.00000000 - Local41);
	MaterialFloat Local43 = (Local42 * 1.00000000);
	MaterialFloat Local44 = min(max(Local43,0.00000000),1.00000000);
	MaterialFloat Local45 = PositiveClampedPow(Local44,10.00000000);
	MaterialFloat Local46 = min(max(Local45,0.00000000),1.00000000);
	MaterialFloat3 Local47 = (Local46 * Material.VectorExpressions[9].rgb);
	MaterialFloat Local48 = (Local39 * Local39);
	MaterialFloat Local49 = (Local48 * 0.40000001);
	MaterialFloat3 Local50 = (Local47 * Local49);
	MaterialFloat3 Local51 = (Local40 + Local50);
	MaterialFloat Local52 = min(max(Local48,0.00000000),1.00000000);
	MaterialFloat3 Local53 = lerp(Local19,Local51,MaterialFloat(Local52));
	MaterialFloat3 Local54 = (Local53 * Material.VectorExpressions[14].rgb);
	MaterialFloat3 Local55 = (Local54 * 1.50000000);
	MaterialFloat3 Local56 = lerp(Local55,Material.VectorExpressions[15].rgb,MaterialFloat(Material.ScalarExpressions[3].y));

	PixelMaterialInputs.EmissiveColor = Local56;
	PixelMaterialInputs.Opacity = 1.00000000;
	PixelMaterialInputs.OpacityMask = 1.00000000;
	PixelMaterialInputs.BaseColor = MaterialFloat3(0.00000000,0.00000000,0.00000000);
	PixelMaterialInputs.Metallic = 0.00000000;
	PixelMaterialInputs.Specular = 0.50000000;
	PixelMaterialInputs.Roughness = 0.50000000;
	PixelMaterialInputs.Anisotropy = 0.00000000;
	PixelMaterialInputs.Tangent = MaterialFloat3(1.00000000,0.00000000,0.00000000);
	PixelMaterialInputs.Subsurface = 0;
	PixelMaterialInputs.AmbientOcclusion = 1.00000000;
	PixelMaterialInputs.Refraction = 0;
	PixelMaterialInputs.PixelDepthOffset = 0.00000000;
	PixelMaterialInputs.ShadingModel = 0;


#if MATERIAL_USES_ANISOTROPY
	Parameters.WorldTangent = CalculateAnisotropyTangent(Parameters, PixelMaterialInputs);
#else
	Parameters.WorldTangent = 0;
#endif
}

		void surf( Input In, inout SurfaceOutputStandard o )
		{
			InitializeExpressions();

			float4 Z4 = float4( 0, 0, 0, 0 );

			float3 UnrealWorldPos = float3( In.worldPos.x, In.worldPos.y, In.worldPos.z );
			
			float3 UnrealNormal = In.normal2;

			FMaterialPixelParameters Parameters;
			#if NUM_TEX_COORD_INTERPOLATORS > 0			
				Parameters.TexCoords[ 0 ] = float2( In.uv_MainTex.x, In.uv_MainTex.y );
			#endif
			#if NUM_TEX_COORD_INTERPOLATORS > 1
				Parameters.TexCoords[ 1 ] = float2( In.uv2_Material_Texture2D_0.x, 1.0 - In.uv2_Material_Texture2D_0.y );
			#endif
			#if NUM_TEX_COORD_INTERPOLATORS > 2
			for( int i = 2; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
			{
				Parameters.TexCoords[ i ] = float2( In.uv_MainTex.x, In.uv_MainTex.y );
			}
			#endif
			Parameters.VertexColor = In.color;
			Parameters.WorldNormal = UnrealNormal;
			Parameters.ReflectionVector = half3( 0, 0, 1 );
			//Parameters.CameraVector = normalize( _WorldSpaceCameraPos.xyz - UnrealWorldPos.xyz );
			Parameters.CameraVector = mul( ( float3x3 )unity_CameraToWorld, float3( 0, 0, 1 ) ) * -1;
			Parameters.LightVector = half3( 0, 0, 0 );
			Parameters.SvPosition = float4( In.screenPos.x, In.screenPos.y, 0, 0 );
			Parameters.ScreenPosition = float4( In.screenPos.x, In.screenPos.y, 0, 0 );

			Parameters.UnMirrored = 1;

			Parameters.TwoSidedSign = 1;
			

			float3 InWorldNormal = UnrealNormal;
			float4 InTangent = In.tangent;
			float4 tangentWorld = float4( UnityObjectToWorldDir( InTangent.xyz ), InTangent.w );
			tangentWorld.xyz = normalize( tangentWorld.xyz );
			float3x3 tangentToWorld = CreateTangentToWorldPerVertex( InWorldNormal, tangentWorld.xyz, tangentWorld.w );
			Parameters.TangentToWorld = tangentToWorld;

			//WorldAlignedTexturing in UE relies on the fact that coords there are 100x larger, prepare values for that
			//but watch out for any computation that might get skewed as a side effect
			UnrealWorldPos = UnrealWorldPos * 100;

			//Parameters.TangentToWorld = half3x3( float3( 1, 1, 1 ), float3( 1, 1, 1 ), UnrealNormal.xyz );
			Parameters.AbsoluteWorldPosition = UnrealWorldPos;
			Parameters.WorldPosition_CamRelative = UnrealWorldPos;
			Parameters.WorldPosition_NoOffsets = UnrealWorldPos;

			Parameters.WorldPosition_NoOffsets_CamRelative = Parameters.WorldPosition_CamRelative;
			Parameters.LightingPositionOffset = float3( 0, 0, 0 );

			Parameters.AOMaterialMask = 0;

			Parameters.Particle.RelativeTime = 0;
			Parameters.Particle.MotionBlurFade;
			Parameters.Particle.Random = 0;
			Parameters.Particle.Velocity = half4( 1, 1, 1, 1 );
			Parameters.Particle.Color = half4( 1, 1, 1, 1 );
			Parameters.Particle.TranslatedWorldPositionAndSize = float4( UnrealWorldPos, 0 );
			Parameters.Particle.MacroUV = half4(0,0,1,1);
			Parameters.Particle.DynamicParameter = half4(0,0,0,0);
			Parameters.Particle.LocalToWorld = float4x4( Z4, Z4, Z4, Z4 );
			Parameters.Particle.Size = float2(1,1);
			Parameters.TexCoordScalesParams = float2( 0, 0 );
			Parameters.PrimitiveId = 0;

			FPixelMaterialInputs PixelMaterialInputs = ( FPixelMaterialInputs)0;
			PixelMaterialInputs.Normal = float3( 0, 0, 1 );
			PixelMaterialInputs.ShadingModel = 0;

			//Extra
			View.GameTime = -_Time.y;// _Time is (t/20, t, t*2, t*3), run in reverse because it works better with ElementalDemo
			View.MaterialTextureMipBias = 0.0;
			View.TemporalAAParams = float2( 0, 0 );
			View.ViewRectMin = float2( 0, 0 );
			View.ViewSizeAndInvSize = View_BufferSizeAndInvSize;
			View.MaterialTextureDerivativeMultiply = 1.0f;
			for( int i2 = 0; i2 < 40; i2++ )
				View.PrimitiveSceneData[ i2 ] = float4( 0, 0, 0, 0 );
			ResolvedView.WorldCameraOrigin = _WorldSpaceCameraPos.xyz;
			ResolvedView.ScreenPositionScaleBias = float4( 1, 1, 0, 0 );
			ResolvedView.TranslatedWorldToView = unity_MatrixV;
			ResolvedView.TranslatedWorldToCameraView = unity_MatrixV;
			ResolvedView.ViewToTranslatedWorld = unity_MatrixInvV;
			ResolvedView.CameraViewToTranslatedWorld = unity_MatrixInvV;
			Primitive.WorldToLocal = unity_WorldToObject;
			Primitive.LocalToWorld = unity_ObjectToWorld;
			CalcPixelMaterialInputs( Parameters, PixelMaterialInputs );

			float4 DiffuseTexel = tex2D(_MainTex, In.uv_MainTex );

			//Debug
			//PixelMaterialInputs.BaseColor = Texture2DSample( Material_Texture2D_1, Material_Texture2D_1Sampler, Parameters.TexCoords[ 0 ].xy );

			o.Albedo = PixelMaterialInputs.BaseColor.rgb;
			o.Alpha = PixelMaterialInputs.Opacity;
			//if( PixelMaterialInputs.OpacityMask < 0.01 ) discard;

			o.Metallic = PixelMaterialInputs.Metallic;
			o.Smoothness = 1.0 - PixelMaterialInputs.Roughness;
			o.Normal = normalize( PixelMaterialInputs.Normal );
			o.Emission = PixelMaterialInputs.EmissiveColor.rgb;
		}
		ENDCG
	}
	Fallback "Diffuse"
}