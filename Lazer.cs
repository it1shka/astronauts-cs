using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public float minDamage, maxDamage;
    public bool isFriendly = true;
    LineRenderer lineRenderer;
    public LayerMask playerMask;
    public LayerMask enemyMask;
    public float distance = 40f;
    public float time = .5f;
    public float spread = 0;
    private void Awake()
    {
        transform.Rotate(new Vector3(0f, 0f, Random.Range(-spread / 2, spread / 2)));
    }
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(shoot());
    }

    private IEnumerator shoot() {
        var ray = Physics2D.Raycast(transform.position, transform.right, distance, (isFriendly) ? playerMask : enemyMask);
        if (ray){
            var scr = ray.transform.gameObject.GetComponent<HealthModule>();
            if (scr)
            {
                var damage = Random.Range(minDamage, maxDamage);
                scr.TakeDamage(damage);
                Utils.Throw(Mathf.FloorToInt(damage * 100).ToString(),
                    ray.point,
                    (isFriendly) ? Utils.Type.EPIC : Utils.Type.MYTHICAL);
            }
        }

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, (ray) ? (Vector3)ray.point : transform.position);
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
