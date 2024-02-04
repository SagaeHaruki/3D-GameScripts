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

    private void ChangePlayerState()
    {
        if (!playerMovement.isFalling)
        {
            if (playerMovement.isMoving)
            {
                if (!playerMovement.isSwimming)
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

                    if (playerMovement.isJumping)
                    {
                        playerMovement.playerState = "Jumping";
                    }

                    if (playerMovement.isDashing)
                    {
                        playerMovement.playerState = "Dashing";
                    }
                }

                if (playerMovement.isSwimming)
                {
                    if (playerMovement.isWalking)
                    {
                        playerMovement.playerState = "SlowSwim";
                    }

                    if (playerMovement.isRunning)
                    {
                        playerMovement.playerState = "FastSwim";
                    }

                    if (playerMovement.isSprinting)
                    {
                        playerMovement.playerState = "SpeedSwim";
                    }
                }
            }

            if (!playerMovement.isMoving)
            {
                if (!playerMovement.isSwimming)
                {
                    playerMovement.playerState = "Idle";
                }

                if (playerMovement.isSwimming)
                {
                    playerMovement.playerState = "IdleSwim";
                }

                if (playerMovement.isJumping)
                {
                    playerMovement.playerState = "IdleJump";
                }
            }
        }

        if (playerMovement.isFalling)
        {
            playerMovement.playerState = "Falling";
        }
    }
}
