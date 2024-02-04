using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public List<GameObject> objectsToDisable = new List<GameObject>();
    public List<SpriteRenderer> turnOffSpriteRenderers = new List<SpriteRenderer>();
    public List<GameObject> objectsToDetect = new List<GameObject>();

    private int currentIndex = 0; // Keeps track of the current index in the list

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves a GameObject in the list
        if (objectsToDetect.Contains(collision.gameObject))
        {
            // Turn off the current SpriteRenderer in the list on the current GameObject
            if (currentIndex < turnOffSpriteRenderers.Count)
            {
                SpriteRenderer spriteRenderer = turnOffSpriteRenderers[currentIndex];
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = false;
                }
                currentIndex++;
            }

            // Disable each GameObject in the list
            foreach (GameObject objToDisable in objectsToDisable)
            {
                if (objToDisable != null)
                {
                    objToDisable.SetActive(false);
                }
            }
        }
    }
}
