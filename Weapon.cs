using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int maxAmmo = 180;
    [HideInInspector]public int currentAmmo;
    public GameObject friendlyAmmo;
    public GameObject notFriendlyAmmo;
    public Transform tf;
    public int bulletsPerShoot = 1;
    public float timeBtwShots = .25f;
    private float currentShootTime;
    //[Range(0, 1)] public float rotationSpeed = .5f;
    [HideInInspector] public bool isShooting = false;

    public Transform targetPosition;
    public Transform firePoint;

    public Utils.Type type = Utils.Type.COMMON;
    public string weaponName = "Auto";
    public bool isFriendly = true;

    public GameObject collider1;
    public Rigidbody2D rigidbody;
    public Collider2D collider2;
    void Start()
    {
        if (!tf) tf = GetComponent<Transform>();
        currentShootTime = timeBtwShots;
        currentAmmo = maxAmmo;
    }
    private void Update()
    {
        if (isShooting && currentShootTime <= 0f)
        {
            Shoot();
        }
        else currentShootTime -= Time.deltaTime;
        if (targetPosition && targetPosition.localPosition != Vector3.zero)
        {
            var direction = targetPosition.position - tf.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            tf.rotation = Quaternion.Euler(tf.rotation.x, tf.rotation.y, angle);
            /*
            var newQuat = new Quaternion(tf.rotation.x, tf.rotation.y, angle, tf.rotation.w);
            var begQuat = tf.rotation;
            tf.rotation = Quaternion.Lerp(begQuat, newQuat, rotationSpeed);*/
        }
    }

    private void Shoot()
    {
        if (currentAmmo <= 0) return;
        for(int i=0; i<bulletsPerShoot; i++)
        {
            if (currentAmmo <= 0) return;
            Instantiate((isFriendly) ? friendlyAmmo:notFriendlyAmmo, firePoint.position, firePoint.rotation);
            currentAmmo--;
        }
        currentShootTime = timeBtwShots;
    }

    public void Activate()
    {
        Destroy(collider1);
        Destroy(collider2);
        Destroy(rigidbody);
        gameObject.layer = LayerMask.NameToLayer("Default");
        gameObject.tag = "Untagged";
    }

}
