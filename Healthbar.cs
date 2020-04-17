using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public Transform fill;
    public Transform subfill;
    [Range(0,1)]public float subfillSpeed = .03f;
    [Range(0,1)]public float value = 1;
    void Update()
    {
        fill.localScale = new Vector3(value,
            fill.localScale.y,
            fill.localScale.z);
        subfill.localScale = new Vector3(
            Mathf.Lerp(subfill.localScale.x, value, subfillSpeed),
            subfill.localScale.y,
            subfill.localScale.z
            );
    }
}
