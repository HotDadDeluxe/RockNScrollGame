using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public HEALTH_V2 pHealth;    // Reference to the player's health script
    public int damage = 1;       // Damage the projectile will deal

    // Adjustable knockback force in the X and Y directions
    public float knockbackForceX = 5f;
    public float knockbackForceY = 10f;

    // Projectile speed and direction
    public float projectileSpeed = 10f;
    public Vector2 direction = Vector2.left;  // Default direction is left

    private bool hasCollided = false;   // To prevent multiple collisions from affecting the projectile

    // Start is called before the first frame update
    void Awake()
    {
        pHealth = FindFirstObjectByType<HEALTH_V2>();
    }

    void Start()
    {
        // Destroy the projectile after 4 seconds if it hasn't collided
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile in the specified direction
        if (!hasCollided)
        {
            transform.Translate(direction * projectileSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the projectile collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage to the player
            LevelManager.Instance.TakeDamage(damage);

            // Get the player's Rigidbody2D to apply the knockback force
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Apply knockback force to the player: upward and to the left
                Vector2 knockbackDirection = new Vector2(-1, 1).normalized; // Left and Up
                playerRb.velocity = new Vector2(knockbackDirection.x * knockbackForceX, knockbackDirection.y * knockbackForceY);

                // Disable normal movement during knockback for a short time
                StartCoroutine(DisableMovementDuringKnockback(collision.gameObject, 0.2f)); // 0.2 seconds of knockback
            }

            // Mark that the projectile has collided and should disappear
            hasCollided = true;
            Destroy(gameObject);  // Destroy the projectile immediately after collision
        }
    }

    // Coroutine to disable normal movement during knockback
    private IEnumerator DisableMovementDuringKnockback(GameObject player, float duration)
    {
        PlayerMove playerMove = player.GetComponent<PlayerMove>();
        if (playerMove != null)
        {
            playerMove.moveSpeed = 0f; // Stop horizontal movement during knockback
        }

        // Wait for the knockback duration to finish
        yield return new WaitForSeconds(duration);

        // Re-enable the player's normal movement after knockback
        if (playerMove != null)
        {
            playerMove.moveSpeed = 5f; // Reset to default speed or whatever value you want
        }
    }
}