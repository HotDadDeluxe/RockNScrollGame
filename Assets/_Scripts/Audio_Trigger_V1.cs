using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;  // Reference to the AudioSource
    [SerializeField] private float targetVolume = 1f;  // Volume value when triggered (100%)

    private void Start()
    {
        // Ensure the audio starts at 0 volume
        if (audioSource != null)
        {
            audioSource.volume = 0f;
            Debug.Log("Audio volume set to 0 at the start.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug message to show when something enters the trigger zone
        Debug.Log("Trigger entered by: " + collision.gameObject.name);

        // Check if the player collides with this object
        if (collision.CompareTag("Player"))
        {
            // Set the volume to 100 (1.0)
            if (audioSource != null)
            {
                audioSource.volume = targetVolume;
                Debug.Log("Player triggered the event. Audio volume set to 100%.");

                // Play the audio if it's not already playing
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                    Debug.Log("Audio started playing.");
                }
            }
            else
            {
                Debug.LogWarning("No AudioSource attached to this object.");
            }
        }
        else
        {
            // If anything other than the player triggers it
            Debug.Log("Something else triggered the audio, but it's not the player.");
        }
    }
}
