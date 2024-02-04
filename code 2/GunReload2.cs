using UnityEngine;
using System.Collections;

public class GunReload2 : MonoBehaviour
{
    public AmmoManager ammoManager;

    private bool isReloading = false;
    private float rotation2 = 360.0f;

    private void OnEnable()
    {
        isReloading = false; // Reset isReloading when the script is reloaded
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            StartCoroutine(ReloadCoroutine(rotation2));
        }
    }

    private IEnumerator ReloadCoroutine(float endRotation)
    {
        float duration = 1.0f;
        float t = 0f;

        float startRotation = transform.localEulerAngles.x;
        float targetRotation = startRotation + endRotation;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float currentRotation = Mathf.Lerp(startRotation, targetRotation, t);
            transform.localEulerAngles = new Vector3(currentRotation, 4.927f, 93.808f); // Set Y and Z to zero
            yield return null;
        }

        transform.localEulerAngles = new Vector3(targetRotation, 4.927f, 93.808f); // Set Y and Z to zero
        isReloading = false;

        // Call the ReloadGun method in the AmmoManager script to handle actual reloading logic
        if (ammoManager != null)
        {
            ammoManager.ReloadGun();
        }
        else
        {
            Debug.LogWarning("AmmoManager reference is not assigned in GunReload2 script.");
        }
    }
}
