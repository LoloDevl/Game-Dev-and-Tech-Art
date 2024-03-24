using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExtractorWoodController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Initialize the NPC's route
        MoveToNextWaypoint();

        // Animation code (from the previous example)
        int totalAnimations = 10;
        int randomAnimation = Random.Range(1, totalAnimations + 1);
        animator.SetTrigger($"PlayAnimation{randomAnimation}");
    }

    private void Update()
    {
        // If NPC is close to the waypoint, move to the next one
        if (navMeshAgent.remainingDistance < 0.5f && !navMeshAgent.pathPending)
        {
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        // Set the next destination for the NavMeshAgent
        navMeshAgent.destination = waypoints[currentWaypointIndex].position;

        // Update the waypoint index
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}
