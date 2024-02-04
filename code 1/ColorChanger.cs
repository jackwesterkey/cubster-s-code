using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 3f;
    public List<GameObject> objectsToChangeMaterial = new List<GameObject>(); // The objects that can have their materials changed
    public List<Material> materialsToApply = new List<Material>(); // List of materials to apply

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

            // Raycast to detect and change materials of objects in the bullet's path
            RaycastHit hit;
            if (Physics.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.forward, out hit, Mathf.Infinity))
            {
                // Check if the hit object is in the list of objects to change materials
                if (objectsToChangeMaterial.Contains(hit.collider.gameObject))
                {
                    // Randomly select a material from the list
                    int randomMaterialIndex = Random.Range(0, materialsToApply.Count);
                    Material selectedMaterial = materialsToApply[randomMaterialIndex];

                    // Change the material of the object hit by the raycast
                    Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        // Assign the selected material
                        renderer.material = selectedMaterial;
                    }
                }
            }
        }
    }
}
