using System.Collections.Generic;
using UnityEngine;

public class DisableScriptOnDelete : MonoBehaviour
{
    // Reference to the script(s) you want to disable
    public List<MonoBehaviour> scriptsToDisable = new List<MonoBehaviour>();

    // Reference to the target GameObject(s)
    public List<GameObject> targetGameObjects = new List<GameObject>();

    private void OnEnable()
    {
        // Subscribe to the OnDestroy event of the target GameObject(s)
        if (scriptsToDisable.Count > 0 && targetGameObjects.Count > 0)
        {
            foreach (var target in targetGameObjects)
            {
                var deleteListener = target.AddComponent<DeleteListener>();
                deleteListener.OnDeleted += DisableTargetScripts;
            }
        }
        else
        {
            Debug.LogWarning("Scripts to disable or target GameObjects are not assigned.");
        }
    }

    private void DisableTargetScripts()
    {
        // Disable the specified script(s)
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

// This script listens for the OnDestroy event of the target GameObject(s)
public class DeleteListener : MonoBehaviour
{
    public delegate void DeleteAction();
    public event DeleteAction OnDeleted;

    private void OnDestroy()
    {
        OnDeleted?.Invoke();
    }
}
