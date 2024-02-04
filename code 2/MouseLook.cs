using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;  // Default sensitivity
    public float leftRightSensitivity = 200f;  // Sensitivity for left-right movement
    public float upDownSensitivity = 200f;  // Sensitivity for up-down movement
    public Transform player;

    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;

    // Toggled cursor lock
    private bool cursorLocked = true;

    // Invert vertical camera movement
    public bool invertVertical = false;

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLocked = !cursorLocked;
            LockCursor();
        }

        if (cursorLocked)
        {
            mouseX = Input.GetAxis("Mouse X") * leftRightSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * upDownSensitivity * Time.deltaTime;

            if (invertVertical)
                mouseY = -mouseY;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Left-to-right camera movement added here
            player.Rotate(Vector3.up * mouseX);
        }
    }

    void LockCursor()
    {
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !cursorLocked;
    }
}
