using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    // public static LevelManager Instance;
    // public static LevelUIManager UIManager;
    public static LevelManager Instance { get; private set; }
    //public static LevelUIManager UIManager { get; private set; }
    private Boolean isPaused = false;
    private Boolean isGameOver = false;

    //public static LevelUIManager Instance { get; private set; }
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private TextMeshProUGUI coinText;  // Text to display the score
    [SerializeField] private TextMeshProUGUI noteText;
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private GameObject deathScreenUI;
    [SerializeField] private GameObject pauseMenuUI;

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

    //public void loadEndCutscene()
    //{
    //    GameManager.Instance.SetState(GameManager.GameState.CutsceneEnd);
    //}

    public void LoadIntro()
    {
        Time.timeScale = 1f;
        GameManager.Instance.SetState(GameManager.GameState.Intro);
    }

    public void LoadOutro()
    {
        Time.timeScale = 1f;
        GameManager.Instance.SetState(GameManager.GameState.Outro);
    }

    public void Reloadlevel()
    {
        Time.timeScale = 1f;
        GameManager.Instance.SetState(GameManager.GameState.Playing);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("LoadMainMenu() called");
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
        ShowPauseMenuUI();
    }

    public void resumeGame()
    {
        isPaused = false;
        if (isGameOver)
        {
            return;
        }
        Time.timeScale = 1f;
        HidePauseMenuUI();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void killPlayer()
    {
        Time.timeScale = 0f;
        isPaused = true;
        isGameOver = true;
        HidePauseMenuUI();
        ShowDeathScreenUI();
        GameManager.Instance.SetState(GameManager.GameState.GameOver);
    }

    public void WinPlayer()
    {
        Time.timeScale = 0f;
        isPaused = true;
        isGameOver = true;
        HidePauseMenuUI();
        ShowLevelCompleteUI();
        //GameManager.Instance.SetState(GameManager.GameState.LevelComplete);
    }

    public void HideAllUI()
    {
        HideDeathScreenUI();
        HideLevelCompleteUI();
        HidePauseMenuUI();
    }


    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null) // Check if the heart image reference is valid
            {
                if (i < currentHealth)
                {
                    hearts[i].sprite = fullHeart;
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                }
            }
        }
    }

    public void UpdateScoreUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + PlayerCollectibles.Instance.GetCollectibleCount("coin");
        }
        if (noteText != null)
        {
            noteText.text = "Notes: " + PlayerCollectibles.Instance.GetCollectibleCount("note");
        }
    }

    public void ShowLevelCompleteUI() => levelCompleteUI.SetActive(true);

    public void HideLevelCompleteUI() => levelCompleteUI.SetActive(false);

    public void ShowPauseMenuUI() => pauseMenuUI.SetActive(true);

    public void HidePauseMenuUI() => pauseMenuUI.SetActive(false);

    public void ShowDeathScreenUI() => deathScreenUI.SetActive(true);

    public void HideDeathScreenUI() => deathScreenUI.SetActive(false);
}
