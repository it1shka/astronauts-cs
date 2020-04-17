using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Utils : MonoBehaviour
{
    public GameObject textMeshPro;
    private static GameObject tmp;
    public enum Type { 
        COMMON,
        UNCOMMON,
        RARE,
        EPIC,
        LEGENDARY,
        MYTHICAL
    };
    private static Dictionary<Type, Color> typeToColor = new Dictionary<Type, Color>() {
        { Type.COMMON,  Color.grey },
        { Type.UNCOMMON, Color.blue},
        { Type.RARE,    Color.green},
        {Type.EPIC, Color.cyan },
        { Type.LEGENDARY, Color.magenta},
        {Type.MYTHICAL, Color.red }
    };

    void Start() {
        tmp = textMeshPro;
    }
    public static void Throw(string textParam, Vector2 worldPosition, Type typeOfThing) {
        var newt = Instantiate(tmp, worldPosition, Quaternion.identity);
        var textmeshComp = newt.GetComponent<TextMeshPro>();
        textmeshComp.text = textParam;
        textmeshComp.color = typeToColor[typeOfThing];
    }
    
    public static void CountKill() {
        PlayerPrefs.SetInt("kills", PlayerPrefs.GetInt("kills")+1);
        PlayerPrefs.Save();
    }

}
