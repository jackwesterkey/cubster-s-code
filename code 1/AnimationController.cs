using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public GameObject objectToToggle;
    public Button firstButton;
    public Button secondButton;

    private bool isWaitingForFirstButton = true;

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator component not assigned.");
            return;
        }

        // Start with cilp 1
        PlayCilp1();

        // Add listeners to the buttons
        firstButton.onClick.AddListener(OnFirstButtonClick);
        secondButton.onClick.AddListener(OnSecondButtonClick);
    }

    void PlayCilp1()
    {
        animator.Play("cilp 1");
        isWaitingForFirstButton = true;
    }

    void PlayCilp2()
    {
        animator.Play("cilp 2");
        isWaitingForFirstButton = false;
    }

    void PlayCilp3()
    {
        animator.Play("cilp 3");
    }

    // Animation event method called during cilp 3
    void OnCilp3AnimationEvent()
    {
        // Turn off the specified GameObject (SpriteRenderer) in cilp 3
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(false);
        }
    }

    public void OnFirstButtonClick()
    {
        if (isWaitingForFirstButton)
        {
            PlayCilp2();
        }
        else
        {
            // Do nothing or provide feedback that the button press is invalid
        }
    }

    public void OnSecondButtonClick()
    {
        // Transition from cilp 1 to cilp 3
        PlayCilp3();
    }
}
