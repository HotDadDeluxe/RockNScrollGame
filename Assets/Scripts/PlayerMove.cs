using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;
    [SerializeField] private Transform feetPos;
    [SerializeField] public float moveSpeed = 5f;

    bool isTakingDamage;
    bool isInvincible;

    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer;

    [Header("Player Animations")]
    [SerializeField] private AnimationClip idleAnimation;
    [SerializeField] private AnimationClip runAnimation;
    [SerializeField] private AnimationClip jumpAnimation;
    [SerializeField] private AnimationClip damageAnimation;

    // Invincibility and Knockback Variables
    [SerializeField] private float invincibilityTime = 1f; // Invincibility duration
    [SerializeField] private float knockbackForce = 10f;  // Knockback strength

    void Start() { }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        // Move player to the right continuously
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y); // Set horizontal velocity, keep the vertical one unchanged

        // Jump logic
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Only modify the vertical velocity during the jump
        }

        // Jump timer logic
        if (isJumping && jumpTimer < jumpTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Ensure only vertical velocity is changed
            jumpTimer += Time.deltaTime;
        }
        else
        {
            isJumping = false;
        }

        // Stop jumping when the button is released
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpTimer = 0;
        }
    }

    // Method to handle taking damage, with invincibility and knockback
    public void TakeDamage(int damage, Vector2 knockbackDirection)
    {
        if (isInvincible) return;

        isInvincible = true;
        // Apply knockback force
        rb.velocity = Vector2.zero;  // Stop current movement to apply knockback accurately
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        // Start invincibility timer
        StartCoroutine(InvincibilityCoroutine());

        // You can add your health reduction here

        // Play damage animation
        PlayDamageAnimation();

        // Handle player death here if health reaches 0
    }

    // Coroutine to reset invincibility after a short time
    private IEnumerator InvincibilityCoroutine()
    {
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    // Play damage animation
    void PlayDamageAnimation()
    {
        if (damageAnimation != null)
        {
            // Here you would trigger the animation for damage
        }
    }
}
