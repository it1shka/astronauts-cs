using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageAppearHandler : MonoBehaviour
{
    public RawImage image;
    public Material material;

    [Range(0, 1)] public float fade = 1f;
    public float noiseSize = 10f;
    public Color color = new Color(191, 21, 0, 255);
    [Range(0, 1)] public float edgeSize = .06f;
    void Start()
    {
        if (!image) image = GetComponent<RawImage>();
        if (!material) material = image.material;
    }

    void Update()
    {
        material.SetFloat("_Fade", fade);
        material.SetFloat("NoiseSize", noiseSize);
        material.SetColor("_Color", color);
        material.SetFloat("_EdgeSize", edgeSize);
    }
}
