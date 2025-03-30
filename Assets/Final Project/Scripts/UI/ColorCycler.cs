using UnityEngine;

public class ColorCycler : MonoBehaviour
{
    [SerializeField] private float colorChangeSpeed = 1f;
    [SerializeField] private Renderer targetRenderer;

    private void Update()
    {
        float t = Time.time * colorChangeSpeed;
        float r = Mathf.Sin(t) * 0.5f + 0.5f;
        float g = Mathf.Sin(t + 2f) * 0.5f + 0.5f;
        float b = Mathf.Sin(t + 4f) * 0.5f + 0.5f;
        Color color = new Color(r, g, b);

        if (targetRenderer != null)
        {
            targetRenderer.material.color = color;
            Color emissionColor = color * colorChangeSpeed;
            targetRenderer.material.SetColor("_EmissionColor", emissionColor);
        }
    }
}
