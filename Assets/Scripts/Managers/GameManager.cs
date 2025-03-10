using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static UIManager uiManager;
    public enum GameState { MainMenu, LevelSelection, CutsceneStart, CutsceneEnd, Playing, Paused, GameOver, LevelComplete }
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
            case GameState.CutsceneStart:
                handleCutsceneStart();
                break;
            case GameState.CutsceneEnd:
                handleCutsceneEnd();
                break;
            case GameState.Playing:
                handlePlaying();
                break;
            case GameState.Paused:
                handlePaused();
                break;
            case GameState.GameOver:
                handleGameOver();
                break;
            case GameState.LevelComplete:
                handleLevelComplete();
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
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

    public void handleCutsceneStart()
    {
        SceneManager.LoadScene("CutScene" + CurrentLevel);

        Debug.Log("Cutscene Start!");
    }

    public void handleCutsceneEnd()
    {
        SceneManager.LoadScene("Level" + CurrentLevel);
        Debug.Log("Cutscene End!");
    }

    public void handlePlaying()
    {
        SceneManager.LoadScene("Level" + CurrentLevel);
    }

    public void handlePaused()
    {
        Debug.Log("Paused!");
    }

    public void handleGameOver()
    {
        Debug.Log("Game Over!");
    }

    public void handleLevelComplete()
    {
        MaxLevelUnlocked = Mathf.Max(MaxLevelUnlocked, CurrentLevel + 1);
        Time.timeScale = 0f; // Pause the game
        UIManager.Instance.ShowLevelCompleteUI();
        Debug.Log("Level Complete!");
    }
}
