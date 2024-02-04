using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    public Button firstButton;
    public Button secondButton;

    void Start()
    {
        // Assuming both buttons are initially interactable
        firstButton.interactable = true;
        secondButton.interactable = true;

        // Add click listeners to the buttons
        firstButton.onClick.AddListener(OnFirstButtonClick);
        secondButton.onClick.AddListener(OnSecondButtonClick);
    }

    void OnFirstButtonClick()
    {
        // Disable interaction for both buttons immediately
        firstButton.interactable = false;
        secondButton.interactable = false;

        // Enable interaction for the second button after one minute
        StartCoroutine(EnableSecondButtonAfterDelay(20f)); // 30 seconds = 30 seconds
    }

    void OnSecondButtonClick()
    {
        // Disable interaction for both buttons immediately
        firstButton.interactable = false;
        secondButton.interactable = false;

        // Enable interaction for the first button immediately
        firstButton.interactable = true;

        // Enable interaction for the second button after one minute
        StartCoroutine(EnableSecondButtonAfterDelay(20f)); // 30 seconds = 30 seconds
    }

    IEnumerator EnableSecondButtonAfterDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        // Enable interaction for the second button after the delay
        secondButton.interactable = true;
    }
}
