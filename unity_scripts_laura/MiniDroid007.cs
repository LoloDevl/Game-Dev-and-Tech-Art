using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Required for the NavMesh components

using UnityEngine;
using UnityEngine.AI;

public class MiniDroid007 : MonoBehaviour
{
    public Animator animator; // Reference to the animator component.
    public float detectionRadius = 5.0f; // Radius within which the droids detect the player.
    public float blockDistance = 1.0f; // Distance at which the droid will stop in front of the player.
    public float staircaseDistance = 10.0f; // Distance from the foot of the staircase where blocking should occur.
    
    private NavMeshAgent agent; // Reference to the NavMeshAgent component.
    private Transform playerTransform; // Cache the player's transform for efficiency.

    // Method to spawn droids on the NavMesh
    public static MiniDroid007 SpawnDroid(Vector3 position, Quaternion rotation, MiniDroid007 droidPrefab)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 1.0f, NavMesh.AllAreas))
        {
            MiniDroid007 droid = Instantiate(droidPrefab, hit.position, rotation);
            droid.Initialize(); // Call the initialization method
            return droid;
        }
        else
        {
            Debug.LogError("Failed to place the droid on the NavMesh.");
            return null;
        }
    }

    // Initialization method for the MiniDroid007
    public void Initialize()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (GameManager.Player != null)
        {
            playerTransform = GameManager.Player.transform;
        }
        else
        {
            Debug.LogError("The Player reference is not set in the GameManager.");
        }
    }
}

