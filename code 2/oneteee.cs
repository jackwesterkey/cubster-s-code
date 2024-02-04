using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneteee : MonoBehaviour
{
    public float shootingInterval = 2f; // Time between shots
    public GameObject projectilePrefab; // Prefab of the projectile to be shot
    public Transform firePoint; // Point where the projectile is spawned
    public float bulletLifetime = 3f; // Lifetime of the projectile
    public float bulletSpeed = 5f; // Speed of the projectile
    public float seekForce = 5f; // Force applied to seek the player
    public Transform target; // The player's transform

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has the "Player" tag
        InvokeRepeating("ShootAtPlayer", 0f, shootingInterval); // Invoke ShootAtPlayer method repeatedly with a delay
    }

    private void ShootAtPlayer()
    {
        // Instantiate a projectile at the firePoint position
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        // Apply force to make the projectile seek the player
        projectileRb.velocity = SeekPlayer(projectileRb.position, target.position);

        // Set the lifetime of the projectile
        Destroy(projectile, bulletLifetime);
    }

    private Vector2 SeekPlayer(Vector2 currentPosition, Vector2 targetPosition)
    {
        // Calculate the direction to the target
        Vector2 direction = (targetPosition - currentPosition).normalized;

        // Apply force to seek the player
        Vector2 seekForceVector = direction * seekForce;

        // Apply additional speed to the projectile
        return seekForceVector * bulletSpeed;
    }

    private void Update()
    {
        // Optional: You can add more advanced AI logic here, like tracking the player or facing the player.
        // For simplicity, this example keeps the enemy stationary.
    }
}