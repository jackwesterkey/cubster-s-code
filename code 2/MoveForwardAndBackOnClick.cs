using UnityEngine;

public class MoveForwardAndBackOnClick : MonoBehaviour
{
    public float forwardSpeed = 5f;  // Speed at which the object moves forward
    public float backwardSpeed = 2f; // Speed at which the object moves backward

    private bool isMovingForward = false;
    private bool isMovingBackward = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isMovingForward = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isMovingForward = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isMovingBackward = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMovingBackward = false;
        }
    }

    private void FixedUpdate()
    {
        if (isMovingForward)
        {
            MoveObjectForward();
        }

        if (isMovingBackward)
        {
            MoveObjectBackward();
        }
    }

    private void MoveObjectForward()
    {
        // Calculate the forward direction based on the object's rotation
        Vector3 forwardDirection = transform.forward;

        // Move the object forward
        transform.position += forwardDirection * forwardSpeed * Time.deltaTime;
    }

    private void MoveObjectBackward()
    {
        // Calculate the backward direction based on the object's rotation
        Vector3 backwardDirection = -transform.forward;

        // Move the object backward
        transform.position += backwardDirection * backwardSpeed * Time.deltaTime;
    }
}
