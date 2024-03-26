using UnityEngine;
[ExecuteInEditMode]
public class JordanPostProcess : MonoBehaviour
{
    public Material _material;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _material);
    }
}
