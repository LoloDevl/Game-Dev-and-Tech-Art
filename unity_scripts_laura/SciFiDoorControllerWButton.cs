using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiDoorControllerWButton : MonoBehaviour
{
    private Animator doorAnimator;
    public Transform playerTransform; // Player's transform will be fetched from the GameManager
    public float proximityDistance = 5.0f; // Distance at which player can trigger the door to open
    public float closeDistance = 7.0f; // Distance at which door will close after player moves away
    private bool isPlayerClose = false; // Flag to check if the player is close to the door
    private bool doorOpened = false; // Flag to check if the door was opened

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
        doorOpened = false;
        doorAnimator.ResetTrigger("OpenDoor");
        doorAnimator.ResetTrigger("CloseDoor");

        if (playerTransform == null)
        {
            Debug.LogError("Player Transform not set in the SciFiDoorControllerWButton script. Please check the GameManager script.");
        }
    }

    void Update()
    {
        // Check the distance between the player and the door
        float distanceToPlayer = Vector3.Distance(transform.position, GameManager.Player.transform.position);

        //Debug.Log($"Distance to Player: {distanceToPlayer}"); // Log the distance for troubleshooting

        // If the door was opened and player moves beyond the close distance, close the door
        if (doorOpened && distanceToPlayer > closeDistance)
        {
            doorAnimator.SetTrigger("CloseDoor");
            doorOpened = false; // Reset the door opened flag
        }

        isPlayerClose = distanceToPlayer < proximityDistance;

        // If the player is close and presses the "F" key, play the door open animation
        if (isPlayerClose && Input.GetKeyDown(KeyCode.F))
        {
            doorAnimator.SetTrigger("OpenDoor");
            doorOpened = true; // Mark that the door was opened
        }
    }
}

