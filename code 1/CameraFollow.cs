using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the object the camera will follow
    public float smoothSpeed = 0.125f; // Adjust this to control the smoothness of the camera movement
    public Vector3 offset; // Offset of the camera from the target

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target); // Make the camera look at the target
        }
    }
}
