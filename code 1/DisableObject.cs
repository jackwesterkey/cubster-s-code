using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public GameObject objectToDeactivate; // Renamed variable for clarity

    private void Start()
    {
        // Ensure the object is initially active
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No target object assigned!");
        }
    }

    public void OnButtonClick()
    {
        // Toggle the active state of the game object
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(!objectToDeactivate.activeSelf);
        }
        else
        {
            Debug.LogWarning("No target object assigned!");
        }
    }
}
