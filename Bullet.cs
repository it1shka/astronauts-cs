using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float spread = 5f;
    public float minDamage = 0.05f, maxDamage = 0.15f;
    public float flyTime = 1f;
    public bool isFriendly = true;
    private void Start()
    {
        transform.Rotate(new Vector3(0f,0f, Random.Range(-spread / 2, spread / 2)));
        var rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        Invoke("OnKill", flyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ((isFriendly) ? "Player" : "Enemy")) return;

        var healthmod = collision.gameObject.GetComponent<HealthModule>();
        if (healthmod)
        {
            var damage = Random.Range(minDamage, maxDamage);
            healthmod.TakeDamage(damage);
            Utils.Throw((Mathf.FloorToInt(damage * 100)).ToString(), new Vector2(
                collision.transform.position.x + Random.Range(-1f,1f),
                collision.transform.position.y + Random.Range(-1f,1f)
                ), 
                (collision.transform.tag == "Player") ? Utils.Type.MYTHICAL : Utils.Type.EPIC);
        }

        OnKill();
    }

    private void OnKill()
    {
        Destroy(gameObject);
    }

}
