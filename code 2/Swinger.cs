using UnityEngine;

public class Swinger : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private bool isGrappling = false;

    // Audio variables
    private AudioSource audioSource;
    public AudioClip grappleSound;  // Assign your grapple sound in the Unity Editor

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>(); // Assumes the AudioSource is on the same GameObject as this script
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isGrappling)
            {
                StartGrapple();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isGrappling)
            {
                StopGrapple();
            }
        }

        // Check if the LineRenderer is not connected and stop the audio
        if (!IsLineConnected() && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    private Vector3 currentGrapplePosition;

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;

            // Adjust the connectedAnchor to be slightly above the hit point
            joint.connectedAnchor = grapplePoint + Vector3.up * 0.5f;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            // Increase maxDistance to allow for more freedom in movement
            joint.maxDistance = distanceFromPoint * 2f;
            joint.minDistance = distanceFromPoint * 0.25f;

            // Set a higher massScale to reduce responsiveness to changes in direction
            joint.massScale = 5.0f; // Experiment with this value

            // Set a lower spring value to reduce interference with player movement
            joint.spring = 0.0f;
            joint.damper = 22200.0f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;

            isGrappling = true;

            // Play grapple sound when the grappling hook is attached
            PlayGrappleSound();
        }
    }

    void StopGrapple()
    {
        isGrappling = false;
        lr.positionCount = 0;
        Destroy(joint);

        // Stop the audio when the grappling hook is released
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    void PlayGrappleSound()
    {
        // Play grapple sound if not already playing
        if (audioSource != null && grappleSound != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(grappleSound); // Assumes that the AudioSource is configured with the desired audio clip
        }
    }

    public bool IsGrappling()
    {
        return isGrappling;
    }

    // Function to check if the LineRenderer is connected to an object
    bool IsLineConnected()
    {
        return lr.positionCount > 0;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
