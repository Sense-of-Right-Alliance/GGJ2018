using UnityEngine;
using System.Collections;

public class CRTEffect : MonoBehaviour
{
    public Shader shader = null;
    private Material material;
    public float Distortion = 0.1f;
    public float InputGamma = 2.4f;
    public float OutputGamma = 2.2f;
    public float TextureSize = 768f;

    public float FadeInTime = 0.5f;
    public float StartSize = 0.0f;

    private IEnumerator coroutine;

    private void Awake()
    {
        StartCoroutine(FadeIn(FadeInTime, StartSize, TextureSize));
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (material == null)
            material = new Material(shader);

        if (material != null)
        {
            material.SetFloat("_Distortion", Distortion);
            material.SetFloat("_InputGamma", InputGamma);
            material.SetFloat("_OutputGamma", OutputGamma);
            material.SetVector("_TextureSize", new Vector2(TextureSize, TextureSize));
            Graphics.Blit(sourceTexture, destTexture, material);
        }
        else
            Graphics.Blit(sourceTexture, destTexture);
    }

    IEnumerator FadeIn(float fadeTime, float startTextureSize, float endTextureSize)
    {
        float sizePerSecond = (endTextureSize - startTextureSize) / fadeTime;
        float elapsed = 0f;
        TextureSize = startTextureSize;
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            TextureSize += sizePerSecond * Time.deltaTime;
            yield return null;
        }
        TextureSize = endTextureSize;
    }
}