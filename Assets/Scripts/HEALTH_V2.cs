using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // Add this for scene management
using UnityEngine.UI;  // Add this for UI elements
using TMPro;
using UnityEngine.SocialPlatforms.Impl;  // Add this for TextMeshPro

public class HEALTH_V2 : MonoBehaviour
{
    public int CURhealth;
    public int maxHealth = 3;

    public bool isInvincible = false;  // To track if the player is invincible

    //[Header("Health Sprites")]
    //[SerializeField] private Sprite smileSprite;   // Sprite for smile (3 health)
    //[SerializeField] private Sprite straightFaceSprite;  // Sprite for straight face (2 health)
    //[SerializeField] private Sprite frownSprite;   // Sprite for frown (1 health)
    //[SerializeField] private Sprite skullSprite;   // Sprite for skull (0 health)

    private SpriteRenderer spriteRenderer;

    [Header("UI Elements")]
    [SerializeField] private GameObject deathScreenUI;  // UI panel to show on death
    [SerializeField] private GameObject health1;  // Text to display
    [SerializeField] private GameObject health2;
    [SerializeField] private GameObject health3;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private TextMeshProUGUI coinText;  // Text to display the score
    [SerializeField] private TextMeshProUGUI noteText;
    [SerializeField] private Button resetButton;  // Button to reset the scene
    [SerializeField] private Button menuButton;  // Button to go back to the menu

    //this is new
    private void Start()
    {
        CURhealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();  // Get the SpriteRenderer attached to the player
        UpdateHealthUI();  // Ensure the correct sprite is set at the start
        UpdateScoreUI();
        //UpdateHeartUI();       // Update the heart UI at the start
    }

    // Take damage and apply invincibility
    public void TakeDamage(int amount)
    {
        if (!isInvincible)
        {
            CURhealth -= amount;

            // Clamp health to ensure it doesn't go below 0
            CURhealth = Mathf.Clamp(CURhealth, 0, maxHealth);

            // Log when damage is taken
            Debug.Log("Player took damage! Current health: " + CURhealth);

            // Activate invincibility if health goes below or equal to zero
            if (CURhealth <= 0)
            {
                StartCoroutine(HandlePlayerDeath());  // Start coroutine to handle death
            }
            else
            {
                StartCoroutine(InvincibilityTimer(1f));  // 1 second of invincibility after damage
            }

            UpdateHealthUI();  // Update the sprite after taking damage
            UpdateScoreUI();  // Update the heart UI when damage is taken
        }
    }

    // Coroutine to handle invincibility
    private IEnumerator InvincibilityTimer(float duration)
    {
        isInvincible = true;
        Debug.Log("Invincibility ON!");  // Log when invincibility starts

        yield return new WaitForSeconds(duration);

        isInvincible = false;
        Debug.Log("Invincibility OFF!");  // Log when invincibility ends
    }

    // Coroutine to handle player death (pause, wait, reload)
    private IEnumerator HandlePlayerDeath()
    {
        Debug.Log("Player died!");

        // Pause the game (stop time)
        Time.timeScale = 0f;

        if (deathScreenUI != null)
        {
            deathScreenUI.SetActive(true);
        }

        if (scoreUI != null)
        {
            int score = PlayerCollectibles.Instance.GetCollectibleCount("coin") + PlayerCollectibles.Instance.GetCollectibleCount("note");
            coinText.text = "Coin: " + PlayerCollectibles.Instance.GetCollectibleCount("coin");
            noteText.text = "Note: " + PlayerCollectibles.Instance.GetCollectibleCount("note");
        }

        yield return null;

        // // Wait for 3 seconds while time is paused
        // yield return new WaitForSecondsRealtime(3f);  // Use WaitForSecondsRealtime to ignore time scaling

        // // Reload the current scene
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // // Unpause the game (restore normal time scale)
        // Time.timeScale = 1f;
    }

    private void ResetScene()
    {
        Time.timeScale = 1f;  // Unpause the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to go back to the menu
    private void GoToMenu()
    {
        Time.timeScale = 1f;  // Unpause the game
        SceneManager.LoadScene("MenuScene");  // Replace with your menu scene name
    }

    // Update the health sprite based on the current health
    private void UpdateHealthUI()
    {
        health1.SetActive(CURhealth >= 1);
        health2.SetActive(CURhealth >= 2);
        health3.SetActive(CURhealth >= 3);
    }

    private void UpdateScoreUI()
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
    // Method to update the heart UI
    //private void UpdateHeartUI()
    //{
    //    for (int i = 0; i < heartImages.Length; i++)
    //    {
    //        if (i < CURhealth)
    //        {
    //            heartImages[i].sprite = fullHeart;
    //        }
    //        else
    //        {
    //            heartImages[i].sprite = emptyHeart;
    //        }
    //    }
    //}

    void Update()
    {
        UpdateScoreUI();
        // You can track health here as needed (currently handled in TakeDamage method)
    }
}
