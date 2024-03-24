using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomAnimationLooper : MonoBehaviour
{
    private Animator animator;
    private BoxCollider groundCollider;
    private Vector3 groundSize;
    private Vector3 groundCenter;
    
    public float boundaryMargin = 0.5f; // Margin to avoid playing animations too close to the edge.

    private void Start()
    {
        animator = GetComponent<Animator>();
        groundCollider = GameObject.FindWithTag("Ground").GetComponent<BoxCollider>();
        groundSize = groundCollider.size;
        groundCenter = groundCollider.transform.position;

        StartCoroutine(PlayRandomAnimations());
    }

    private IEnumerator PlayRandomAnimations()
    {
        while (true)
        {
            Vector3 position = transform.position;

            // Ensure object is within boundaries
            if (position.x < groundCenter.x - groundSize.x / 2 + boundaryMargin || position.x > groundCenter.x + groundSize.x / 2 - boundaryMargin || 
                position.z < groundCenter.z - groundSize.z / 2 + boundaryMargin || position.z > groundCenter.z + groundSize.z / 2 - boundaryMargin)
            {
                // If out of bounds, play idle animation
                animator.SetTrigger("Idle");
            }
            else
            {
                int randomTrigger = Random.Range(1, 7);  // Assuming you have triggers set for each animation (1 for the first animation, 2 for the second, etc.)
                animator.SetTrigger(randomTrigger.ToString());
            }

            float waitTime = Random.Range(2, 5); // Random delay between animations
            yield return new WaitForSeconds(waitTime);
        }
    }
}
