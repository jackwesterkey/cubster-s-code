using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YourNamespace
{
    public class BackToGuns : MonoBehaviour
    {
        public List<MonoBehaviour> scriptsToEnable = new List<MonoBehaviour>(); // Changed variable name
        public List<GameObject> targetGameObjects = new List<GameObject>();
        public float enableDuration = 60f; // Changed variable name
        private bool areScriptsEnabled = false;

        private void OnEnable()
        {
            if (scriptsToEnable.Count > 0 && targetGameObjects.Count > 0)
            {
                foreach (var target in targetGameObjects)
                {
                    var deleteListener = target.AddComponent<DeleteListener>();
                    deleteListener.OnDeleted += EnableTargetScripts; // Changed method name
                }
            }
            else
            {
                Debug.LogWarning("Scripts to enable or target GameObjects are not assigned.");
            }
        }

        private void EnableTargetScripts() // Changed method name
        {
            if (!areScriptsEnabled)
            {
                // Enable the specified script(s)
                foreach (var script in scriptsToEnable)
                {
                    if (script != null)
                    {
                        script.enabled = true;
                        Debug.Log($"Script '{script.GetType().Name}' enabled on GameObject '{script.gameObject.name}' for {enableDuration} seconds.");
                    }
                }

                StartCoroutine(DisableScriptsAfterDelay()); // Changed method name
            }
        }

        private IEnumerator DisableScriptsAfterDelay() // Changed method name
        {
            yield return new WaitForSeconds(enableDuration);

            // Disable the scripts after the specified duration
            foreach (var script in scriptsToEnable)
            {
                if (script != null)
                {
                    script.enabled = false;
                    Debug.Log($"Script '{script.GetType().Name}' disabled on GameObject '{script.gameObject.name}'.");
                }
            }

            areScriptsEnabled = false;
        }
    }
}

// Rest of the code remains the same
