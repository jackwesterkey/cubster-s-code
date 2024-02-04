using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sandy2 : MonoBehaviour
{
    private Animator animator;
    private bool hasPlayed = false;

    void Start()
    {
        // Assuming your Animator component is attached to the same GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the "sewwe32" animation has been played and has not been processed yet
        if (!hasPlayed && animator.GetCurrentAnimatorStateInfo(0).IsName("sewwe32") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            // Set the flag to true to avoid processing this again
            hasPlayed = true;

            // Load the "fin" scene
            SceneManager.LoadScene("fin");
        }
    }
}
