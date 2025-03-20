using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // Add this for scene management
using UnityEngine.UI;  // Add this for UI elements
using TMPro;
using UnityEngine.SocialPlatforms.Impl;  // Add this for TextMeshPro

public class HEALTH_V2 : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 3;
    public bool isInvincible = false;  // To track if the player is invincible

    private SpriteRenderer spriteRenderer;


    //this is new
    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Take damage and apply invincibility
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
                HandlePlayerDeath();  // Start coroutine to handle death
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
        Debug.Log("Invincibility ON!");  // Log when invincibility starts

        yield return new WaitForSeconds(duration);

        isInvincible = false;
        Debug.Log("Invincibility OFF!");  // Log when invincibility ends
    }

    // Coroutine to handle player death (pause, wait, reload)
    private void HandlePlayerDeath()
    {
        Debug.Log("Player died!");
        LevelManager.Instance.killPlayer();
    }

    void Update()
    {
        //LevelManager.Instance.UpdateHealthUI(currentHealth);  // Update the level time
        //LevelManager.Instance.UpdateScoreUI();
        //GameManager.Instance.UpdateHealthUI(currentHealth);  // Update the level time
    }
}
