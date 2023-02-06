using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject PauseMenu;
    public static bool isGamePaused = false;

    private void Start()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                Debug.Log("durdu");
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
                isGamePaused = true;
            }

            else
            {
                Debug.Log("dewam");
                Continue();
            }
        }
    }

    public void Continue()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
