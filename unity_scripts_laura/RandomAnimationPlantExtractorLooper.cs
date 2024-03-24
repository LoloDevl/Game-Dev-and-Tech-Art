using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class RandomAnimationPlantExtractorLooper : MonoBehaviour
{
    private Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();

        // Start the cycle of animations
        StartCoroutine(HandleAnimations());
    }

    private IEnumerator HandleAnimations()
    {
        while (true)
        {
            // Always start with Idle animation
            animator.SetTrigger("Idle");

            // Let the idle animation play for a while
            yield return new WaitForSeconds(Random.Range(2, 5));

            // Play the pick_up_plant animation
            animator.SetTrigger("pick_up_plant");

            // Wait for some time before going back to idle
            yield return new WaitForSeconds(Random.Range(2, 5));
        }
    }
}

