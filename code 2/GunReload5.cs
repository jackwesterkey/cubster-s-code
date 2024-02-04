using UnityEngine;
using System.Collections;

public class GunReload5 : MonoBehaviour
{
    public AmmoManager4 ammoManager4; // Reference to the AmmoManager4 script

    private bool isRotating = false;
    private float rotation2 = 360.0f; // Assuming 180 degrees for the second rotation

    private void OnEnable()
    {
        isRotating = false; // Reset isRotating when the script is reloaded
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isRotating)
        {
            isRotating = true;
            StartCoroutine(RotateAndReloadCoroutine(rotation2));
        }
    }

    private IEnumerator RotateAndReloadCoroutine(float endRotation)
    {
        float rotationDuration = 1.0f; // Adjust this value to control the duration of the rotation
        float reloadDuration = 1.0f;   // Adjust this value to control the duration of the reloading

        float rotationT = 0f;
        float reloadT = 0f;

        float startRotation = transform.localEulerAngles.z;
        float targetRotation = startRotation + endRotation;

        while (rotationT < 1f)
        {
            rotationT += Time.deltaTime / rotationDuration;
            float currentRotation = Mathf.Lerp(startRotation, targetRotation, rotationT);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, currentRotation);
            yield return null;
        }

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, targetRotation);
        isRotating = false;

        // Call the ReloadGun method in the AmmoManager4 script to handle actual reloading logic
        StartCoroutine(ReloadCoroutine(reloadDuration));
    }

    private IEnumerator ReloadCoroutine(float duration)
    {
        float t = 0f;

        // Call the ReloadGun method in the AmmoManager4 script to handle actual reloading logic
        if (ammoManager4 != null)
        {
            ammoManager4.ReloadGun();
        }
        else
        {
            Debug.LogWarning("AmmoManager4 reference is not assigned in GunReload3 script.");
        }

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            yield return null;
        }
    }
}
