using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class totalkills : MonoBehaviour
{
    public TextMeshProUGUI textM;
    //void Start()
    void Update()
    {
        textM.text = "Total kills: " + PlayerPrefs.GetInt("kills").ToString();
    }

}
