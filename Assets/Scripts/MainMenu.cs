using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject main;
    public GameObject HTP;
    public void Play()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void HowToPlay()
    {
        main.SetActive(false);
        HTP.SetActive(true);
    }
    public void Back()
    {
        main.SetActive(true);
        HTP.SetActive(false);
    }
}
