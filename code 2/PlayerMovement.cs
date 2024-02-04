using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Adjust the speed as needed.

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontalInput = 0;
        float verticalInput = 0;

        if (Input.GetKey(KeyCode.S))
        {
            verticalInput = 1;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            verticalInput = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = -1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = 1;
        }

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }
}
