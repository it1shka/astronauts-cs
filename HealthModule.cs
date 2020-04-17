using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModule : MonoBehaviour
{
    public float maxHealth = 5f;
    [HideInInspector]public float health;
    public Animator anim;
    public SpriteRenderer spriteRend;
    public float affectTime = .1f;
    public Color affectColor;
    public bool isAffected = false;
    public float deathTime = 1;
    public float regenerationPerSecond = .1f;

    private bool isDead = false;
    private void Start()
    {
        isDead = false;
        health = maxHealth;
        if (!anim) anim = GetComponent<Animator>();
        if (!spriteRend) spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f) Death();
        else if(!isAffected) StartCoroutine(affect());
    }
    private void Update()
    {
        health += Time.deltaTime * regenerationPerSecond;
        health = Mathf.Clamp(health, 0, maxHealth);
    }
    private void Death()
    {
        if (isDead) return;
        isDead = true;
        KillCount();
        anim.SetTrigger("died");
        Destroy(gameObject, deathTime);
    }

    void KillCount()
    {
        if(gameObject.tag != "Player")
        {
            killcounter.kills++;
            Utils.CountKill();
        }
    }

    IEnumerator affect() {
        isAffected = true;
        var normalColor = spriteRend.color;
        spriteRend.color = affectColor;
        yield return new WaitForSeconds(affectTime);
        spriteRend.color = normalColor;
        isAffected = false;
        yield break;
    }
}
