using System.Collections;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 2f;  // Speed multiplier set in the Inspector
    [SerializeField] private float restoreSpeed = 1.5f;    // Multiplier to restore speed after the boost
    private float originalSpeed;
    private bool isCoroutineRunning = false;
    private bool isInsideBoost = false;
    private PlayerMove playerMove;  // Reference to PlayerMove script

    void Awake()
    {
        // Ensure that the Player object is found early, in Awake
        playerMove = GameObject.FindWithTag("Player")?.GetComponent<PlayerMove>();

        if (playerMove == null)
        {
            Debug.LogError("PlayerMove script is not attached to the Player object or the Player object is missing the Player tag.");
        }
        else
        {
            originalSpeed = playerMove.moveSpeed;  // Store the original speed
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the speed boost area
        if (other.CompareTag("Player") && playerMove != null)
        {
            // Apply speed boost
            isInsideBoost = true;
            playerMove.moveSpeed *= speedMultiplier;  // Boost the speed by the multiplier

            // Optionally destroy the boost object after use
            Destroy(gameObject);

            // Start the coroutine to restore speed after the boost duration
            if (!isCoroutineRunning)
            {
                StartCoroutine(RestoreSpeedAfterDelay(2f));  // Adjust the duration as needed
                isCoroutineRunning = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerMove != null)
        {
            // Start restoring speed when the player exits the boost area
            if (!isInsideBoost)
            {
                StartCoroutine(RestoreSpeedAfterDelay(0.1f));  // Adjust the delay
                isCoroutineRunning = true;
            }
        }
    }

    // Coroutine to restore the player's original speed after the boost duration
    IEnumerator RestoreSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!isInsideBoost && playerMove != null)
        {
            // Restore the original speed after the delay
            playerMove.moveSpeed = originalSpeed * restoreSpeed;
            isCoroutineRunning = false;
        }
    }

    void Update()
    {
        // Any additional logic or checks can go here
    }
}
