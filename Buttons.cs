using UnityEngine;
public class Buttons : MonoBehaviour
{
    public GameObject mainGUI, pauseGUI;
    public void Retry() {
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevel);
    }
    public void Pause(){
        Time.timeScale = 0;
        mainGUI.SetActive(false);
        pauseGUI.SetActive(true);
    }
    public void Resume(){
        Time.timeScale = 1;
        mainGUI.SetActive(true);
        pauseGUI.SetActive(false);
    }
    public void Menu(){
        Time.timeScale = 1;
        Application.LoadLevel("Menu");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ToGitHub()
    {
        Application.OpenURL("https://github.com/it1shka");
    }
    public void Tutorial()
    {
        Application.LoadLevel("Tutorial");
    }
}
