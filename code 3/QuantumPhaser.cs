using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumPhaser : MonoBehaviour
{
    public float objectScript; // Public float to hold the script reference

    private bool isVisible = true; // Keeps track of object's visibility
    private Behaviour scriptBehavior;

    private void Start()
    {
        // Get the script behavior component on the object
        scriptBehavior = GetComponent<Behaviour>(); // Adjust the script type if needed
    }

    void Update()
    {
        // Check if the '1' key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Deactivate (hide) the object and disable its script behavior
            SetObjectVisibility(false);
        }

        // Check if the '2' key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Activate (show) the object and enable its script behavior
            SetObjectVisibility(true);
        }
    }

    void SetObjectVisibility(bool visible)
    {
        isVisible = visible;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = visible; // Set the object's visibility
        }

        // Enable or disable the object's script behavior based on visibility
        scriptBehavior.enabled = visible;
    }
}
