using UnityEngine;

public class DamageOnCollision32D : MonoBehaviour
{
    public HealthManager2D playerHealthManager; // Reference to the player's HealthManager2D script

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the other collider has a HealthManager2D component (player or enemy)
        HealthManager2D otherHealthManager = collision.collider.GetComponent<HealthManager2D>();

        if (otherHealthManager != null)
        {
            // Reduce other collider's health by 4 (for player)
            otherHealthManager.TakeDamage(4);
        }
        else
        {
            // Debug.LogWarning("No HealthManager2D component found on the collided object.");
        }
    }
}
