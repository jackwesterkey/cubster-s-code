using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mesa : MonoBehaviour
{
    // Reference to the object or component controlling the spirit render
    public Renderer spiritRenderer;

    // Update is called once per frame
    void Update()
    {
        // Check if the spirit render is off
        if (spiritRenderer != null && !spiritRenderer.enabled)
        {
            // Load the MainMenu scene
            SceneManager.LoadScene("MainMenu");
        }
    }
}
