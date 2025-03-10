using System.Collections;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    [Header("Projectile Properties")]
    [SerializeField] private float speed = 10f;        // Speed of the projectile
    [SerializeField] private int damage = 1;           // Damage dealt to the player
    [SerializeField] private float knockbackForce = 5f; // Knockback force to apply to the player
    [SerializeField] private float lifetime = 5f;      // Lifetime of the projectile before it destroys itself

    private float timer; // Timer to count down the projectile's lifetime

    public Vector2 direction; // Direction of the projectile

    private void Start()
    {
        // Start the timer for projectile lifetime
        timer = lifetime;
    }

    private void Update()
    {
        // Move the projectile in the specified direction
        transform.Translate(direction * speed * Time.deltaTime);

        // Reduce the lifetime of the projectile and destroy it after it expires
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject); // Destroy the projectile after lifetime ends
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Log the object we're colliding with for debugging
        Debug.Log("Collided with: " + collision.gameObject.name);

        // Check if the collided object has the HEALTH_V2 component
        HEALTH_V2 health = collision.gameObject.GetComponent<HEALTH_V2>();

        if (health != null)  // If the object has the HEALTH_V2 component
        {
            // Apply damage to the object
            health.TakeDamage(damage);
            Debug.Log("Damage applied to: " + collision.gameObject.name);

            // Check if the target object has a Rigidbody2D (for knockback)
            Rigidbody2D targetRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (targetRb != null)
            {
                // Apply knockback (vector from projectile to target)
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                targetRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                Debug.Log("Knockback applied to: " + collision.gameObject.name);
            }

            // Destroy the projectile after it hits a valid target
            Destroy(gameObject);
        }
        else
        {
            // If the object doesn't have HEALTH_V2, destroy the projectile (no damage)
            Debug.Log("No HEALTH_V2 found, destroying projectile.");
            Destroy(gameObject);
        }
    }
}
