using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    public float rotationSpeed = 60.0f; // Adjust the speed of rotation in degrees per second
    public float positionSpeed = 5.0f; // Speed for moving the object
    public float minY = 0.0f; // Minimum Y position
    public float maxY = 10.0f; // Maximum Y position

    private int direction = 1; // 1 for moving up, -1 for moving down
    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotate the object around its Z-axis (forward/backward) at a constant speed
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // Move the object up and down along the Y-axis
        Vector3 newPosition = transform.position;
        newPosition.y += positionSpeed * direction * Time.deltaTime;

        // Check if the object has reached the maximum or minimum Y position
        if (newPosition.y >= maxY)
        {
            newPosition.y = maxY;
            direction = -1; // Change direction to move down
        }
        else if (newPosition.y <= minY)
        {
            newPosition.y = minY;
            direction = 1; // Change direction to move up
        }

        transform.position = newPosition;
    }
}