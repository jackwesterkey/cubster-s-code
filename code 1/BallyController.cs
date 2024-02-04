using UnityEngine;

public class BallyController : MonoBehaviour
{
    public float speed = 5f;
    public float smoothing = 0.5f; // Adjust the smoothing factor

    private Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        // Get input from the player
        float verticalInput = 0f;
        float horizontalInput = 0f;

        // Use W for forward movement
        if (Input.GetKey(KeyCode.A))
        {
            verticalInput = 1f;
        }
        // Use S for backward movement
        else if (Input.GetKey(KeyCode.D))
        {
            verticalInput = -1f;
        }

        // Use A for left movement
        if (Input.GetKey(KeyCode.S))
        {
            horizontalInput = -1f;
        }
        // Use D for right movement
        else if (Input.GetKey(KeyCode.W))
        {
            horizontalInput = 1f;
        }

        // Calculate the movement direction
        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Calculate the target position based on input
        targetPosition = transform.position + inputDirection * speed;

        // Move the player smoothly towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}
