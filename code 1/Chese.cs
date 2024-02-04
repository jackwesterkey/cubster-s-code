using System.Collections.Generic;
using UnityEngine;

public class Chese : MonoBehaviour
{
    public SpriteRenderer turnOnSpriteRenderer;
    public List<SpriteRenderer> turnOffSpriteRenderers;
    public GameObject otherGameObjectToDisable;

    private void OnDestroy()
    {
        // Check if the turnOnSpriteRenderer is not null before trying to access it
        if (turnOnSpriteRenderer != null)
        {
            turnOnSpriteRenderer.enabled = true;
        }

        // Iterate through the list of turnOffSpriteRenderers and disable them
        foreach (var spriteRenderer in turnOffSpriteRenderers)
        {
            // Check if the spriteRenderer is not null before trying to access it
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
        }

        // Check if the otherGameObjectToDisable is not null before trying to access it
        if (otherGameObjectToDisable != null)
        {
            // Disable the other game object
            otherGameObjectToDisable.SetActive(false);
        }
    }
}
