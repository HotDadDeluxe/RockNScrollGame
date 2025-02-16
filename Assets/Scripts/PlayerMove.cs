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

    // New Serialize Fields for animations
    [Header("Player Animations")]
    [SerializeField] private AnimationClip idleAnimation;
    [SerializeField] private AnimationClip runAnimation;
    [SerializeField] private AnimationClip jumpAnimation;
    [SerializeField] private AnimationClip damageAnimation;

    void Start()
    {

    }


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
}
