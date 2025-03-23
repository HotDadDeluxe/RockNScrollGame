using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{
    public string collectibleType;
    public float destroyDelay = 1f; // Optional delay time
    public float minPitch = 0.5f; // Minimum pitch range
    public float maxPitch = 1.5f; // Maximum pitch range

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.Collect(collectibleType);
            SpriteRenderer sr = GetComponent<SpriteRenderer>(); // 1. Get the SpriteRenderer component
            if (sr != null) sr.enabled = false; // 2. Disable the SpriteRenderer before delay
            AudioSource audioSource = GetComponent<AudioSource>(); // 3. Get the AudioSource component
            if (audioSource != null) audioSource.pitch = Random.Range(minPitch, maxPitch); // Alter the pitch randomly
            StartCoroutine(DestroyAfterDelay()); // Start the delay coroutine
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay); // Wait for the delay
        Destroy(gameObject); // Destroy the object
    }
}
