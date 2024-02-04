using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    public float rotationSpeed = 5.0f;

    void Start()
    {
        // Lock and hide the cursor at the start of the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the camera based on mouse input
        transform.Rotate(Vector3.up * mouseX * rotationSpeed);
        transform.Rotate(Vector3.left * mouseY * rotationSpeed);

        // Limit the vertical rotation to prevent flipping
        float currentXRotation = transform.eulerAngles.x;
        if (currentXRotation > 80 && currentXRotation < 180)
        {
            currentXRotation = 80;
        }
        else if (currentXRotation > 180 && currentXRotation < 280)
        {
            currentXRotation = 280;
        }

        // Apply the limited rotation
        transform.eulerAngles = new Vector3(currentXRotation, transform.eulerAngles.y, 0);
    }
}
