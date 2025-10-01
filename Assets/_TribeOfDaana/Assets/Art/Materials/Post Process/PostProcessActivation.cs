using UnityEngine;

public class PostProcessActivation : MonoBehaviour
{
    public Material PostProcessMaterial;
    
    public bool usePostProcessing = false;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (usePostProcessing && PostProcessMaterial != null)
        {
            Graphics.Blit(source, destination, PostProcessMaterial);
        }
    }
    
}
