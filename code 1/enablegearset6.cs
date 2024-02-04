using UnityEngine;
using System.Collections.Generic;

public class enablegearset6 : MonoBehaviour
{
    // List of scripts to enable when the target GameObjects are deleted
    public List<MonoBehaviour> scriptsToEnable;

    // List of target GameObjects
    public List<GameObject> targetGameObjects;

    private void OnEnable()
    {
        // Subscribe to the OnDestroy event of the target GameObjects
        if (scriptsToEnable != null && scriptsToEnable.Count > 0 &&
            targetGameObjects != null && targetGameObjects.Count > 0)
        {
            foreach (var targetGameObject in targetGameObjects)
            {
                if (targetGameObject != null)
                {
                    // Subscribe to the OnDestroy event for each target GameObject
                    targetGameObject.AddComponent<DeleteListener>().OnDeleted += EnableTargetScripts;
                }
            }
        }
        else
        {
            Debug.LogWarning("Scripts to enable or target GameObjects are not assigned.");
        }
    }

    private void EnableTargetScripts()
    {
        // Enable the specified scripts for each target GameObject
        if (scriptsToEnable != null && scriptsToEnable.Count > 0 &&
            targetGameObjects != null && targetGameObjects.Count > 0)
        {
            foreach (var targetGameObject in targetGameObjects)
            {
                if (targetGameObject != null)
                {
                    foreach (var script in scriptsToEnable)
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
}
