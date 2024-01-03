using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
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

        ChangePlayerState();
    }

    public void ChangePlayerState()
    {
        if (!playerMovement.isFalling)
        {
            if (playerMovement.isMoving)
            {
                if (playerMovement.isWalking)
                {
                    playerMovement.playerState = "Walking";
                }

                if (playerMovement.isRunning)
                {
                    playerMovement.playerState = "Running";
                }

                if (playerMovement.isSprinting)
                {
                    playerMovement.playerState = "Sprinting";
                }
            }

            if (!playerMovement.isMoving)
            {
                playerMovement.playerState = "Idle";
            }
        }
    }
}
