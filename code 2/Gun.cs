using UnityEngine;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public List<Transform> bulletSpawnPoints;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 3f;

    public AmmoManager ammoManager;
    public ParticleSystem muzzleFlash1;
    public ParticleSystem muzzleFlash2;

    private bool isMuzzleFlash1Playing = false;
    private bool isMuzzleFlash2Playing = false;
    private bool gameStarted = false;
    private float startDelay = 1f;

    // Make AudioSource public
    public AudioSource audioSource;

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
        if (Input.GetMouseButtonDown(0) && gameStarted)
        {
            startDelay = 0f;
            SetMuzzleFlashStartDelay(startDelay);

            if (Shoot())
            {
                ammoManager.UseAmmo();
                PlayGunshotSound();
            }
        }
    }

    bool Shoot()
    {
        if (ammoManager != null)
        {
            if (ammoManager.currentAmmo > 0)
            {
                foreach (Transform bulletSpawnPoint in bulletSpawnPoints)
                {
                    GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                    bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
                    Destroy(bullet, bulletLifetime);
                }

                PlayMuzzleFlash1();
                PlayMuzzleFlash2();

                return true;
            }
            else
            {
                Debug.Log("Out of ammo!");
                return false;
            }
        }
        else
        {
            Debug.Log("AmmoManager not assigned to Gun!");
            return false;
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

    void PlayMuzzleFlash2()
    {
        if (muzzleFlash2 != null && !isMuzzleFlash2Playing)
        {
            isMuzzleFlash2Playing = true;
            muzzleFlash2.Play();
            Invoke("StopMuzzleFlash2", 0.6f);
        }
        else
        {
            Debug.Log("MuzzleFlash2 particle system not assigned or already playing!");
        }
    }

    void StopMuzzleFlash2()
    {
        if (muzzleFlash2 != null)
        {
            muzzleFlash2.Stop();
            isMuzzleFlash2Playing = false;
        }
    }

    void SetMuzzleFlashStartDelay(float delay)
    {
        if (muzzleFlash1 != null)
        {
            var mainModule1 = muzzleFlash1.main;
            mainModule1.startDelay = delay;
        }

        if (muzzleFlash2 != null)
        {
            var mainModule2 = muzzleFlash2.main;
            mainModule2.startDelay = delay;
        }
    }

    void SetGameStarted()
    {
        gameStarted = true;
        SetMuzzleFlashStartDelay(0f);
    }

    // Removed public AudioClip gunshotSound

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
