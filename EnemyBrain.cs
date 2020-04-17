using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public Movement movement;
    public HealthModule healthModule;
    public Healthbar healthbar;
    private Transform target;
    //check weapon
    public LayerMask checkWeap;
    public Transform head;

    public Transform checkGround;
    public float groundCheckDist = 1f;
    public LayerMask groundMask;

    private Transform player; //enemy
    public LayerMask playerMask; //enemyMask
    public float checkDist = 20f;

    public string enemyTag = "Player";

    void Start()
    {
        if (!movement) movement = GetComponent<Movement>();
        if (!healthModule) healthModule = GetComponent<HealthModule>();
        if (!healthbar) healthbar = GetComponentInChildren<Healthbar>();
    }

    void Update()
    {
        healthbar.value = healthModule.health / healthModule.maxHealth;
        player = SearchForNearestEnemy();
        if (!player) {
            JustChill();
            return;
        }
        var ray = Physics2D.Raycast(head.position, (player.position - head.position).normalized, checkDist, playerMask);
        var isVisible = ray && ray.transform.tag == enemyTag;
        RotateToTarget();
        if (!movement.weapScr || (movement.weapScr && movement.weapScr.currentAmmo == 0)){
            SearchingForWeapon();
        }
        else if (!isVisible) {
            ChasePlayer();
        }
        else{
            ShootInPlayer();
        }
    }

    private void JustChill(){
        movement.input = Vector2.zero;
        if(movement.weapScr)
        movement.weapScr.isShooting = false;
    }

    private Transform SearchForNearestEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        Transform returnEnemy = null;
        var minDist = Mathf.Infinity;
        foreach(var elem in enemies)
        {
            var curDist = Vector2.Distance(head.position, elem.transform.position);
            if(curDist < minDist) {
                minDist = curDist;
                returnEnemy = elem.transform;
            }
        }
        return returnEnemy;
    }

    private Transform GetNearestWeapon(){
            var weaps = GameObject.FindGameObjectsWithTag("Weapon");
            Transform returnWeap = null;
            var minDist = Mathf.Infinity;
            foreach (var weap in weaps) {
                var isVisible = Physics2D.Raycast(head.position,
                    (weap.transform.position - head.position).normalized,
                    Mathf.Infinity,
                    checkWeap);
                if (isVisible.transform.tag == "Weapon" ) {
                    var dist = Vector2.Distance(head.position,
                        weap.transform.position);
                    if (dist < minDist) {
                        minDist = dist;
                        returnWeap = weap.transform;
                    }
                }
            }
            return returnWeap;
    }

    private void SearchingForWeapon()
    {
        target = GetNearestWeapon();
        if (!target) { JustChill(); return; }
        if (Vector2.Distance(movement.weaponCheckerPoint.position, target.position) < movement.checkRad)
            movement.PickUpNearestWeapon();
        else
            Step(target.position);
    }

    private void ChasePlayer() {
        target = player;
        movement.weapScr.isShooting = false;
        Step(player.position);
    }

    private void ShootInPlayer() {
        target = player;

        movement.input = Vector2.zero;
        movement.weapScr.isShooting = true;
    }

    private void RotateToTarget() {
        if (!target) return;
        movement.inputShoot = (target.position - movement.weaponCenter.position).normalized;
        if(movement.weapScr)
        movement.weapScr.targetPosition = target;
    }

    private void Step(Vector2 target) {
        var checkForGround = Physics2D.Raycast(checkGround.position, 
            checkGround.right,
            groundCheckDist,
            groundMask
            );
        if(checkForGround) {
            movement.input = new Vector2(0f,1f);
        }
        else {
            movement.input = new Vector2((target.x > head.position.x) ? 1f : -1f, 0f);
        }

    }


    //fix it
    /*private void OnDestroy()
    {
        FindObjectOfType<killcounter>().kills++;
        Utils.CountKill();
    }*/

}
