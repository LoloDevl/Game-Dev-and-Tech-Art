using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoorController : MonoBehaviour
{
    private Animator doorAnimator;
    public float activationDistance = 5.0f;
    private Transform playerTransform; // Using Transform type for the player position

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        if (GameManager.Player != null)
        {
            playerTransform = GameManager.Player.transform; // Accessing the transform of the Player
        }
        else
        {
            Debug.LogError("Player not found in GameManager");
        }
    }

    private void Update()
    {
        if (playerTransform != null && doorAnimator != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= activationDistance)
            {
                PlayerIsNear();
            }
            else
            {
                PlayerIsFar();
            }
        }
    }

    private void PlayerIsNear()
    {
        doorAnimator.SetBool("IsNear", true);
    }

    private void PlayerIsFar()
    {
        doorAnimator.SetBool("IsNear", false);
    }
}

