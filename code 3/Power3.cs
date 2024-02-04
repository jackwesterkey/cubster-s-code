using UnityEngine;

public class Power3 : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5.0f;
    public float keepBackDistance = 4.0f;
    public float smoothness = 5.0f;

    private Animator enemywalkAnimator;
    private bool isEnemywalkPlaying = false;
    private Vector3 initialPosition;

    void Start()
    {
        enemywalkAnimator = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the direction to the player
        Vector3 toPlayer = player.position - transform.position;
        toPlayer.Normalize();

        // Check if the object is moving
        bool isObjectMoving = (transform.position != initialPosition);
        initialPosition = transform.position;

        if (isObjectMoving)
        {
            MoveTowardsPlayer(toPlayer);

            if (!isEnemywalkPlaying)
            {
                StartEnemywalk();
                isEnemywalkPlaying = true;
            }
        }
        else
        {
            StopEnemywalk();
            isEnemywalkPlaying = false;
        }
    }

    private void MoveTowardsPlayer(Vector3 toPlayer)
    {
        // Calculate the position to move to, which is keepBackDistance units away from the player
        Vector3 targetPosition = player.position - toPlayer * keepBackDistance;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void StartEnemywalk()
    {
        // Play the "enemywalk" animation
        enemywalkAnimator.Play("enemywalk");
    }

    void StopEnemywalk()
    {
        // Stop the "enemywalk" animation
        enemywalkAnimator.Play("New State");
    }
}
