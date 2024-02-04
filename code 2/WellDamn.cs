using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YourNamespace
{
    public class WellDamn : MonoBehaviour
    {
        // Reference to the script(s) you want to disable
        public List<MonoBehaviour> scriptsToDisable = new List<MonoBehaviour>();

        // Reference to the target GameObject(s)
        public List<GameObject> targetGameObjects = new List<GameObject>();

        // Time in seconds for which the script should remain disabled
        public float disableDuration = 60f; // 1 minute by default

        private bool isScriptsDisabled = false;

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
            if (!isScriptsDisabled)
            {
                // Disable the specified script(s)
                foreach (var script in scriptsToDisable)
                {
                    if (script != null)
                    {
                        script.enabled = false;
                        Debug.Log($"Script '{script.GetType().Name}' disabled on GameObject '{script.gameObject.name}' for {disableDuration} seconds.");
                    }
                }

                // Start a coroutine to re-enable the scripts after a certain duration
                StartCoroutine(EnableScriptsAfterDelay());
            }
        }

        private IEnumerator EnableScriptsAfterDelay()
        {
            yield return new WaitForSeconds(disableDuration);

            // Re-enable the scripts after the specified duration
            foreach (var script in scriptsToDisable)
            {
                if (script != null)
                {
                    script.enabled = true;
                    Debug.Log($"Script '{script.GetType().Name}' re-enabled on GameObject '{script.gameObject.name}'.");
                }
            }

            // Set the flag to indicate that scripts are no longer disabled
            isScriptsDisabled = false;
        }
    }
}

namespace YourNamespace
{
    // This script listens for the OnDestroy event of the target GameObject(s)
    public class DeleteListener : MonoBehaviour
    {
        public delegate void DeleteAction();
        public event DeleteAction OnDeleted;

        // Rename OnDestroy to avoid conflicts
        private void OnDestroy()
        {
            OnDeleted?.Invoke();
        }
    }
}
