using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainGun2 : MonoBehaviour
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

    // Animator for handling gun animations
    public Animator gunAnimator;

    void Start()
    {
        // Initialize the AudioSource component if not assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Set the audio to loop
        audioSource.loop = true;

        // Get the Animator component attached to the gun
        if (gunAnimator == null)
        {
            gunAnimator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        // Check if the right mouse button is held down and there is available ammo
        if (Input.GetMouseButtonDown(1) && ammoManager4 != null && ammoManager4.currentAmmo > 0)
        {
            // Play "railer" once when right mouse button is clicked
            if (gunAnimator != null)
            {
                gunAnimator.Play("railer", 0, 0f);
            }
        }

        if (Input.GetMouseButton(1) && ammoManager4 != null && ammoManager4.currentAmmo > 0)
        {
            if (!isFiring)
            {
                // Start the firing coroutine
                StartCoroutine(FireRhythmCoroutine());

                // Play muzzle flash when holding down the right mouse button
                PlayMuzzleFlash();

                // Play chain gun sound if not already playing
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                // Loop "railer" animation
                if (gunAnimator != null)
                {
                    gunAnimator.SetBool("isShooting", true);
                }
            }
        }
        else
        {
            isFiring = false; // Stop firing when the right mouse button is released

            // Stop the audio when the right mouse button is released
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // Set the "isShooting" parameter to false in the Animator
            if (gunAnimator != null)
            {
                gunAnimator.SetBool("isShooting", false);

                // Play "New State" when not shooting
                gunAnimator.Play("New State", 0, 0f);
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
