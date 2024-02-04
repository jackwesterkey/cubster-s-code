using UnityEngine;

public class EyesOfAPanther : MonoBehaviour
{
    private Animator animator;
    private bool isHootScheduled = false;
    private bool hasPlayedHootOnce = false;
    private int hootCount = 0;

    // Public variable to set the tag of the triggering object
    public string triggeringTag = "duck";

    // Public variable to set the maximum distance for the trigger
    public float maxTriggerDistance = 2.0f;

    private void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Find all GameObjects with the specified tag
        GameObject[] duckObjects = GameObject.FindGameObjectsWithTag(triggeringTag);

        // Check if any of the duckObjects are close enough to trigger the animation
        foreach (GameObject duckObject in duckObjects)
        {
            if (duckObject != null && Vector3.Distance(transform.position, duckObject.transform.position) <= maxTriggerDistance)
            {
                if (!isHootScheduled)
                {
                    // Play the "hoot" animation
                    animator.Play("hoot");
                    isHootScheduled = true;
                    hasPlayedHootOnce = false;
                    hootCount++;

                    // Check if the "hoot" has been played four times
                    if (hootCount >= 4)
                    {
                        // Delete the object itself (the one with this script)
                        Destroy(gameObject);
                    }
                }
            }
        }

        // Check if the "hoot" animation is done playing
        if (isHootScheduled)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Check if the normalized time is greater than or equal to 1 (fully played)
            if (stateInfo.normalizedTime >= 1f)
            {
                // Only reset the flag if the "hoot" has played at least once
                if (!hasPlayedHootOnce)
                {
                    hasPlayedHootOnce = true;
                }
                else
                {
                    // Reset the flag to allow the next "hoot" animation
                    isHootScheduled = false;

                    // Play the "New State" animation directly
                    animator.Play("New State");
                }
            }
        }
    }
}
