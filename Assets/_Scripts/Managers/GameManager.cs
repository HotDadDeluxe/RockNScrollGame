using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // public static UIManager uiManager;
    // public static UIManager uiManager;
    public enum GameState { MainMenu, LevelSelection, CutsceneStart, CutsceneEnd, Playing, GameOver, LevelComplete }
    public GameState CurrentState { get; private set; }
    public int CurrentLevel { get; private set; } = 1;
    public int MaxLevelUnlocked { get; private set; } = 1;
    public bool isArtistic = true;

    public static event System.Action<GameState> OnGameStateChanged;

    // how do I make instances of other manager classes


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
        SetState(GameState.MainMenu);
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case GameState.MainMenu:
                handleMainMenu();
                break;
            case GameState.LevelSelection:
                handleLevelSelection();
                break;
            // case GameState.Loading:
            //     handleLoading();
            //     break;
            case GameState.CutsceneStart:
                handleCutsceneStart();
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
            case GameState.CutsceneEnd:
                handleCutsceneEnd();
                break;


            default:
                throw new System.ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }

    public GameState getState()
    {
        return CurrentState;
    }

    public void SetCurrentLevel(int level)
    {
        CurrentLevel = level;
    }

    public void handleMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
        Debug.Log("Main Menu!");
    }

    public void handleLevelSelection()
    {
        SceneManager.LoadScene("WorldMapScene");
        Debug.Log("Level Selection!");
    }

    public void handleGameOver()
    {
        Time.timeScale = 0f; // Pause the game
        LevelUIManager.Instance.ShowDeathScreenUI();
        Debug.Log("Game Over!");
    }

    public void handleCutsceneStart()
    {
        SceneManager.LoadScene("Cutscene" + CurrentLevel + "Start");
        Debug.Log("Cutscene" + CurrentLevel + "Start!");
    }

    public void handleCutsceneEnd()
    {
        SceneManager.LoadScene("Cutscene" + CurrentLevel + "End");
        Debug.Log("Cutscene" + CurrentLevel + "End!");
    }

    public void handlePlaying()
    {
        Time.timeScale = 1f; // Resume the game
        // UIManager.Instance.HideAllUI();
        Debug.Log("Loading Level " + CurrentLevel + "!");
        // LoadLoadingScreen("Level" + CurrentLevel);
        SceneManager.LoadScene("Level" + CurrentLevel);
        // Debug.Log("Playing Level " + CurrentLevel + "!");
    }

    public void handleLevelComplete()
    {
        MaxLevelUnlocked = Mathf.Max(MaxLevelUnlocked, CurrentLevel + 1);
        Time.timeScale = 0f; // Pause the game
        LevelUIManager.Instance.ShowLevelCompleteUI();
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
}
