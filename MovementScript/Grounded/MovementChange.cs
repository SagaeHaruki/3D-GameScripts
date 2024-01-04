using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementChange : MonoBehaviour
{
    Movement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (playerMovement == null) 
        {
            return;
        }

        SprintingKey();
        ToggleWalkAndRun();
        ChangePlayerSpeed();
    }

    private void SprintingKey()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerMovement.isSprinting = true;
        }
        else
        {
            playerMovement.isSprinting = false;
        }
    }

    private void ToggleWalkAndRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (playerMovement.isRunning)
            {
                playerMovement.isRunning = false;
                playerMovement.isWalking = true;
            }
            else
            {
                playerMovement.isRunning = true;
                playerMovement.isWalking = false;
            }
        }
    }

    private void ChangePlayerSpeed()
    {
        if (playerMovement.isWalking)
        {
            playerMovement.speedModifier = 0.5f;
        }

        if (playerMovement.isRunning)
        {
            playerMovement.speedModifier = 1.5f;
        }

        if (playerMovement.isSprinting)
        {
            playerMovement.speedModifier = 2f;
        }
    }
}
