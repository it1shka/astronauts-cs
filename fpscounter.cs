using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class fpscounter : MonoBehaviour
{
    public TextMeshProUGUI textM;
    void Update()
    {
        textM.text = Mathf.RoundToInt(1 / Time.deltaTime).ToString() + " fps";
    }
}
