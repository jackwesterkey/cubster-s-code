using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private bool isGrappling = false;
    private Rigidbody playerRigidbody; // Reference to the player's Rigidbody

    private RigidbodyConstraints originalConstraints;

    // Audio variables
    private AudioSource audioSource;
    public AudioClip grappleSound;  // Assign your grapple sound in the Unity Editor

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        playerRigidbody = player.GetComponent<Rigidbody>(); // Get the player's Rigidbody

        // Store the original constraints
        originalConstraints = playerRigidbody.constraints;

        // Ensure an AudioSource is attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
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
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;

            // Lock player movement by setting the Rigidbody constraints
            playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            isGrappling = true;

            // Play grapple sound when the grappling hook is attached
            PlayGrappleSound();
        }
    }

    void StopGrapple()
    {
        // Restore the original constraints
        playerRigidbody.constraints = originalConstraints;

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
