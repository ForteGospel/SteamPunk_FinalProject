Shader "Unreal/MI_Road"
{
	Properties
	{
		_MainTex("MainTex (RGB)", 2D) = "white" {}				Material_Texture2D_0( "None", 2D ) = "white" {}
		Material_Texture2D_1( "Normal", 2D ) = "white" {}
		Material_Texture2D_2( "Albedo", 2D ) = "white" {}
		Material_Texture2D_3( "None", 2D ) = "white" {}


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

		#define NUM_TEX_COORD_INTERPOLATORS 1
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
			uniform sampler2D    Material_Texture2D_3;
			uniform SamplerState Material_Texture2D_3Sampler;
			
		//};
		struct MaterialStruct
		{
			float4 VectorExpressions[6];
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
	Material.VectorExpressions[1] = float4(1.000000,-0.000000,-0.000000,-0.000000);//
	Material.VectorExpressions[2] = float4(0.000000,1.000000,1.000000,1.000000);//
	Material.VectorExpressions[3] = float4(0.000000,0.000000,0.000000,0.000000);//
	Material.VectorExpressions[4] = float4(1.000000,1.000000,1.000000,1.000000);//
	Material.VectorExpressions[5] = float4(1.000000,1.000000,1.000000,0.000000);//
	Material.ScalarExpressions[0] = float4(0.000000,0.000000,0.000000,0.000000);
	Material.ScalarExpressions[1] = float4(-0.000000,1.000000,0.000000,1.000000);
	Material.ScalarExpressions[2] = float4(0.000000,1.000000,2.000000,0.020000);
	Material.ScalarExpressions[3] = float4(1.000000,0.000000,1.000000,0.000000);
}void CalcPixelMaterialInputs(in out FMaterialPixelParameters Parameters, in out FPixelMaterialInputs PixelMaterialInputs)
{
	// Initial calculations (required for Normal)
	MaterialFloat2 Local0 = ((MaterialFloat2(0.50000000,-0.50000000) * -1.00000000) + Parameters.TexCoords[0].xy);
	MaterialFloat Local1 = dot(Local0, Material.VectorExpressions[1].rg);
	MaterialFloat Local2 = dot(Local0, Material.VectorExpressions[2].rg);
	MaterialFloat2 Local3 = (MaterialFloat2(0.50000000,-0.50000000) + MaterialFloat2(Local1,Local2));
	MaterialFloat Local4 = (Local3.r + Material.ScalarExpressions[1].z);
	MaterialFloat Local5 = (Local4 * Material.ScalarExpressions[1].w);
	MaterialFloat Local6 = (Local3.g + Material.ScalarExpressions[2].x);
	MaterialFloat Local7 = (Local6 * Material.ScalarExpressions[2].y);
	MaterialFloat2 Local8 = (MaterialFloat2(Local5,Local7) * Material.ScalarExpressions[2].z);
	MaterialFloat Local9 = (-0.50000000 * Material.ScalarExpressions[2].w);
	MaterialFloat4 Local10 = Texture2DSampleBias(Material_Texture2D_0, Material_Texture2D_0Sampler,Parameters.TexCoords[0].xy,View.MaterialTextureMipBias);
	MaterialFloat Local11 = (Material.ScalarExpressions[2].w * Local10.g);
	MaterialFloat Local12 = (Local11 + Local9);
	MaterialFloat3 Local13 = mul((MaterialFloat3x3)(Parameters.TangentToWorld), Parameters.CameraVector);
	MaterialFloat2 Local14 = (Local13.rg * Local12);
	MaterialFloat2 Local15 = (Local14 + Local8);
	MaterialFloat4 Local16 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_1, Material_Texture2D_1Sampler,Local15,View.MaterialTextureMipBias));
	MaterialFloat2 Local17 = (Material.ScalarExpressions[3].x * MaterialFloat2(Local16.rgb.r,Local16.rgb.g));

	// The Normal is a special case as it might have its own expressions and also be used to calculate other inputs, so perform the assignment here
	PixelMaterialInputs.Normal = MaterialFloat3(Local17,Local16.rgb.b);


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
	MaterialFloat3 Local18 = lerp(MaterialFloat3(0.00000000,0.00000000,0.00000000),Material.VectorExpressions[3].rgb,MaterialFloat(Material.ScalarExpressions[3].y));
	MaterialFloat4 Local19 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2, Material_Texture2D_2Sampler,Local8,View.MaterialTextureMipBias));
	MaterialFloat3 Local20 = (Material.VectorExpressions[5].rgb * Local19.rgb);
	MaterialFloat3 Local21 = (Material.ScalarExpressions[3].z * Local20);
	MaterialFloat4 Local22 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3, Material_Texture2D_3Sampler,Local8,View.MaterialTextureMipBias));

	PixelMaterialInputs.EmissiveColor = Local18;
	PixelMaterialInputs.Opacity = 1.00000000;
	PixelMaterialInputs.OpacityMask = 1.00000000;
	PixelMaterialInputs.BaseColor = Local21;
	PixelMaterialInputs.Metallic = Material.ScalarExpressions[3].w;
	PixelMaterialInputs.Specular = 0.50000000;
	PixelMaterialInputs.Roughness = Local22.rgb;
	PixelMaterialInputs.Anisotropy = 0.00000000;
	PixelMaterialInputs.Tangent = MaterialFloat3(1.00000000,0.00000000,0.00000000);
	PixelMaterialInputs.Subsurface = 0;
	PixelMaterialInputs.AmbientOcclusion = Local10.r;
	PixelMaterialInputs.Refraction = 0;
	PixelMaterialInputs.PixelDepthOffset = 0.00000000;
	PixelMaterialInputs.ShadingModel = 1;


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