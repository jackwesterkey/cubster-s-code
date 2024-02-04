using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public AmmoManager1 ammoManager1; // Reference to the AmmoManager1 script

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Rigidbody rigidbody; // Reference to the player's rigidbody
    public float bulletSpeed = 10f;
    public float bulletLifetime = 3f;
    public int numPellets = 5; // Number of pellets in one shot
    public float spreadAngle = 10f; // Spread angle for the pellets
    public float knockbackForce = 5f; // Knockback force

    // Muzzle flash particle systems
    public ParticleSystem muzzleFlash1;

    // Flags to check if the muzzle flashes are currently playing
    private bool isMuzzleFlash1Playing = false;

    // AudioSource for shotgun sound
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Set start delay to 1 for both muzzle flashes
        SetMuzzleFlashStartDelay(1f);

        // Set the gameStarted flag to true after a short delay (adjust as needed)
        Invoke("SetGameStarted", 1.5f);

        // Initialize the AudioSource component if not assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check for left mouse button release to shoot
        if (Input.GetMouseButtonUp(0))
        {
            // Attempt to shoot and subtract one ammo
            if (Shoot())
            {
                ammoManager1.UseAmmo(); // Consume one unit of ammo

                // Play the shotgun sound
                PlayShotgunSound();
            }
        }
    }

    bool Shoot()
    {
        // Check if there is enough ammo to shoot
        if (ammoManager1.currentAmmo >= 1)
        {
            // Play the muzzle flash before shooting
            PlayMuzzleFlash1();

            // Loop to instantiate multiple bullets with a spread
            for (int i = 0; i < numPellets; i++)
            {
                // Calculate a random rotation within the specified spread angle
                Quaternion spreadRotation = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0f);

                // Instantiate a new bullet with the calculated rotation
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation * spreadRotation);

                // Get the rigidbody component of the bullet
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

                // Apply force to the bullet in the forward direction
                bulletRigidbody.velocity = bullet.transform.forward * bulletSpeed;

                // Apply knockback force to the player using Rigidbody
                rigidbody.AddForce(-bulletSpawnPoint.forward * knockbackForce, ForceMode.Impulse);

                // Call the DestroyBullet function after a set lifetime
                Destroy(bullet, bulletLifetime);
            }

            return true; // Shooting successful
        }
        else
        {
            // Out of ammo, cannot shoot
            Debug.Log("Out of ammo!");
            return false; // Shooting failed
        }
    }

    // Method to play the first muzzle flash only when the mouse button is clicked
    void PlayMuzzleFlash1()
    {
        if (muzzleFlash1 != null && !isMuzzleFlash1Playing)
        {
            // Set the flag to indicate that the first muzzle flash is playing
            isMuzzleFlash1Playing = true;

            // Play the first muzzle flash particle system
            muzzleFlash1.Play();

            // Stop the first muzzle flash after a short delay (adjust as needed)
            Invoke("StopMuzzleFlash1", 0.6f);
        }
    }

    // Method to stop the first muzzle flash
    void StopMuzzleFlash1()
    {
        if (muzzleFlash1 != null)
        {
            // Stop the first muzzle flash particle system
            muzzleFlash1.Stop();

            // Reset the first flag
            isMuzzleFlash1Playing = false;
        }
    }

    // Method to set the start delay for both muzzle flashes
    void SetMuzzleFlashStartDelay(float delay)
    {
        if (muzzleFlash1 != null)
        {
            var mainModule1 = muzzleFlash1.main;
            mainModule1.startDelay = delay;
        }
    }

    // Method to set the gameStarted flag
    void SetGameStarted()
    {
        // Set start delay back to 0 for both muzzle flashes
        SetMuzzleFlashStartDelay(0f);
    }

    // Method to play the shotgun sound
    void PlayShotgunSound()
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
