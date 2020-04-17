using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using Cinemachine;
public class ViewPoint : MonoBehaviour
{
    public float distance = 5f;
    public float deltaSize = 5f;
    [Range(0, 1)]public float deltaSizeSpeed = .5f;
    public CinemachineVirtualCamera cinemachine;
    public SpriteRenderer spriteRenderer;
    public float basicCinemachineSize = 14f;

    private void Start()
    {
        if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        var inp = new Vector3(Mathf.Abs(CnInputManager.GetAxis("ShootX")), CnInputManager.GetAxis("ShootY"),0f);
        var endPos = inp * distance;
        var delta = Vector2.Distance((Vector2)inp, Vector2.zero);
        transform.localPosition = endPos;
        cinemachine.m_Lens.OrthographicSize = Mathf.Lerp(basicCinemachineSize + delta * deltaSize,
            cinemachine.m_Lens.OrthographicSize, deltaSizeSpeed);

        var c = spriteRenderer.color;
        spriteRenderer.color = new Color(c.r, c.g, c.b, delta);
        
    }
}
