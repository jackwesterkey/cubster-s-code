using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public List<Transform> bulletSpawnPoints; // List of bullet spawn points
    public float bulletSpeed = 10f;
    public float bulletLifetime = 3f;

    public AmmoManager4 ammoManager4;

    public ParticleSystem muzzleFlash;

    // AudioSource for chain gun sound
    public AudioSource audioSource;

    private bool isFiring = false;
    private float shotDelay = 0.2f; // Fixed shot delay of 0.2 seconds

    private Animator gunAnimator;

    void Start()
    {
        // Get the Animator component attached to the gun
        gunAnimator = GetComponent<Animator>();

        // Initialize the AudioSource component if not assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Set the audio to loop
        audioSource.loop = true;
    }

    void Update()
    {
        // Check if the right mouse button is held down and there is available ammo
        if (Input.GetMouseButton(1) && ammoManager4 != null && ammoManager4.currentAmmo > 0)
        {
            if (gunAnimator != null && !isFiring)
            {
                // Enable the Animator when starting to shoot and there is ammo
                gunAnimator.enabled = true;

                // Set the "isShooting" parameter based on the public float "animator"
                gunAnimator.SetFloat("isShooting", 1f);

                // Start the firing coroutine
                StartCoroutine(FireRhythmCoroutine());

                // Play muzzle flash when holding down the right mouse button
                PlayMuzzleFlash();

                // Play chain gun sound if not already playing
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
        else
        {
            isFiring = false; // Stop firing when the right mouse button is released
            if (gunAnimator != null)
            {
                // Disable the Animator when not shooting or out of ammo
                gunAnimator.enabled = false;

                // Reset the "isShooting" parameter when the button is released or out of ammo
                gunAnimator.SetFloat("isShooting", 0f);
            }

            // Stop the audio when the right mouse button is released
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    IEnumerator FireRhythmCoroutine()
    {
        isFiring = true;

        while (Input.GetMouseButton(1) && ammoManager4 != null && ammoManager4.currentAmmo > 0)
        {
            Shoot();
            ammoManager4.UseAmmo();
            yield return new WaitForSeconds(shotDelay);
            // Play muzzle flash for each shot in sync with the shot delay
            PlayMuzzleFlash();
        }

        isFiring = false; // Reset firing flag after all shots are fired or right mouse button is released
    }

    void Shoot()
    {
        if (ammoManager4 != null && ammoManager4.currentAmmo > 0)
        {
            foreach (Transform bulletSpawnPoint in bulletSpawnPoints)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
                Destroy(bullet, bulletLifetime);
            }
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }

    void PlayMuzzleFlash()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
    }
}
