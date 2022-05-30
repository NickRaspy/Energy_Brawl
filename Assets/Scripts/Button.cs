using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject UI;
    public GameObject pauseUI;
    public Pause getPaused;
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Continue()
    {
        getPaused.isPaused = false;
    }
}
