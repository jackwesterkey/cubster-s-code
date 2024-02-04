using UnityEngine;
using UnityEngine.AI;

public class testie : MonoBehaviour
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
            // Check if the agent is moving towards the player
            if (IsMovingTowardsPlayer())
            {
                // If moving towards the player, play the "Walk" animation
                animator.Play("Mini Simple Characters Armature|Walk");
            }
            else
            {
                // If not moving towards the player, play the "Idle" animation
                animator.Play("Mini Simple Characters Armature|Idle");
            }
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

            // Check if the enemy is close to the player or keepBackDistance is reached
            if (Vector3.Distance(transform.position, player.position) < enableAnimatorDistance || Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // If close to the player or keepBackDistance is reached, play the "Idle" animation
                animator.Play("Mini Simple Characters Armature|Idle");
            }
            else
            {
                // Move towards the target position.
                agent.SetDestination(targetPosition);
            }
        }
        else
        {
            // Implement your own movement logic here if you're not using NavMeshAgent.
        }
    }

    private bool IsMovingTowardsPlayer()
    {
        // Check if the agent is moving towards the player by comparing the direction
        Vector3 toPlayer = player.position - transform.position;
        toPlayer.Normalize();
        float dotProduct = Vector3.Dot(toPlayer, transform.forward);

        // If the dot product is positive, the enemy is moving towards the player
        return dotProduct > 0.5f;
    }
}
