using UnityEngine;

public class EnemyAI2dshooter : MonoBehaviour
{
    public float shootingInterval = 2f; // Time between shots
    public GameObject projectilePrefab; // Prefab of the projectile to be shot
    public Transform firePoint; // Point where the projectile is spawned
    public float bulletLifetime = 3f; // Lifetime of the projectile
    public float bulletSpeed = 10f; // Speed of the projectile
    public Transform target; // The player's transform

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has the "Player" tag
        InvokeRepeating("ShootAtPlayer", 0f, shootingInterval); // Invoke ShootAtPlayer method repeatedly with a delay
    }

    private void ShootAtPlayer()
    {
        // Calculate the direction from the enemy to the player
        Vector2 direction = (target.position - firePoint.position).normalized;

        // Instantiate a projectile at the firePoint position with the calculated direction
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        // Set the velocity of the projectile to shoot it towards the player
        projectileRb.velocity = direction * bulletSpeed;

        // Set the lifetime of the projectile
        Destroy(projectile, bulletLifetime);
    }

    private void Update()
    {
        // Optional: You can add more advanced AI logic here, like tracking the player or facing the player.
        // For simplicity, this example keeps the enemy stationary.
    }
}
