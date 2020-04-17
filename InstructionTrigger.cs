using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionTrigger : MonoBehaviour
{
    public string message;
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != player) return;
        var scr = FindObjectOfType<Instruction>();
        scr.showTextF(message);
        Destroy(gameObject);
    }
}
