using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public Movement movement;
    public HealthModule healthModule;
    public Slider jectpack;
    public Slider health;
    public Slider subHealth;
    [Range(0,1)]public float subhealthSliderSpeed = .5f;
    public Slider bullets;

    void Start()
    {
        if (!movement) movement = GetComponent<Movement>();
        if (!healthModule) healthModule = GetComponent<HealthModule>();
    }

    void Update()
    {
        var inp1 = new Vector2(CnInputManager.GetAxis("Horizontal"),
            CnInputManager.GetAxis("Vertical"));
        var inp2 = new Vector2(CnInputManager.GetAxis("ShootX"),
            CnInputManager.GetAxis("ShootY"));
        movement.input = inp1;
        movement.inputShoot = inp2;

        jectpack.value = movement.currentJetpack / movement.maxJetpack;
        health.value = healthModule.health / healthModule.maxHealth;
        subHealth.value = Mathf.Lerp(subHealth.value, healthModule.health / healthModule.maxHealth, subhealthSliderSpeed);
        bullets.value = (movement.weapScr) ? (float)movement.weapScr.currentAmmo / movement.weapScr.maxAmmo : 0f;

        if (CnInputManager.GetButtonDown("Jump")){
            movement.PickUpNearestWeapon();
        }
        
    }

    // fix this
    /*private void OnDestroy()
    {
        isDead = true;
        var scr = FindObjectOfType<Buttons>();
        scr.Pause();
    }*/


}
