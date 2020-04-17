using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class levelpicker : MonoBehaviour
{
    public LevelInfo[] levels;
    [SerializeField] private int currentLevel;
    public TextMeshProUGUI levelname, cost;
    public GameObject buyButton, playButton;
    private RawImage lastPreview;
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("currentlevel");
        UpdateLevels();
    }
    public void Right()
    {
        currentLevel++;
        if (currentLevel >= levels.Length) currentLevel = 0;
        PlayerPrefs.SetInt("currentlevel", currentLevel);
        PlayerPrefs.Save();
        UpdateLevels();
    }
    public void Left()
    {
        currentLevel--;
        if (currentLevel < 0) currentLevel = levels.Length - 1;
        PlayerPrefs.SetInt("currentlevel", currentLevel);
        PlayerPrefs.Save();
        UpdateLevels();
    }

    public void Buy()
    {
        var cur = levels[currentLevel];
        var currentKills = PlayerPrefs.GetInt("kills");
        if (currentKills < cur.cost) return;
        PlayerPrefs.SetInt("kills", currentKills - cur.cost);
        PlayerPrefs.SetInt(cur.name, 1);
        PlayerPrefs.Save();
        UpdateLevels();
    }
    public void Play()
    {
        Application.LoadLevel(levels[currentLevel].name);
    }
    void UpdateLevels()
    {
        var cur = levels[currentLevel];
        levelname.text = cur.name;
        if(lastPreview)lastPreview.enabled = false;
        lastPreview = cur.preview;
        lastPreview.enabled = true;
        if(PlayerPrefs.GetInt(cur.name) == 1 || cur.cost == 0) //bought
        {
            buyButton.SetActive(false);
            playButton.SetActive(true);
            cost.text = "";
            lastPreview.color = Color.white;
        }
        else //not bought
        {
            buyButton.SetActive(true);
            playButton.SetActive(false);
            cost.text = cur.cost.ToString() + " kills";
            lastPreview.color = Color.grey;
        }
    }

    
}

[System.Serializable]public class LevelInfo
{
    public string name;
    public RawImage preview;
    public int cost;
}