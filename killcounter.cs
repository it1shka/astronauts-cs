using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class killcounter : MonoBehaviour
{
    public static int kills = 0;
    public TextMeshProUGUI textM;
    private void Start()
    {
        kills = 0;
    }
    void Update()
    {
        textM.text = kills.ToString() + " kills";
    }
}
