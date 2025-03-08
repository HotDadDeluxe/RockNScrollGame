using System.Collections;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private Transform feetPos;
    [SerializeField] public float moveSpeed = 5f;

    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isSliding = false;
    private float jumpTimer;
    [SerializeField] private float jumpTime = 0.5f;

    [Header("Player Animations")]
    [SerializeField] private Animator animator;
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Damage = Animator.StringToHash("Damage");
    private static readonly int Slide = Animator.StringToHash("Slide");

    [Header("Slide Settings")]
    [SerializeField] private float slideDuration = 1.5f;
    [SerializeField] private Vector2 slideHitboxSize = new Vector2(1.5f, 0.5f);
    private Vector2 originalHitboxSize;

    [Header("Invincibility and Knockback")]
    [SerializeField] private float invincibilityTime = 1f;
    [SerializeField] private float knockbackForce = 10f;
    private bool isInvincible = false;

    void Start()
    {
        originalHitboxSize = rb.GetComponent<BoxCollider2D>().size;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleSlide();
        UpdateAnimations();
    }

    private void HandleMovement()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimer = 0f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger(Jump);
        }

        if (isJumping && jumpTimer < jumpTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimer += Time.deltaTime;
        }
        else
        {
            isJumping = false;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
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

    private IEnumerator SlideCoroutine()
    {
        isSliding = true;
        rb.GetComponent<BoxCollider2D>().size = slideHitboxSize;
        transform.rotation = Quaternion.Euler(0, 0, 90);
        animator.SetTrigger(Slide);

        yield return new WaitForSeconds(slideDuration);

        rb.GetComponent<BoxCollider2D>().size = originalHitboxSize;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        isSliding = false;
    }

    private void UpdateAnimations()
    {
        if (isGrounded && !isJumping && !isSliding)
        {
            animator.SetBool(Run, moveSpeed != 0);
        }
        else
        {
            animator.SetBool(Run, false);
        }

        if (!isGrounded && isJumping)
        {
            animator.SetTrigger(Jump);
        }

        if (isSliding)
        {
            animator.SetTrigger(Slide);
        }
    }

    public void TakeDamage(int damage, Vector2 knockbackDirection)
    {
        if (isInvincible) return;

        isInvincible = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(InvincibilityCoroutine());
        animator.SetTrigger(Damage);
    }

    private IEnumerator InvincibilityCoroutine()
    {
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }
}
