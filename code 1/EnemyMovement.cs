using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; // The player's transform
    public float moveSpeed = 5.0f; // Movement speed
    public float smoothness = 5.0f; // Smoothing factor

    private Rigidbody rigid;

    private void Start()
    {
        // Get the Rigidbody component on startup
        rigid = GetComponent<Rigidbody>();
        if (rigid == null)
        {
            Debug.LogError("Rigidbody component not found on the enemy.");
        }
    }

    private void Update()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                return;
            }
        }

        // Calculate the direction from the enemy to the player.
        Vector3 moveDirection = target.position - transform.position;
        moveDirection.Normalize();

        // Use Lerp to smoothly move the enemy towards the player.
        Vector3 newPosition = Vector3.Lerp(transform.position, transform.position + moveDirection, Time.deltaTime * smoothness);

        // Move the enemy to the new position.
        // transform.position = newPosition; // Comment out this line if using Rigidbody.

        // Use Rigidbody to move the enemy.
        if (rigid != null)
        {
            // Calculate the velocity based on the direction and speed.
            Vector3 velocity = moveDirection * moveSpeed;

            // Apply the velocity as a force to the Rigidbody.
            rigid.AddForce(velocity);
        }
    }
}
