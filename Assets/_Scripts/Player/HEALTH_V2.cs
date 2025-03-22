using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEALTH_V2 : MonoBehaviour
{
    public int CURhealth;
    public int maxHealth = 3;

    public bool isInvincible = false;  // To track if the player is invincible

    private void Start()
    {
        CURhealth = maxHealth;
    }

    // Take damage and apply invincibility
    public void TakeDamage(int amount)
    {
        if (!isInvincible)
        {
            CURhealth -= amount;

            // Log when damage is taken
            Debug.Log("Player took damage! Current health: " + CURhealth);

            // Activate invincibility if health goes below or equal to zero
            if (CURhealth <= 0)
            {
                CURhealth = 0;
                Destroy(gameObject);  // Destroy the player object
            }
            else
            {
                StartCoroutine(InvincibilityTimer(1f));  // 1 second of invincibility after damage (adjust as needed)
            }
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

    void Update()
    {
        // You can track health here as needed
    }
}
