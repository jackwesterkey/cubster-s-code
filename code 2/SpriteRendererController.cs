using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteRendererController : MonoBehaviour
{
    public List<SpriteRenderer> triggerSpriteRenderers;
    public List<SpriteRenderer> turnOnSpriteRenderers;
    public List<SpriteRenderer> turnOffSpriteRenderers;

    public Button uiButton; // Reference to your UI Button

    void Start()
    {
        // Make sure the initial state is set correctly
        SetSpriteRenderersState(turnOnSpriteRenderers, false);
        SetSpriteRenderersState(turnOffSpriteRenderers, true);
    }

    void Update()
    {
        bool anyTriggerActive = false;

        // Check if any of the trigger sprite renderers are active
        foreach (var triggerSpriteRenderer in triggerSpriteRenderers)
        {
            if (triggerSpriteRenderer.enabled)
            {
                anyTriggerActive = true;
                break;
            }
        }

        // If any trigger is active, turn on the specified sprite renderers
        SetSpriteRenderersState(turnOnSpriteRenderers, anyTriggerActive);

        // Turn off the specified sprite renderers
        SetSpriteRenderersState(turnOffSpriteRenderers, !anyTriggerActive);

        // Destroy the UI Button if the last item in turnOnSpriteRenderers is active
        if (anyTriggerActive && IsLastItemActive(turnOnSpriteRenderers) && uiButton != null)
        {
            Destroy(uiButton.gameObject);
        }
    }

    void SetSpriteRenderersState(List<SpriteRenderer> spriteRenderers, bool state)
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            // Check if the SpriteRenderer component is not null before modifying it
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = state;
            }
        }
    }

    bool IsLastItemActive(List<SpriteRenderer> spriteRenderers)
    {
        // Check if the last item in the list is active
        if (spriteRenderers.Count > 0)
        {
            return spriteRenderers[spriteRenderers.Count - 1].enabled;
        }
        return false;
    }
}
