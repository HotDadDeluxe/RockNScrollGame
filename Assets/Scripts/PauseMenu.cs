using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool isPaused = false;
    // Start is called before the first frame update
    // void Start()
    // {
    //     pauseMenu.SetActive(false);
    //     if (pauseMenu == null)
    //     {
    //         Debug.LogError("Pause Menu UI is not assigned in the inspector!");
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Pause Menu UI is not assigned in the inspector!");
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    void pauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    void resumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void gotoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    void quitGame()
    {
        Application.Quit();
    }
}
