using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hit : MonoBehaviour
{
    public List<GameObject> gameObjectsToDelete;
    private int currentIndex = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        // Initialize gameObjectsToDelete here if needed
        // gameObjectsToDelete = new List<GameObject>();
    }

    public void OnButtonClick()
    {
        if (gameObjectsToDelete == null || gameObjectsToDelete.Count == 0)
        {
            Debug.LogWarning("No GameObjects assigned to the list!");
            return;
        }

        // Destroy the current GameObject
        if (currentIndex < gameObjectsToDelete.Count)
        {
            Destroy(gameObjectsToDelete[currentIndex]);
            currentIndex++;

            // Reset to the first GameObject if we reach the end of the list
            if (currentIndex >= gameObjectsToDelete.Count)
            {
                currentIndex = 0;
            }
        }
        else
        {
            Debug.LogWarning("All GameObjects are already destroyed!");
        }
    }
}
