using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ohio : MonoBehaviour
{
    public float speed = 3f; // Speed of the enemy
    public Transform[] waypoints; // Array of waypoints for the enemy to follow
    private int currentWaypointIndex = 0;
    private int direction = 1; // 1 for forward, -1 for backward

    void Update()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned to the EnemyAI2D script!");
            return;
        }

        // Move towards the current waypoint
        Vector2 directionVector = waypoints[currentWaypointIndex].position - transform.position;
        directionVector.Normalize();
        transform.Translate(directionVector * speed * Time.deltaTime);

        // Check if the enemy has reached the current waypoint
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // Move to the next or previous waypoint based on the direction
            currentWaypointIndex += direction;

            // Reverse direction if at the beginning or end of waypoints
            if (currentWaypointIndex >= waypoints.Length || currentWaypointIndex < 0)
            {
                direction *= -1;
                currentWaypointIndex += direction * 2; // Move two steps to prevent skipping waypoints
            }
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
