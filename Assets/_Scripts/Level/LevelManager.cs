using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    // public static LevelManager Instance;
    // public static LevelUIManager UIManager;
    public static LevelManager Instance { get; private set; }
    private Boolean isPaused = false;
    private Boolean isGameOver = false;
    void Start()
    {

    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //     }
    //     if (UIManager == null)
    //     {
    //         UIManager = FindObjectOfType<LevelUIManager>();
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            if (!isPaused)
            {
                Debug.Log("Game is not paused, pausing now");
                pauseGame();
            }
            else
            {
                Debug.Log("Game is paused, resuming now");
                resumeGame();
            }
        }
    }

    public void loadEndCutscene()
    {
        GameManager.Instance.SetState(GameManager.GameState.CutsceneEnd);
    }

    public void reloadLevel()
    {
        GameManager.Instance.SetState(GameManager.GameState.CutsceneStart);
    }

    public void gotoMainMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.SetState(GameManager.GameState.MainMenu);
    }

    public void pauseGame()
    {
        if (isGameOver)
        {
            return;
        }
        Time.timeScale = 0f;
        isPaused = true;
        LevelUIManager.Instance.ShowPauseMenuUI();
    }

    public void resumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        LevelUIManager.Instance.HidePauseMenuUI();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void killPlayer()
    {
        Time.timeScale = 0f;
        isGameOver = true;
        GameManager.Instance.SetState(GameManager.GameState.GameOver);
        LevelUIManager.Instance.ShowDeathScreenUI();
    }

    public void forceEndLevel()
    {
        Time.timeScale = 0f;
        GameManager.Instance.SetState(GameManager.GameState.LevelComplete);
        LevelUIManager.Instance.ShowLevelCompleteUI();
    }

    public void UpdateHealthUI(int currentHealth)
    {
        LevelUIManager.Instance.UpdateHealthUI(currentHealth);
    }

    public void UpdateScoreUI()
    {
        LevelUIManager.Instance.UpdateScoreUI();
    }
}
