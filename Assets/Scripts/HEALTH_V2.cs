using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // Add this for scene management

public class HEALTH_V2 : MonoBehaviour
{
    public int CURhealth;
    public int maxHealth = 3;

    public bool isInvincible = false;  // To track if the player is invincible

    [Header("Health Sprites")]
    [SerializeField] private Sprite smileSprite;   // Sprite for smile (3 health)
    [SerializeField] private Sprite straightFaceSprite;  // Sprite for straight face (2 health)
    [SerializeField] private Sprite frownSprite;   // Sprite for frown (1 health)
    [SerializeField] private Sprite skullSprite;   // Sprite for skull (0 health)

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        CURhealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();  // Get the SpriteRenderer attached to the player
        UpdateHealthSprite();  // Ensure the correct sprite is set at the start
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

            UpdateHealthSprite();  // Update the sprite after taking damage
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

        // Wait for 3 seconds while time is paused
        yield return new WaitForSecondsRealtime(3f);  // Use WaitForSecondsRealtime to ignore time scaling

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Unpause the game (restore normal time scale)
        Time.timeScale = 1f;
    }

    // Update the health sprite based on the current health
    private void UpdateHealthSprite()
    {
        if (CURhealth == 3)
        {
            spriteRenderer.sprite = smileSprite;  // Display smile for 3 health
        }
        else if (CURhealth == 2)
        {
            spriteRenderer.sprite = straightFaceSprite;  // Display straight face for 2 health
        }
        else if (CURhealth == 1)
        {
            spriteRenderer.sprite = frownSprite;  // Display frown for 1 health
        }
        else if (CURhealth <= 0)
        {
            spriteRenderer.sprite = skullSprite;  // Display skull for 0 health
        }
    }

    void Update()
    {
        // You can track health here as needed (currently handled in TakeDamage method)
    }
}
