using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
{
    public float rotationSpeed = 60.0f; // Adjust the speed of rotation in degrees per second
    public float positionSpeed = 5.0f; // Speed for moving the object
    public float minY = 0.0f; // Minimum Y position
    public float maxY = 10.0f; // Maximum Y position
    public float minz = 0.0f; // Minimum Z position
    public float maxz = 10.0f; // Maximum Z position
    public float minx = 0.0f; // Minimum X position
    public float maxx = 10.0f; // Maximum X position

    private int directionY = 1; // 1 for moving up, -1 for moving down
    private int directionX = 1; // 1 for moving right, -1 for moving left
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
        newPosition.y += positionSpeed * directionY * Time.deltaTime;

        // Check if the object has reached the maximum or minimum Y position
        if (newPosition.y >= maxY)
        {
            newPosition.y = maxY;
            directionY = -1; // Change direction to move down
        }
        else if (newPosition.y <= minY)
        {
            newPosition.y = minY;
            directionY = 1; // Change direction to move up
        }

        // Move the object right and left along the X-axis
        newPosition.x += positionSpeed * directionX * Time.deltaTime;

        // Check if the object has reached the maximum or minimum X position
        if (newPosition.x >= maxx)
        {
            newPosition.x = maxx;
            directionX = -1; // Change direction to move left
        }
        else if (newPosition.x <= minx)
        {
            newPosition.x = minx;
            directionX = 1; // Change direction to move right
        }

        // Set the Z position within the specified range
        newPosition.z = Mathf.Clamp(newPosition.z, minz, maxz);

        transform.position = newPosition;
    }
}
