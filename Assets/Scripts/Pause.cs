using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool isPaused = false;
    public bool isCompleted = false;

    public GameObject pauseUI;
    public GameObject UI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        if (isPaused && !isCompleted)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            UI.SetActive(false);
            pauseUI.SetActive(true);
        }
        else if(!isCompleted)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            UI.SetActive(true);
            pauseUI.SetActive(false);
        }
    }
}
