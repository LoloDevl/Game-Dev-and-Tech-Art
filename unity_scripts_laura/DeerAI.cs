using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DeerAI : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float runDistance = 20f;
    public float grazingIdleTime = 5f; // Time between idle wandering points
    private Transform playerTransform;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isRunning = false;

    // Interval in seconds for how often we should look for the player
    public float playerSearchInterval = 1f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(FindPlayerWithDelay(playerSearchInterval));
    }

    void Update()
    {
        // If player has not been found yet, skip the rest of the update
        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        // Check if the deer is running and the player is far enough away to stop running
        if (isRunning && distanceToPlayer > detectionRadius)
        {
            isRunning = false;
            animator.SetBool("isSpooked", false);
            animator.SetBool("isRunning", false);
            WanderRandomly(); // Start wandering after stopping
        }
        // Check if the player is close enough to spook the deer
        else if (!isRunning && distanceToPlayer < detectionRadius)
        {
            isRunning = true;
            animator.SetBool("isSpooked", true);
            animator.SetBool("isRunning", true);
            RunFromPlayer();
        }
    }

    private IEnumerator FindPlayerWithDelay(float delay)
    {
        while (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
                break;
            }
            yield return new WaitForSeconds(delay);
        }
    }

    private void RunFromPlayer()
    {
        // Calculate direction away from player and set the destination for the NavMeshAgent
        Vector3 fleeDirection = (transform.position - playerTransform.position).normalized;
        Vector3 runTarget = transform.position + fleeDirection * runDistance;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(runTarget, out hit, runDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        else
        {
            agent.SetDestination(transform.position + fleeDirection * runDistance);
        }
    }

    private void WanderRandomly()
    {
        // Set a new random destination for the deer to idle
        Vector3 randomDirection = Random.insideUnitSphere * runDistance;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, runDistance, 1))
        {
            finalPosition = hit.position;
        }
        agent.SetDestination(finalPosition);
    }
}

