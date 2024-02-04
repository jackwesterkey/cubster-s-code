using UnityEngine;
using System.Collections.Generic;

public class OhBoy2 : MonoBehaviour
{
    // Adjust these values according to your needs
    public float normalScaleY = 5.3135f;
    public float squishedScaleY = 1.012753f;

    private bool hasPlayerHit = false;
    private bool isScriptEnabled = true;

    // List of scripts to destroy
    public List<MonoBehaviour> scriptsToDestroy;

    // Audio settings
    public AudioClip collisionSound;
    private AudioSource audioSource;

    private void Start()
    {
        // Add an AudioSource component to the GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = collisionSound;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the player, the player hasn't hit the enemy yet, and the script is enabled
        if (collision.gameObject.CompareTag("Player") && !hasPlayerHit && isScriptEnabled)
        {
            // Set the flag to true
            hasPlayerHit = true;

            // Play the collision sound
            if (audioSource != null && collisionSound != null)
            {
                audioSource.Play();
            }

            // Adjust the Y scale of the enemy
            transform.localScale = new Vector3(transform.localScale.x, squishedScaleY, transform.localScale.z);

            // Disable the script
            isScriptEnabled = false;

            // Optionally, destroy other scripts from the list
            foreach (var script in scriptsToDestroy)
            {
                if (script != null)
                {
                    Destroy(script);
                }
            }

            // Optionally, you can also disable other components or perform other actions here
            // For example: GetComponent<YourOtherScript>().enabled = false;
        }
    }
}
