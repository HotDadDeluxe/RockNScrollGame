using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum GameState { MainMenu, LevelSelection, Controls, Intro, Playing, Outro, GameOver, LevelComplete }
    [SerializeField] public GameState CurrentState;
    [SerializeField] public int CurrentLevel {get; private set; } = 1; // Default to level 1
    public int MaxLevelUnlocked { get; private set; } = 1;
    [SerializeField] public bool isArtistic;

    public static event System.Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Debug.Log("GameManager awake, hash code: " + this.GetHashCode());
        if (Instance == null)
        {
            Debug.Log("Instance set to " + this.GetHashCode());
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
            
        }
        else
        {
            Destroy(this.gameObject);
            Debug.Log("Duplicate GameManager, destroying " + this.GetHashCode());
            return;
        }
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

    public void setArtistic(string collectedItems)
    {
        if (collectedItems == "coin")
        {
            isArtistic = false;
        }
        else if (collectedItems == "note")
        {
            isArtistic = true;
        }
        else
        {
            Debug.LogError("Invalid collected item: " + collectedItems);
            isArtistic = true;
            return;
        }
    }

    public void SetCurrentLevel(int level) => CurrentLevel = level;
    public void SetMaxLevel(int level)
    {
        if (level > MaxLevelUnlocked)
        {
            MaxLevelUnlocked = level;
        }
    }

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
        else if (CurrentLevel == 2 || CurrentLevel == 3)
        {
            if (isArtistic)
            {
                Debug.Log("Intro" + CurrentLevel + "Artistic Start!");
                SceneManager.LoadScene("Intro" + CurrentLevel + "Artistic");
                return;
            }
            else
            {
                Debug.Log("Intro" + CurrentLevel + "Corporate Start!");
                SceneManager.LoadScene("Intro" + CurrentLevel + "Corporate");
                return;
            }
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
}
