using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;  // Change to your platform prefab
    public Transform platformSpawnPoint;  // Change to the spawn point of the platform
    public float platformLifetime = 120f;  // Lifetime of the platform in seconds (2 minutes)
    public AmmoManager2 ammoManager;  // Reference to the AmmoManager2 script
    public ParticleSystem muzzleFlash1;  // Added line
    public AudioSource audioSource;  // Added line

    // Update is called once per frame
    void Update()
    {
        // Check for left mouse button click to spawn platform
        if (Input.GetMouseButtonDown(0) && ammoManager.currentAmmo > 0)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        // Instantiate a new platform
        GameObject platform = Instantiate(platformPrefab, platformSpawnPoint.position, platformSpawnPoint.rotation);

        // Call the DestroyPlatform function after a set lifetime
        Destroy(platform, platformLifetime);

        // Play muzzle flash and gunshot sound
        PlayMuzzleFlash1();
        PlayGunshotSound();

        // Decrease the number of shots remaining using the AmmoManager
        ammoManager.UseAmmo();
    }

    void PlayMuzzleFlash1()
    {
        if (muzzleFlash1 != null)
        {
            muzzleFlash1.Play();
        }
        else
        {
            Debug.Log("MuzzleFlash1 particle system not assigned!");
        }
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
