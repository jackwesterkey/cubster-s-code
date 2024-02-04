using UnityEngine;

public class ShowMouseCursor : MonoBehaviour
{
    void Start()
    {
        // Make the mouse cursor visible and free to move
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        // You can add other logic here if needed
    }
}
