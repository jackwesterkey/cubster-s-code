using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 3f;
    public List<GameObject> objectsToDisable = new List<GameObject>(); // The objects that can have their colliders disabled

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Instantiate a bullet at the bulletSpawnPoint's position and rotation
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Set the bullet's speed
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = bulletSpawnPoint.forward * bulletSpeed;

            // Destroy the bullet after a certain lifetime
            Destroy(bullet, bulletLifetime);

            // Raycast to detect and disable colliders of objects in the bullet's path
            RaycastHit hit;
            if (Physics.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.forward, out hit, Mathf.Infinity))
            {
                // Check if the hit object is in the list of objects to disable colliders
                if (objectsToDisable.Contains(hit.collider.gameObject))
                {
                    // Disable the collider of the object hit by the raycast
                    hit.collider.enabled = false;
                }
            }
        }
    }
}
