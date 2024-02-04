using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    // Speed for moving the object along the X-axis
    public float positionSpeed = 5.0f;

    // Minimum and maximum X positions
    public float minX = 0.0f;
    public float maxX = 10.0f;

    // Speed for moving the object along the Y-axis
    public float verticalSpeed = 2.0f;

    // Minimum and maximum Y positions
    public float minY = 0.0f;
    public float maxY = 10.0f;

    private int directionX = 1; // 1 for moving right, -1 for moving left
    private int directionY = 1; // 1 for moving up, -1 for moving down
    private Vector3 startPosition;

    // New variables for shooting
    public Transform target; // The player's transform
    public GameObject projectilePrefab; // The projectile to shoot
    public Transform bulletSpawnPoint; // The point where bullets are spawned
    public float shootingInterval = 2.0f; // Time interval between shots
    public float bulletSpeed = 10.0f; // Speed of the bullets
    public float bulletLifetime = 3f; // Lifetime of the bullets
    public AudioClip shootSound; // Audio clip to play when shooting
    private float timeSinceLastShot; // Time since the last shot

    void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the object right and left along the X-axis
        Vector3 newPosition = transform.position;
        newPosition.x += positionSpeed * directionX * Time.deltaTime;

        // Check if the object has reached the maximum or minimum X position
        if (newPosition.x >= maxX)
        {
            newPosition.x = maxX;
            directionX = -1; // Change direction to move left
        }
        else if (newPosition.x <= minX)
        {
            newPosition.x = minX;
            directionX = 1; // Change direction to move right
        }

        // Move the object up and down along the Y-axis
        newPosition.y += verticalSpeed * directionY * Time.deltaTime;

        // Check if the object has reached the maximum or minimum Y position
        if (newPosition.y >= maxY)
        {
            newPosition.y = maxY;
            directionY = -1; // Change direction to move down
        }
        else if (newPosition.y <= minY)
        {
            newPosition.y = minY;
            directionY = 1; // Change direction to move up
        }

        // Update the time since the last shot
        timeSinceLastShot += Time.deltaTime;

        // Shoot at the player if enough time has passed
        if (timeSinceLastShot >= shootingInterval)
        {
            ShootAtPlayer();
            timeSinceLastShot = 0f;
        }

        transform.position = newPosition;
    }

    void ShootAtPlayer()
    {
        if (target != null && projectilePrefab != null && bulletSpawnPoint != null)
        {
            // Calculate the direction towards the player
            Vector3 directionToPlayer = (target.position - bulletSpawnPoint.position).normalized;

            // Instantiate the projectile at the spawn point
            GameObject projectile = Instantiate(projectilePrefab, bulletSpawnPoint.position, Quaternion.identity);

            // Play the shoot sound at the spawn position
            if (shootSound != null)
            {
                AudioSource.PlayClipAtPoint(shootSound, bulletSpawnPoint.position);
            }

            // Set the velocity of the projectile to track the player
            projectile.GetComponent<Rigidbody>().velocity = directionToPlayer * bulletSpeed;

            // Set the lifetime of the projectile
            Destroy(projectile, bulletLifetime);
        }
    }
}
