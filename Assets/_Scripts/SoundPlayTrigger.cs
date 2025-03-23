using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    AudioSource source;  // Sound clip to play
    Collider2D soundTrigger; // AudioSource to play the sound

    private void Awake()
    {

        source = GetComponent<AudioSource>();
        soundTrigger = GetComponent<Collider2D>();
     }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the trigger zone (you can add more conditions if needed)
        if (other.CompareTag("Player"))
        {
                source.Play();
        }
    }
}