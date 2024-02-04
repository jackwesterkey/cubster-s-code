using UnityEngine;
using System.Collections.Generic;

public class Duck : MonoBehaviour
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
                OnDuckDestroyed listener = targetGameObject.AddComponent<OnDuckDestroyed>();
                listener.OnDestroyed += EnableTargetScripts;
            }
        }
    }

    private void EnableTargetScripts()
    {
        // Enable all scripts on all GameObjects with the "duck" tag
        GameObject[] duckObjects = GameObject.FindGameObjectsWithTag("duck");

        foreach (var duckObject in duckObjects)
        {
            if (duckObject != null)
            {
                MonoBehaviour[] scripts = duckObject.GetComponents<MonoBehaviour>();

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

// Custom component to notify when a GameObject with the tag "duck" is destroyed
public class OnDuckDestroyed : MonoBehaviour
{
    public event System.Action OnDestroyed;

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}
