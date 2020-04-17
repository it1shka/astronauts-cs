using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject[] guns;
    public float timeBtwSpawn = 20f;
    private float currentTime = 0f;
    public int spawnAmount = 10;
    public Transform leftBorder;
    public Transform rightBorder;
    void Update()
    {
        if (currentTime <= 0f)
            Spawn();
        else currentTime -= Time.deltaTime;
    }
    private void Spawn()
    {
        for(int i=0; i<spawnAmount; i++)
        {
            Instantiate(
                guns[Random.Range(0, guns.Length)],
                new Vector2(Random.Range(leftBorder.position.x, rightBorder.position.x), transform.position.y),
                Quaternion.identity
                );   
        }
        currentTime = timeBtwSpawn;
    }
}
