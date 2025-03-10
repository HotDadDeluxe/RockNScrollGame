using UnityEngine;

public class Mover_Proto : MonoBehaviour
{
    // Oscillation variables
    public float horizontalAmplitude = 5.0f; // Amount of horizontal oscillation
    public float horizontalFrequency = 1.0f; // Speed of horizontal oscillation
    public float verticalAmplitude = 2.0f; // Amount of vertical oscillation
    public float verticalFrequency = 1.5f; // Speed of vertical oscillation
    public float movingSpeed = 1.0f; // Speed at which the object moves forward

    private float initialX; // Initial X position for oscillation
    private float initialY; // Initial Y position for oscillation

    private void Start()
    {
        // Save the initial positions for oscillation calculations
        initialX = transform.position.x;
        initialY = transform.position.y;
    }

    private void Update()
    {
        // Horizontal and vertical oscillation using sine and cosine functions
        float horizontalOffset = Mathf.Sin(Time.time * horizontalFrequency) * horizontalAmplitude;
        float verticalOffset = Mathf.Cos(Time.time * verticalFrequency) * verticalAmplitude;

        // Update the object's position with oscillation
        Vector3 newPosition = transform.position;
        newPosition.x = initialX + horizontalOffset;
        newPosition.y = initialY + verticalOffset;
        transform.position = newPosition;

        // Move the object forward at the given speed
        transform.Translate(Vector3.right * movingSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When the player touches the object, make them a child of it
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // When the player leaves the object, remove the parent-child relationship
        if (collision.gameObject.CompareTag("Player"))
        {
            Transform playerTransform = collision.transform;
            Transform parentTransform = playerTransform.parent;

            // Ensure the parent is not being activated or deactivated
            if (parentTransform != null && !parentTransform.gameObject.activeSelf)
            {
                playerTransform.SetParent(null);
            }
        }
    }
}
