using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : MonoBehaviour
{
    Movement playerMovement;

    [SerializeField] private float jumpCooldown = 1f;
    [SerializeField] private float lastJumpTime;
    [SerializeField] private bool canJump = true;

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();

        lastJumpTime = -jumpCooldown;
    }

    private void Update()
    {
        if (playerMovement == null) 
        {
            return;
        }

        NormalJumping();
        JumpCooldown();
    }

    private void NormalJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !playerMovement.isJumping && canJump && !playerMovement.isSwimming)
        {
            if (playerMovement.isGrounded)
            {
                if (playerMovement.isMoving)
                {
                    if (playerMovement.isRunning || playerMovement.isSprinting)
                    {
                        playerMovement.isJumping = true;
                        playerMovement.Velocity.y = playerMovement.jumpForce;
                    }

                    if (playerMovement.isWalking)
                    {
                        playerMovement.isJumping = true;
                        playerMovement.Velocity.y = playerMovement.jumpForce;
                    }
                }

                if (!playerMovement.isMoving)
                {
                    playerMovement.isJumping = true;
                    playerMovement.Velocity.y = playerMovement.jumpForce;
                }

                lastJumpTime = Time.time;
                canJump = false;
            }
        }
        else if (playerMovement.charControl.isGrounded)
        {
            playerMovement.isJumping = false;
        }
        else if (playerMovement.isSwimming)
        {
            playerMovement.isJumping = false;
        }
    }

    private void JumpCooldown()
    {
        if (!canJump && Time.time - lastJumpTime > jumpCooldown)
        {
            canJump = true;
            lastJumpTime = 0f;
        }
    }
}
