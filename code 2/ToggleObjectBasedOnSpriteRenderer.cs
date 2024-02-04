using UnityEngine;

public class ToggleObjectBasedOnSpriteRenderer : MonoBehaviour
{
    public SpriteRenderer spriteRendererToCheck;
    public GameObject objectToToggle;

    void Update()
    {
        // Check if the sprite renderer is enabled
        bool isSpriteRendererEnabled = spriteRendererToCheck.enabled;

        // Toggle the game object based on the sprite renderer state
        objectToToggle.SetActive(isSpriteRendererEnabled);
    }
}
