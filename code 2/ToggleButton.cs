using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        // Get the Button component attached to the GameObject
        button = GetComponent<Button>();

        // Add a listener to the button click event
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        // Disable the button interactability
        button.interactable = false;

        // Perform other actions if needed

        // You can re-enable the button interactability after a certain duration if desired
        // Invoke("EnableButtonInteractability", 2f);
    }

    void EnableButtonInteractability()
    {
        // Enable the button interactability after a certain duration
        button.interactable = true;
    }
}
