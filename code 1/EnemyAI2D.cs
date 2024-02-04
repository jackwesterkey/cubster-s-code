using UnityEngine;

public class EnemyAI2D : MonoBehaviour
{
    public float speed = 3f; // Speed of the enemy
    public Transform target; // Target (e.g., the player)

    void Update()
    {
        if (target != null)
        {
            // Move towards the target
            Vector2 direction = target.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the enemy collided with the player
        if (other.CompareTag("Player"))
        {
            // You can add code here to handle the player/enemy interaction (e.g., deal damage to the player)
            Debug.Log("Player hit!");
        }
    }
}
