using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Instruction : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public GameObject[] buttons;
    private int currentButton = 0;
    public float timeBtw = .02f;
    public GameObject main;
    void Start() { currentButton = 0; }
    IEnumerator showText(string message){
        main?.SetActive(false);
        foreach(var elem in message) {
            textMesh.text += elem;
            yield return new WaitForSeconds(timeBtw);
        }
        buttons[currentButton]?.SetActive(true);
        currentButton++;
        yield break;
    }
    public void showTextF(string message) {
        StartCoroutine(showText(message));
    }
}
