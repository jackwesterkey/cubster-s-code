using UnityEngine;
using System.Collections.Generic;

public class DisableGearSet : MonoBehaviour
{
    // List of scripts to disable when the target GameObjects are deleted
    public List<MonoBehaviour> scriptsToDisable;

    // List of target GameObjects
    public List<GameObject> targetGameObjects;

    private void OnEnable()
    {
        // Subscribe to the OnDestroy event of the target GameObjects
        if (scriptsToDisable != null && scriptsToDisable.Count > 0 &&
            targetGameObjects != null && targetGameObjects.Count > 0)
        {
            foreach (var targetGameObject in targetGameObjects)
            {
                if (targetGameObject != null)
                {
                    // Subscribe to the OnDestroy event for each target GameObject
                    targetGameObject.AddComponent<DeleteListener>().OnDeleted += DisableTargetScripts;
                }
            }
        }
        else
        {
            Debug.LogWarning("Scripts to disable or target GameObjects are not assigned.");
        }
    }

    private void DisableTargetScripts()
    {
        // Disable the specified scripts for each target GameObject
        if (scriptsToDisable != null && scriptsToDisable.Count > 0 &&
            targetGameObjects != null && targetGameObjects.Count > 0)
        {
            foreach (var targetGameObject in targetGameObjects)
            {
                if (targetGameObject != null)
                {
                    foreach (var script in scriptsToDisable)
                    {
                        if (script != null)
                        {
                            script.enabled = false;
                            Debug.Log($"Script '{script.GetType().Name}' disabled on GameObject '{script.gameObject.name}'.");
                        }
                    }
                }
            }
        }
    }
}
