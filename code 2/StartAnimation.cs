using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check if the animator is enabled
        if (animator.enabled)
        {
            // Turn off Solo mode by setting the layer weight to 1
            animator.SetLayerWeight(layerIndex, 1f);

            // Get the next state index (assuming it's the next one in the layer)
            int nextStateIndex = stateInfo.fullPathHash + 1;

            // Force a transition to the next state
            animator.Play(nextStateIndex, layerIndex, 0f);
        }
    }

    // Other methods are left commented out
}
