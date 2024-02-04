using UnityEngine;

public class EnableObject : MonoBehaviour
{
    public GameObject objectToActivate;

    private void Start()
    {
        // Ensure the object is initially inactive
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No target object assigned!");
        }
    }

    public void OnButtonClick()
    {
        // Toggle the active state of the game object
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(!objectToActivate.activeSelf);
        }
        else
        {
            Debug.LogWarning("No target object assigned!");
        }
    }
}
