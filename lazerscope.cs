using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazerscope : MonoBehaviour
{
    public LayerMask layerMask;
    public LineRenderer lineRenderer;
    public GameObject scopeEffect;
    void Start()
    {
        if (!lineRenderer) lineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        var ray = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, layerMask);
        if (!ray) return;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, ray.point);
        if (scopeEffect) Instantiate(scopeEffect, ray.point, Quaternion.identity);
    }
}
