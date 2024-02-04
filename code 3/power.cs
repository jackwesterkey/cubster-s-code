using UnityEngine;
using UnityEngine.AI;

public class power : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5.0f;
    public float keepBackDistance = 4.0f;
    public float enableAnimatorDistance = 2.0f; // Distance to enable the Animator
    public float smoothness = 5.0f;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Animator animator;
    private Vector3 initialPosition; // Store the initial position of the enemy

    void Start()
    {
        // Store the initial position when the game starts.
        initialPosition = transform.position;
    }

    void Update()
    {
        // Check if the enemy has moved a certain distance to enable the Animator.
        if (Vector3.Distance(initialPosition, transform.position) >= enableAnimatorDistance)
        {
            // The line triggering the "Shoot" animation is removed.
        }

        MoveTowards(target.position);
    }

    private void MoveTowards(Vector3 destination)
    {
        if (agent != null)
        {
            // Calculate the direction to the player.
            Vector3 toPlayer = destination - transform.position;
            toPlayer.Normalize();

            // Calculate the position to move to, which is keepBackDistance units away from the player.
            Vector3 targetPosition = destination - toPlayer * keepBackDistance;

            // Move towards the target position.
            agent.SetDestination(targetPosition);
        }
        else
        {
            // Implement your own movement logic here if you're not using NavMeshAgent.
        }
    }
}
