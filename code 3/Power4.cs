using UnityEngine;
using System.Collections;

public class Power4 : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5.0f;
    public float keepBackDistance = 4.0f;
    public float smoothness = 5.0f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float shootingInterval = 2.0f;
    public float bulletSpeed = 10.0f;
    public float bulletLifetime = 3f;
    private float timeSinceLastShot;
    private bool canShoot = false;

    private Animator thePooMonsterAnimator;
    private bool isThePooMonsterPlaying = false;
    private Vector3 initialPosition;

    void Start()
    {
        thePooMonsterAnimator = GetComponent<Animator>();
        initialPosition = transform.position;

        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        // Add an initial delay of 2 seconds before the enemy can start shooting.
        yield return new WaitForSeconds(2.0f);

        // Set the flag to allow shooting.
        canShoot = true;

        // Now, the enemy can start executing its regular logic in the Update method.
    }

    void Update()
    {
        Vector3 toPlayer = player.position - transform.position;
        toPlayer.Normalize();

        bool isObjectMoving = (transform.position != initialPosition);
        initialPosition = transform.position;

        if (isObjectMoving)
        {
            MoveTowardsPlayer(toPlayer);
        }

        // Check for shooting cooldown and shooting input
        if (canShoot && Time.time - timeSinceLastShot > shootingInterval)
        {
            Shoot();
        }
    }

    private void MoveTowardsPlayer(Vector3 toPlayer)
    {
        Vector3 targetPosition = player.position - toPlayer * keepBackDistance;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Play "the poo monster" animation while moving
        if (!isThePooMonsterPlaying)
        {
            StartThePooMonster();
        }
    }

    void StartThePooMonster()
    {
        thePooMonsterAnimator.Play("the poo monster");
        isThePooMonsterPlaying = true;
    }

    void StopThePooMonster()
    {
        thePooMonsterAnimator.Play("New State");
        isThePooMonsterPlaying = false;
    }

    void Shoot()
    {
        // Calculate the direction to the player
        Vector3 toPlayer = player.position - transform.position;
        toPlayer.Normalize();

        // Calculate the position to move to, which is keepBackDistance units away from the player
        Vector3 targetPosition = player.position - toPlayer * keepBackDistance;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Play "the poo monster" animation while shooting
        StartThePooMonster();

        // Instantiate a projectile at the spawn point
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        // Add force to the projectile to make it move at the specified speed
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            projectileRb.AddForce(toPlayer * bulletSpeed, ForceMode.Impulse);

            // Set the lifetime of the projectile
            Destroy(projectile, bulletLifetime);
        }

        // Stop "the poo monster" animation after shooting
        StopThePooMonster();

        // Update the last shot time for cooldown
        timeSinceLastShot = Time.time;

        // Reset canShoot after shooting
        canShoot = false;
        // Start a coroutine to reset canShoot after a delay
        StartCoroutine(ResetCanShoot());
    }

    // Coroutine to reset canShoot after a delay
    IEnumerator ResetCanShoot()
    {
        yield return new WaitForSeconds(shootingInterval);
        canShoot = true;
    }
}