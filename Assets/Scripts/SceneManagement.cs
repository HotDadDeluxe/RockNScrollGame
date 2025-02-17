using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionTrigger : MonoBehaviour
{
    public string sceneName; // The name of the scene to load after the pause
    public AudioClip sceneChangeSound; // The sound to play when the player touches the object
    private AudioSource audioSource; // AudioSource reference to play the sound

    public float pauseTime = 2f; // Time to pause the scene before transitioning to the new scene

    private void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        // Ensure the AudioSource and sceneChangeSound are set
        if (audioSource == null || sceneChangeSound == null)
        {
            Debug.LogWarning("AudioSource or sceneChangeSound is not assigned.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with the object
        if (collision.gameObject.CompareTag("Player"))
        {
            // Play the sound if it's assigned
            if (audioSource != null && sceneChangeSound != null)
            {
                audioSource.PlayOneShot(sceneChangeSound);
            }

            // Start the scene transition after the specified delay
            StartCoroutine(TransitionToSceneAfterDelay(pauseTime));
        }
    }

    private IEnumerator TransitionToSceneAfterDelay(float delay)
    {
        audioSource.PlayOneShot(sceneChangeSound);
        // Wait for the specified pause time
        yield return new WaitForSeconds(delay);

        // Load the scene after the delay
        SceneManager.LoadScene(sceneName);
    }
}