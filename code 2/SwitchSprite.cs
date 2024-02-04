using UnityEngine;

public class SwitchSprite : MonoBehaviour
{
    // Array of GameObjects with SpriteRenderer components
    public GameObject[] spriteObjects;

    // Index to keep track of the current GameObject in the array
    private int currentIndex = 0;

    // Reference to the SpriteRenderer component
    private SpriteRenderer currentSpriteRenderer;

    // Set up references and initial state
    private void Start()
    {
        // Find the SpriteRenderer component on the specified GameObject
        currentSpriteRenderer = spriteObjects[currentIndex].GetComponent<SpriteRenderer>();

        // Check if the SpriteRenderer is found
        if (currentSpriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the specified GameObject.");
        }

        // Initialize the visibility state based on the first GameObject
        currentSpriteRenderer.enabled = true;

        // Turn off other GameObjects
        for (int i = 0; i < spriteObjects.Length; i++)
        {
            if (i != currentIndex)
            {
                spriteObjects[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    // Attach this method to your UI Button's OnClick event in the Unity Editor
    public void OnButtonClick()
    {
        // Turn off the current SpriteRenderer
        currentSpriteRenderer.enabled = false;

        // Move to the next GameObject in the array
        currentIndex = (currentIndex + 1) % spriteObjects.Length;

        // Find the SpriteRenderer component on the next GameObject
        currentSpriteRenderer = spriteObjects[currentIndex].GetComponent<SpriteRenderer>();

        // Check if the SpriteRenderer is found
        if (currentSpriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the specified GameObject.");
        }

        // Turn on the next SpriteRenderer
        currentSpriteRenderer.enabled = true;
    }
}
