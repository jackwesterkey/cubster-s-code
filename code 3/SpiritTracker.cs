using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpiritTracker : MonoBehaviour
{
    public List<SpriteRenderer> spirits;
    public Button interactableButton;

    private bool isAnySpiritTurnedOff = false;

    void Update()
    {
        // Check if any spirit is turned off
        bool currentSpiritState = IsAnySpiritTurnedOff();

        // If the state has changed, turn on the button and start the delay coroutine
        if (currentSpiritState != isAnySpiritTurnedOff)
        {
            isAnySpiritTurnedOff = currentSpiritState;
            interactableButton.interactable = isAnySpiritTurnedOff;

            // Start the coroutine to turn off the button after a delay
            StartCoroutine(TurnOffButtonAfterDelay());
        }
    }

    bool IsAnySpiritTurnedOff()
    {
        // Add your logic to determine if any spirit is turned off
        // For example, you might check if any spirit is not rendering or within a certain distance
        // Replace this with your specific conditions
        foreach (var spirit in spirits)
        {
            if (!spirit.isVisible)
            {
                return true; // At least one spirit is turned off
            }
        }

        return false; // No spirits turned off
    }

    IEnumerator TurnOffButtonAfterDelay()
    {
        // Add a delay here, for example, 2 seconds
        yield return new WaitForSeconds(2f);

        // After the delay, turn off the button
        interactableButton.interactable = false;
    }
}
