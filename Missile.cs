using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float accelPerSecond = 40;
    public float currentSpeed = -30;
    public float damage = 1;
    public float flyTime = 10;
    [Range(0,1)]public float rotationSpeed = 0.5f;
    public bool isFriendly = true;
    Transform tf;
    Transform target;
    public Transform dustPoint;
    public GameObject dust;
    void Start()
    {
        tf = GetComponent<Transform>();
        Invoke("OnKill", flyTime);
    }

    void Update()
    {
        var nearestObj = SearchForEnemy();
        target = (nearestObj) ? nearestObj.transform : null;
        TurnToTarget();
        currentSpeed += accelPerSecond * Time.deltaTime;
        tf.position += (target) ? (target.position - tf.position).normalized * currentSpeed * Time.deltaTime : tf.right.normalized * currentSpeed * Time.deltaTime;
        Instantiate(dust, dustPoint.position, Quaternion.identity);
    }

    private void TurnToTarget()
    {
        if (!target) return;
        var direction = (target.position - tf.position).normalized;
        var lookRotation = Quaternion.Euler(tf.rotation.x, tf.rotation.y, 
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        tf.rotation = Quaternion.Slerp(tf.rotation, lookRotation, rotationSpeed);
    }

    private GameObject SearchForEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag((isFriendly) ? "Enemy" : "Player");
        var minDist = Mathf.Infinity;
        GameObject retOblect = null;
        foreach(var obj in enemies)
        {
            var curDist = Vector2.Distance(tf.position, obj.transform.position);
            if(curDist < minDist)
            {
                minDist = curDist;
                retOblect = obj;
            }
        }
        return retOblect;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ((isFriendly) ? "Player" : "Enemy")) return;

        var healthmod = collision.gameObject.GetComponent<HealthModule>();
        if (healthmod)
        {
            healthmod.TakeDamage(damage);
            Utils.Throw((Mathf.FloorToInt(damage * 100)).ToString(), new Vector2(
                collision.transform.position.x + Random.Range(-1f, 1f),
                collision.transform.position.y + Random.Range(-1f, 1f)
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
