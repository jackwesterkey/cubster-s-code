using UnityEngine;

public class HoldRightClick: MonoBehaviour
{
    public GameObject objectToEnable;

    void Update()
    {
        // Check if right mouse button is being held down
        if (Input.GetMouseButton(1)) // 1 corresponds to the right mouse button
        {
            // Enable the game object while right click is held down
            objectToEnable.SetActive(true);
        }
        else
        {
            // Disable the game object when right click is not held down
            objectToEnable.SetActive(false);
        }
    }
}
