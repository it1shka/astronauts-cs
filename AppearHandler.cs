using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearHandler : MonoBehaviour
{
    public SpriteRenderer spriteRend;
    public Material spriteMat;

    [Range(0,1)]public float fade = 1f;
    public float noiseSize = 10f;
    public Color color = new Color(191, 21, 0, 255);
    [Range(0, 1)]public float edgeSize = .06f;


    void Start()
    {
        if (!spriteRend) spriteRend = GetComponent<SpriteRenderer>();
        if (!spriteMat) spriteMat = spriteRend.material;
    }

    void Update()
    {
        spriteMat.SetFloat("_Fade", fade);
        spriteMat.SetFloat("NoiseSize", noiseSize);
        spriteMat.SetColor("_Color", color);
        spriteMat.SetFloat("_EdgeSize", edgeSize);
    }
}
