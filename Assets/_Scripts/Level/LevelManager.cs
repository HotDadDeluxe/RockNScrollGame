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
     //public static LevelUIManager UIManager;
    public static LevelManager Instance { get; private set; }
    //public LevelManager Instance; // Singleton instance of LevelManager

    //public static LevelUIManager UIManager { get; private set; }
    private Boolean isPaused = false;
    private Boolean isGameOver = false;
    public int currentHealth;
    public int maxHealth = 3;
    public bool isInvincible = false;
    [SerializeField] private int level;

    //public static LevelUIManager Instance { get; private set; }
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private TextMeshProUGUI coinText;  // Text to display the score
    [SerializeField] private TextMeshProUGUI noteText;
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private GameObject artisticMessage;
    [SerializeField] private GameObject corporateMessage;
    [SerializeField] private GameObject deathScreenUI;
    [SerializeField] private GameObject pauseMenuUI;

    [SerializeField] private Dictionary<string, int> collectiblesCount;
    

    //damage animation set up
    //private PlayerMove playerMove;
    //private Animator animator;


    private void Start()
    {
        currentHealth = maxHealth;
        GameManager.Instance.SetCurrentLevel(level);
        GameManager.Instance.SetMaxLevel(level);
        collectiblesCount = new Dictionary<string, int>();
        //playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        //animator = playerMove.GetComponent<Animator>();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            collectiblesCount = new Dictionary<string, int>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Collect(string collectibleType)
    {
        if (collectiblesCount.ContainsKey(collectibleType))
        {
            collectiblesCount[collectibleType]++;
        }
        else
        {
            collectiblesCount[collectibleType] = 1;
        }
    }

    public int GetCollectibleCount(string collectibleType)
    {
        return collectiblesCount.ContainsKey(collectibleType) ? collectiblesCount[collectibleType] : 0;
    }

    public void SetRoute()
    {
        string mostCollectedItem = null;
        int maxCount = 0;

        foreach (var collectible in collectiblesCount)
        {
            if (collectible.Value > maxCount)
            {
                maxCount = collectible.Value;
                mostCollectedItem = collectible.Key;
            }
        }
        if (mostCollectedItem == null)
        {
            Debug.Log("No collectibles collected yet.");
            GameManager.Instance.setArtistic("coin");
        }
        else if (mostCollectedItem == "coin")
        {
            GameManager.Instance.setArtistic("coin");
        }
        else if (mostCollectedItem == "note")
        {
            GameManager.Instance.setArtistic("note");
        }
        else
        {
            Debug.Log("No valid route found, defaulting to coin.");
            GameManager.Instance.setArtistic("coin");
        }
    }

    public void TakeDamage(int amount)
    {
        if (!isInvincible)
        {
            currentHealth -= amount;

            // Clamp health to ensure it doesn't go below 0
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            // Log when damage is taken
            Debug.Log("Player took damage! Current health: " + currentHealth);

            // Activate invincibility if health goes below or equal to zero
            if (currentHealth <= 0)
            {
                killPlayer();  // Start coroutine to handle death
            }
            else
            {
                StartCoroutine(InvincibilityTimer(1f));  // 1 second of invincibility after damage
            }
        }
        else
        {
            Debug.Log("Play is invincible! No damage taken.");  // Log when invincibility prevents damage
        }
    }

    // Coroutine to handle invincibility
    private IEnumerator InvincibilityTimer(float duration)
    {
        isInvincible = true;
        //animator.SetBool("IsDamaged", true);
        Debug.Log("Invincibility ON!");  // Log when invincibility starts

        yield return new WaitForSeconds(duration);
        isInvincible = false;
        //animator.SetBool("IsDamaged", false);
        Debug.Log("Invincibility OFF!");  // Log when invincibility ends
    }

  

    void Update()
    {
        UpdateHealthUI();
        UpdateScoreUI();
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
        SetRoute();
        HidePauseMenuUI();
        ShowLevelCompleteUI();
        GameManager.Instance.SetState(GameManager.GameState.LevelComplete);
    }

    public void HideAllUI()
    {
        HideDeathScreenUI();
        HideLevelCompleteUI();
        HidePauseMenuUI();
    }


    public void UpdateHealthUI()
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
            coinText.text = "Coins: " + GetCollectibleCount("coin");
        }
        if (noteText != null)
        {
            noteText.text = "Notes: " + GetCollectibleCount("note");
        }
    }

    public void ShowLevelCompleteUI()
    {
        if (GameManager.Instance.isArtistic)
        {
            corporateMessage.SetActive(false);
            artisticMessage.SetActive(true);
        }
        else
        {
            artisticMessage.SetActive(false);
            corporateMessage.SetActive(true);
        }
        levelCompleteUI.SetActive(true);
    }


    public void HideLevelCompleteUI() => levelCompleteUI.SetActive(false);

    public void ShowPauseMenuUI() => pauseMenuUI.SetActive(true);

    public void HidePauseMenuUI() => pauseMenuUI.SetActive(false);

    public void ShowDeathScreenUI() => deathScreenUI.SetActive(true);

    public void HideDeathScreenUI() => deathScreenUI.SetActive(false);
}
