using UnityEngine;
using System.Collections.Generic;

public class Cheese : MonoBehaviour
{
    // List of target GameObjects
    public List<GameObject> targetGameObjects;

    private void Start()
    {
        // Attach a custom component to listen for GameObject destruction
        foreach (var targetGameObject in targetGameObjects)
        {
            if (targetGameObject != null)
            {
                OnCheeseDestroyed listener = targetGameObject.AddComponent<OnCheeseDestroyed>();
                listener.OnDestroyed += EnableTargetScripts;
            }
        }
    }

    private void EnableTargetScripts()
    {
        // Enable all scripts on all GameObjects with the "cheese" tag
        GameObject[] cheeseObjects = GameObject.FindGameObjectsWithTag("cheese");

        foreach (var cheeseObject in cheeseObjects)
        {
            if (cheeseObject != null)
            {
                MonoBehaviour[] scripts = cheeseObject.GetComponents<MonoBehaviour>();

                foreach (var script in scripts)
                {
                    if (script != null)
                    {
                        script.enabled = true;
                        Debug.Log($"Script '{script.GetType().Name}' enabled on GameObject '{script.gameObject.name}'.");
                    }
                }
            }
        }
    }
}

// Custom component to notify when a GameObject with the tag "cheese" is destroyed
public class OnCheeseDestroyed : MonoBehaviour
{
    public event System.Action OnDestroyed;

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}
