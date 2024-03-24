using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotAI : MonoBehaviour
{
    public List<Transform> waypoints; // List of waypoints
    public float speed = 5f;          // Flying speed
    public Animator animator;         // Reference to the Animator component

    private int currentWaypointIndex = 0;
    private bool isFlying = false;

    private void Start()
    {
        if (waypoints.Count > 0)
        {
            // Start with flying
            isFlying = true;
            animator.SetTrigger("Fly");
            MoveToNextWaypoint();
        }
    }

    private void Update()
    {
        if (isFlying)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            Vector3 direction = (targetWaypoint.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetWaypoint.position) < 1f)
            {
                // Reached the waypoint
                isFlying = false;
                animator.SetTrigger("Land");
                // Call coroutine to wait for some time before taking off again
                StartCoroutine(WaitAndFlyAgain());
            }
        }
    }

    private void MoveToNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        isFlying = true;
        animator.SetTrigger("Fly");
    }

    IEnumerator WaitAndFlyAgain()
    {
        yield return new WaitForSeconds(5); // Wait for 5 seconds
        animator.SetTrigger("TakeOff");
        yield return new WaitForSeconds(1); // Wait for TakeOff animation
        MoveToNextWaypoint();
    }
}
