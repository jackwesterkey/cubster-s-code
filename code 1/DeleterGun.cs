using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleterGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 3f;
    public List<GameObject> objectsToDelete = new List<GameObject>(); // The objects that can be deleted

    public ParticleSystem muzzleFlash1;
    public AudioSource audioSource;
    private bool isMuzzleFlash1Playing = false;
    private bool gameStarted = false;
    private float startDelay = 1f;

    void Start()
    {
        SetMuzzleFlashStartDelay(startDelay);
        Invoke("SetGameStarted", 1.5f);

        // Initialize the AudioSource component if not assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && gameStarted)
        {
            // Instantiate a bullet at the bulletSpawnPoint's position and rotation
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Set the bullet's speed
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = bulletSpawnPoint.forward * bulletSpeed;

            // Destroy the bullet after a certain lifetime
            Destroy(bullet, bulletLifetime);

            // Raycast to detect and destroy objects in the bullet's path
            RaycastHit hit;
            if (Physics.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.forward, out hit, Mathf.Infinity))
            {
                // Check if the hit object is in the list of objects to delete
                if (objectsToDelete.Contains(hit.collider.gameObject))
                {
                    // Destroy the object hit by the raycast
                    Destroy(hit.collider.gameObject);
                }
            }

            // Play muzzle flash and gunshot sound
            PlayMuzzleFlash1();
            PlayGunshotSound();
        }
    }

    void PlayMuzzleFlash1()
    {
        if (muzzleFlash1 != null && !isMuzzleFlash1Playing)
        {
            isMuzzleFlash1Playing = true;
            muzzleFlash1.Play();
            Invoke("StopMuzzleFlash1", 0.6f);
        }
        else
        {
            Debug.Log("MuzzleFlash1 particle system not assigned or already playing!");
        }
    }

    void StopMuzzleFlash1()
    {
        if (muzzleFlash1 != null)
        {
            muzzleFlash1.Stop();
            isMuzzleFlash1Playing = false;
        }
    }

    void SetMuzzleFlashStartDelay(float delay)
    {
        if (muzzleFlash1 != null)
        {
            var mainModule1 = muzzleFlash1.main;
            mainModule1.startDelay = delay;
        }
    }

    void SetGameStarted()
    {
        gameStarted = true;
        SetMuzzleFlashStartDelay(0f);
    }

    void PlayGunshotSound()
    {
        // Play the audio clip directly from the AudioSource component
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.Log("AudioSource not assigned!");
        }
    }
}
