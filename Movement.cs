using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    public Transform tf;
    public Rigidbody2D rb;
    public Animator anim;

    public float speedX;

    public float maxJetpack = 10;
    [HideInInspector]public float currentJetpack;
    public float decreaseJetpack = 1.5f;
    public float increaseJetpack = 1f;
    private bool isFlying = false;
    public float minJectpackToFly = 1.5f;
    public float flyVelocity = 15f;
    public Transform jetPoint;
    public GameObject flyDust;

    public float ySensivity = .5f;

    public GameObject weapon;
    public Weapon weapScr;
    public float shootSensivity = .9f;
    [HideInInspector, Range(0,1)] public Vector2 input;
    [HideInInspector, Range(0,1)] public Vector2 inputShoot;

    public Transform weaponCenter;
    public Transform weaponCheckerPoint;
    public float checkRad = 1f;
    public LayerMask weaponMask = 9;
    public Transform viewObject; //viewPoint for Player and Player for Enemy
    public bool isFriendly = true;
    void Start()
    {
        currentJetpack = maxJetpack;
        if (!tf) tf = GetComponent<Transform>();
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!anim) anim = GetComponent<Animator>();
        //if (!weapScr) weapScr = weapon.GetComponent<Weapon>();
    }

    void Update()
    {
        if (inputShoot.x > 0) tf.rotation = Quaternion.Euler(0f, 0f, 0f);
        else if (inputShoot.x < 0) tf.rotation = Quaternion.Euler(0f, 180f, 0f);
        if(weapScr != null)
        weapScr.isShooting = (Vector2.Distance(Vector2.zero, inputShoot) > shootSensivity) ? true : false;

        rb.velocity = new Vector2(input.x * speedX, rb.velocity.y);
        if (input.y > ySensivity)
        {
            if (currentJetpack > ((isFlying) ? 0f : minJectpackToFly))
            {
                isFlying = true;
                rb.velocity = new Vector2(rb.velocity.x, flyVelocity);
                currentJetpack -= Time.deltaTime * decreaseJetpack;
                Instantiate(flyDust, jetPoint.position, Quaternion.identity);
            }
            else isFlying = false;
        }
        else isFlying = false;
        if (!isFlying) currentJetpack += Time.deltaTime * increaseJetpack;
        currentJetpack = Mathf.Clamp(currentJetpack, 0f, maxJetpack);
        anim.SetFloat("inputX", Mathf.Abs(input.x));
        anim.SetFloat("inputY", (input.y > ySensivity) ? input.y : 0f);
    }


    public void PickUpNearestWeapon()
    {
        var nearest = TakeNearest();
        if (nearest)
        {
            Destroy(weapon);
            weapon = nearest;
            weapScr = nearest.GetComponent<Weapon>();
            weapScr.isFriendly = this.isFriendly;
            weapScr.Activate();
            weapon.transform.SetParent(weaponCenter);
            weapScr.targetPosition = viewObject;
            weapon.transform.localPosition = new Vector2(0, 0);

            Utils.Throw(weapScr.weaponName, weaponCenter.position, weapScr.type);
        }
    }

    private GameObject TakeNearest()
    {
        var weaps = Physics2D.OverlapCircleAll(weaponCheckerPoint.position, checkRad, weaponMask);
        GameObject willReturn = null;
        var minDist = Mathf.Infinity;
        foreach(var weap in weaps) {
            var currentDist = Vector2.Distance(weap.transform.position, tf.position);
            if (currentDist < minDist) {
                willReturn = weap.gameObject;
                minDist = currentDist;
            }
        }
        return willReturn;
    }
    
}
