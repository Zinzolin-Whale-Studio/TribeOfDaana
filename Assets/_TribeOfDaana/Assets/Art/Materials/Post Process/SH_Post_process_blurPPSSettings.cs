// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( SH_Post_process_blurPPSRenderer ), PostProcessEvent.AfterStack, "SH_Post_process_blur", true )]
public sealed class SH_Post_process_blurPPSSettings : PostProcessEffectSettings
{
}

public sealed class SH_Post_process_blurPPSRenderer : PostProcessEffectRenderer<SH_Post_process_blurPPSSettings>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "SH_Post_process_blur" ) );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
