using UnityEngine;

public class OhBoy : MonoBehaviour
{
    // Adjust these values according to your needs
    public float normalScaleY = 5.3135f;
    public float squishedScaleY = 1.012753f;

    private bool hasPlayerHit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the player and the player hasn't hit the enemy yet
        if (collision.gameObject.CompareTag("Player") && !hasPlayerHit)
        {
            // Set the flag to true
            hasPlayerHit = true;

            // Adjust the Y scale of the enemy
            transform.localScale = new Vector3(transform.localScale.x, squishedScaleY, transform.localScale.z);
        }
    }
}
