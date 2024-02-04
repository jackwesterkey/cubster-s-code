using UnityEngine;
using System.Collections;

public class GunReload : MonoBehaviour
{
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
            StartCoroutine(RotateGunCoroutine(rotation2));
        }
    }

    private IEnumerator RotateGunCoroutine(float endRotation)
    {
        float duration = 1.0f; // Adjust this value to control the duration of the rotation
        float t = 0f;

        float startRotation = transform.localEulerAngles.z;
        float targetRotation = startRotation + endRotation;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float currentRotation = Mathf.Lerp(startRotation, targetRotation, t);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, currentRotation);
            yield return null;
        }

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, targetRotation);
        isRotating = false;
    }
}
