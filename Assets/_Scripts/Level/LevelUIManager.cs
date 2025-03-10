using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.CompilerServices;
using System;
using TMPro;  // Add this for TextMeshPro

public class LevelUIManager : MonoBehaviour
{
    public static LevelUIManager Instance { get; private set; }
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private TextMeshProUGUI coinText;  // Text to display the score
    [SerializeField] private TextMeshProUGUI noteText;
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private GameObject deathScreenUI;
    [SerializeField] private GameObject pauseMenuUI;

    // public static bool isPaused = false;

    void Awake()
    {
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
        // HideAllUI();
    }

    public void HideAllUI()
    {
        HideDeathScreenUI();
        HideLevelCompleteUI();
        HidePauseMenuUI();
    }

    public void endLevel()
    {
        Time.timeScale = 1f;
        GameManager.Instance.SetState(GameManager.GameState.LevelComplete);
        SceneManager.LoadScene("LevelOneEndScene");
    }

    public void quitGame()
    {
        Application.Quit();
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

    public void ShowLevelCompleteUI()
    {
        levelCompleteUI.SetActive(true);
    }

    public void HideLevelCompleteUI()
    {
        levelCompleteUI.SetActive(false);
    }

    public void ShowPauseMenuUI()
    {
        Debug.Log("ShowPauseMenuUI called");
        pauseMenuUI.SetActive(true);
    }

    public void HidePauseMenuUI()
    {
        Debug.Log("HidePauseMenuUI called");
        pauseMenuUI.SetActive(false);
    }

    public void ShowDeathScreenUI()
    {
        Debug.Log("ShowDeathScreenUI called");
        deathScreenUI.SetActive(true);
    }

    public void HideDeathScreenUI()
    {
        Debug.Log("HideDeathScreenUI called");
        deathScreenUI.SetActive(false);
    }
}