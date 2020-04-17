using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ImportantText : MonoBehaviour
{
    public float time;
    public Vector2 start;
    void Start(){
        Destroy(gameObject, time);
        GetComponent<Rigidbody2D>().velocity = start;
    }
    
}
