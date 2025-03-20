using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum GameState { MainMenu, LevelSelection, Controls, Intro, Playing, Outro, GameOver, LevelComplete }
    public GameState CurrentState { get; private set; }
    public int CurrentLevel { get; private set; } = 1;
    public int MaxLevelUnlocked { get; private set; } = 1;
    public bool isArtistic = false;

    public static event System.Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Debug.Log("GameManager Awake");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        //SetState(GameState.MainMenu);
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case GameState.MainMenu:
                handleMainMenu();
                break;
            case GameState.Controls:
                handleControls();
                break;
            case GameState.LevelSelection:
                handleLevelSelection();
                break;
            case GameState.Intro:
                handleIntro();
                break;
            case GameState.Outro:
                handleOutro();
                break;
            case GameState.Playing:
                handlePlaying();
                break;
            case GameState.GameOver:
                handleGameOver();
                break;
            case GameState.LevelComplete:
                handleLevelComplete();
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }

    public GameState getState() => CurrentState;

    public void SetCurrentLevel(int level) => CurrentLevel = level;

    public void handleMainMenu()
    {
        Debug.Log("Main Menu!");
        SceneManager.LoadScene("MainMenuScene");
    }

    public void handleControls()
    {
        Debug.Log("Controls!");
        SceneManager.LoadScene("ControlsScene");
    }

    public void handleLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
        Debug.Log("Level Selection!");
    }

    public void handleIntro()
    {
        Time.timeScale = 1f;
        Debug.Log("handleIntro() called with level: " + CurrentLevel);
        if (CurrentLevel == 1)
        {
            Debug.Log("Cutscene1Start!");
            SceneManager.LoadScene("Intro1");
        }
        else if (CurrentLevel == 2)
        {
            if (isArtistic)
            {
                Debug.Log("Intro2Artistic Start!");
                SceneManager.LoadScene("Intro2Artistic");
                return;
            }
            else
            {
                Debug.Log("Intro2Corporate Start!");
                SceneManager.LoadScene("Intro2Corporate");
                return;
            }
        }
        else if (CurrentLevel == 3)
        {
            SceneManager.LoadScene("Intro3Start");
            Debug.Log("Cutscene3Start!");
        }
    }

    public void handleOutro()
    {
        Time.timeScale = 1f;
        Debug.Log("handling Outro!");
        if (isArtistic)
        {
            Debug.Log("Outro" + CurrentLevel + "Artistic Start!");
            SceneManager.LoadScene("Outro" + CurrentLevel + "Artistic");
            return;
        }
        else
        {
            Debug.Log("Outro" + CurrentLevel + "Corporate Start!");
            SceneManager.LoadScene("Outro" + CurrentLevel + "Corporate");
            return;
        }
    }

    public void handlePlaying()
    {
        if (LevelManager.Instance != null)
        {
            Destroy(LevelManager.Instance.gameObject);
        }
        Time.timeScale = 1f; 
        Debug.Log("Loading Level " + CurrentLevel + "!");
        SceneManager.LoadScene("Level" + CurrentLevel);
    }

    public void handleGameOver()
    {
        Debug.Log("Game Over!");
    }

    public void handleLevelComplete()
    {
        MaxLevelUnlocked = Mathf.Max(MaxLevelUnlocked, CurrentLevel + 1);
        Time.timeScale = 0f; // Pause the game
        Debug.Log("Level Complete!");
    }

    public void setRoute(String route)
    {
        if (route == "artistic")
        {
            isArtistic = true;
        }
        else if (route == "realistic")
        {
            isArtistic = false;
        }
    }

    //public void UpdateHealthUI(int n)
    //{
    //    LevelManager.Instance.UpdateHealthUI(n);
    //}
}
