using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitingfordeath : MonoBehaviour
{
    public GameObject player;
    public Buttons scr;
    public GameObject continueButton;
    void Update()
    {
        if (!player) GameOver();
    }
    void GameOver()
    {
        scr.Pause();
        Destroy(continueButton);
        Destroy(this);
    }
}
