using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public HEALTH_V2 pHealth;
    public int damage = 1;

    // Adjustable knockback force in the X and Y directions
    public float knockbackForceX = 5f;
    public float knockbackForceY = 10f;

    private bool isKnockedBack = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage to the player
            pHealth.TakeDamage(damage);

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
