using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDroid012RandomAnimationController : MonoBehaviour

{
    private Animator animator;
    private BoxCollider groundCollider;
    private Vector3 nextPosition;

    private string[] animationStates = { "Idle", "Jump", "Run", "FootTap", "FootScratch" };

    private void Start()
    {
        animator = GetComponent<Animator>();
        groundCollider = GameObject.FindWithTag("Ground").GetComponent<BoxCollider>();

        StartCoroutine(MoveAndAnimate());
    }

    private IEnumerator MoveAndAnimate()
    {
        while (true)
        {
            // Calculate a random position within the ground collider
            nextPosition = new Vector3(
                Random.Range(groundCollider.bounds.min.x, groundCollider.bounds.max.x),
                transform.position.y,
                Random.Range(groundCollider.bounds.min.z, groundCollider.bounds.max.z)
            );

            // Start moving towards the next position
            while (Vector3.Distance(transform.position, nextPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, 2f * Time.deltaTime);
                animator.SetTrigger("Run");
                yield return null;
            }

            // Play a random animation
            PlayRandomAnimation();

            // Wait for a few seconds before moving to the next random position
            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

    private void PlayRandomAnimation()
    {
        string selectedAnimation = animationStates[Random.Range(0, animationStates.Length)];
        animator.SetTrigger(selectedAnimation);
    }
}

