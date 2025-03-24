using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private Transform feetPos;
    [SerializeField] public float moveSpeed = 5f;

    bool isTakingDamage;
    bool isInvincible;

    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isSliding = false;
    private float jumpTimer;
    [SerializeField] private float jumpTime = 0.5f;  // Time the player can hold the jump button

    [Header("Player Animations")]
    [SerializeField] private Animator animator; // Reference to the Animator

    // Invincibility and Knockback Variables
    [SerializeField] private float invincibilityTime = 1f; // Invincibility duration
    [SerializeField] private float knockbackForce = 10f;  // Knockback strength

    [Header("Slide Settings")]
    [SerializeField] private float slideDuration = 1.5f; // Duration of the slide
    [SerializeField] private Vector2 slideHitboxSize = new Vector2(1.5f, 0.5f); // Size of the hitbox during slide
    private Vector2 originalHitboxSize; // Original size of the hitbox

    void Start()
    {
        if (rb != null)
        {
            originalHitboxSize = rb.GetComponent<CapsuleCollider2D>().size; // Store the original hitbox size
        }
    }

    private void Update()
    {
        if (transform != null && rb != null)
        {
            HandleMovement();
            HandleJump();
            HandleSlide();
            UpdateAnimations();
        }
    }

    private void HandleMovement()
    {
        if (transform != null && rb != null)
        {
            // Check if the player is grounded
            isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

            // Adjust speed based on key input
            float modifiedSpeed = moveSpeed;

            // Check if the left movement keys are pressed
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                modifiedSpeed = moveSpeed * 0.9f; // Set speed to 90%
            }
            // Check if the right movement keys are pressed
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                modifiedSpeed = moveSpeed * 1.15f; // Set speed to 115%
            }

            // If both A/Left Arrow and D/Right Arrow are pressed, reset to original speed
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) &&
                (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
            {
                modifiedSpeed = moveSpeed; // Reset to original speed
            }

            // Move player using the modified speed
            rb.velocity = new Vector2(modifiedSpeed, rb.velocity.y); // Set horizontal velocity, keep the vertical one unchanged
        }
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
            jumpTimer = 0f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (isJumping && jumpTimer < jumpTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimer += Time.deltaTime;
        }
        else
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
            jumpTimer = 0;
        }
    }

    private void HandleSlide()
    {
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftShift)) && !isSliding)
        {
            StartCoroutine(SlideCoroutine());
        }
    }

    private void UpdateAnimations()
    {
        // Set IsRunning parameter based on movement
        animator.SetBool("IsRunning", moveSpeed != 0 && isGrounded && !isJumping && !isSliding);

        // Set IsJumping parameter
        animator.SetBool("IsJumping", isJumping);

        // Set IsDamaged parameter
        animator.SetBool("IsDamaged", isTakingDamage);

        // Set IsSliding parameter
        animator.SetBool("IsSliding", isSliding);
    }

    private IEnumerator SlideCoroutine()
    {
        isSliding = true;
        if (rb != null)
        {
            rb.GetComponent<BoxCollider2D>().size = slideHitboxSize; // Change hitbox size
        }

        transform.rotation = Quaternion.Euler(0, 0, 90); // Set rotation to horizontal
        animator.SetBool("IsSliding", true); // Play slide animation

        yield return new WaitForSeconds(slideDuration);

        if (rb != null)
        {
            rb.GetComponent<BoxCollider2D>().size = originalHitboxSize; // Revert hitbox size
        }
        transform.rotation = Quaternion.Euler(0, 0, 0); // Reset rotation to vertical
        isSliding = false;
        animator.SetBool("IsSliding", false);
    }

    // Method to handle taking damage, with invincibility and knockback
    public void TakeDamage(int damage, Vector2 knockbackDirection)
    {
        if (isInvincible) return;

        isInvincible = true;
        isTakingDamage = true;

        // Apply knockback force
        if (rb != null)
        {
            rb.velocity = Vector2.zero;  // Stop current movement to apply knockback accurately
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }

        // Start invincibility timer
        StartCoroutine(InvincibilityCoroutine());

        // Play damage animation
        if (animator != null)
        {
            animator.SetBool("IsDamaged", true);  // Trigger damage animation
        }

        // Handle player death here if health reaches 0
    }

    // Coroutine to reset invincibility after a short time
    private IEnumerator InvincibilityCoroutine()
    {
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
        isTakingDamage = false;
        animator.SetBool("IsDamaged", false);
    }
}