using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mew : MonoBehaviour
{
    public GameObject bulletPrefab;
    public List<Transform> bulletSpawnPoints;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 3f;
    public GameObject target;  // Specify the target GameObject

    private bool gameStarted = false;

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameStarted)
        {
            // Remove audio-related function call
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        // Your shooting logic without audio

        foreach (Transform bulletSpawnPoint in bulletSpawnPoints)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Store the initial position of the bullet
            Vector3 initialPosition = bullet.transform.position;

            // Calculate the direction to the target
            Vector3 directionToTarget = (target.transform.position - initialPosition).normalized;

            // Move the bullet towards the target over time
            for (float elapsed = 0; elapsed < bulletLifetime; elapsed += Time.deltaTime)
            {
                bullet.transform.position = initialPosition + directionToTarget * bulletSpeed * elapsed;
                // Yielding null allows the frame to render before moving to the next position
                yield return null;
            }

            // Destroy the bullet after its lifetime
            Destroy(bullet);
        }
    }

    void StartGame()
    {
        gameStarted = true;
    }
}
