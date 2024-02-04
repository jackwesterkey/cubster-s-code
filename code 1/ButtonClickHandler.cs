using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonClickHandler : MonoBehaviour
{
    public List<Animator> targetAnimators; // List of Animators on other GameObjects
    private bool isAnimationPlayed = false;

    // Attach this method to the UI Button's onClick event in the Unity Editor
    public void OnButtonClick()
    {
        // If the animation has already been played, return
        if (isAnimationPlayed)
        {
            return;
        }

        // Play the "not" animation on each Animator in the list
        foreach (Animator animator in targetAnimators)
        {
            animator.Play("not");
        }

        // Invoke a method to transition back to "New State" after a delay
        Invoke("TransitionToNewState", targetAnimators[0].GetCurrentAnimatorStateInfo(0).length);

        // Set the flag to indicate that the animation has been played
        isAnimationPlayed = true;
    }

    // Method to transition back to "New State"
    private void TransitionToNewState()
    {
        // Transition each Animator in the list back to "New State"
        foreach (Animator animator in targetAnimators)
        {
            animator.Play("New State");
        }

        isAnimationPlayed = false; // Reset the flag
    }
}
