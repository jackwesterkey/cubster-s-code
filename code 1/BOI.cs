using UnityEngine;
using UnityEngine.UI;

public class BOI : MonoBehaviour
{
    private Button myButton;

    private void Start()
    {
        // Get the Button component on the GameObject
        myButton = GetComponent<Button>();

        if (myButton != null)
        {
            // Disable the button when the game starts
            myButton.interactable = false;
        }
        else
        {
            // If there is no Button component, log an error
            Debug.LogError("Button component not found on the GameObject.");
        }
    }
}
