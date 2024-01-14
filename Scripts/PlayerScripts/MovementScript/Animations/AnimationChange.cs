using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChange : MonoBehaviour
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

        ChangeAnimations();
    }

    private void ChangeAnimations()
    {
        if (!playerMovement.isFalling)
        {
            // Player Moving
            if(playerMovement.isMoving)
            {
                // Player Not Swimming
                if (!playerMovement.isSwimming)
                {
                    if (!playerMovement.isAttacking)
                    {
                        if (playerMovement.isWalking)
                        {
                            playerMovement.animator.SetBool("isWalking", true);
                            playerMovement.animator.SetBool("isRunning", false);
                            playerMovement.animator.SetBool("isSprinting", false);
                            playerMovement.animator.SetBool("isJumping", false);
                            playerMovement.animator.SetBool("isFalling", false);
                            playerMovement.animator.SetBool("isDashing", false);
                            playerMovement.animator.SetBool("isSwimming", false);

                            playerMovement.animator.SetBool("isAttacking1", false);
                            playerMovement.animator.SetBool("isAttacking2", false);
                            playerMovement.animator.SetBool("isAttacking3", false);
                        }

                        if (playerMovement.isRunning)
                        {
                            playerMovement.animator.SetBool("isWalking", false);
                            playerMovement.animator.SetBool("isRunning", true);
                            playerMovement.animator.SetBool("isSprinting", false);
                            playerMovement.animator.SetBool("isJumping", false);
                            playerMovement.animator.SetBool("isFalling", false);
                            playerMovement.animator.SetBool("isDashing", false);
                            playerMovement.animator.SetBool("isSwimming", false);

                            playerMovement.animator.SetBool("isAttacking1", false);
                            playerMovement.animator.SetBool("isAttacking2", false);
                            playerMovement.animator.SetBool("isAttacking3", false);
                        }

                        if (playerMovement.isSprinting)
                        {
                            playerMovement.animator.SetBool("isWalking", false);
                            playerMovement.animator.SetBool("isRunning", false);
                            playerMovement.animator.SetBool("isSprinting", true);
                            playerMovement.animator.SetBool("isJumping", false);
                            playerMovement.animator.SetBool("isFalling", false);
                            playerMovement.animator.SetBool("isDashing", false);
                            playerMovement.animator.SetBool("isSwimming", false);

                            playerMovement.animator.SetBool("isAttacking1", false);
                            playerMovement.animator.SetBool("isAttacking2", false);
                            playerMovement.animator.SetBool("isAttacking3", false);
                        }

                        if (playerMovement.isDashing)
                        {
                            playerMovement.animator.SetBool("isWalking", false);
                            playerMovement.animator.SetBool("isRunning", false);
                            playerMovement.animator.SetBool("isSprinting", false);
                            playerMovement.animator.SetBool("isFalling", false);
                            playerMovement.animator.SetBool("isDashing", true);

                            playerMovement.animator.SetBool("isAttacking1", false);
                            playerMovement.animator.SetBool("isAttacking2", false);
                            playerMovement.animator.SetBool("isAttacking3", false);
                        }

                        if (!playerMovement.isDashing)
                        {
                            playerMovement.animator.SetBool("isDashing", false);
                        }

                        #region Jump Section
                        if (playerMovement.isJumping)
                        {
                            playerMovement.animator.SetBool("isWalking", false);
                            playerMovement.animator.SetBool("isRunning", false);
                            playerMovement.animator.SetBool("isSprinting", false);
                            playerMovement.animator.SetBool("isJumping", true);
                            playerMovement.animator.SetBool("isFalling", false);
                        }

                        if (!playerMovement.isJumping)
                        {
                            playerMovement.animator.SetBool("isJumping", false);
                            playerMovement.animator.SetBool("isFalling", false);
                        }
                        #endregion
                    }

                    // Attacking Moving
                    if (playerMovement.isAttacking)
                    {
                        if (playerMovement.attackType == "Attack1")
                        {
                            playerMovement.animator.SetBool("isAttacking1", true);
                            playerMovement.animator.SetBool("isAttacking2", false);
                            playerMovement.animator.SetBool("isAttacking3", false);

                            playerMovement.animator.SetBool("isWalking", false);
                            playerMovement.animator.SetBool("isRunning", false);
                            playerMovement.animator.SetBool("isSprinting", false);

                            playerMovement.animator.SetBool("isDashing", false);
                        }
                        else if (playerMovement.attackType == "Attack2")
                        {
                            playerMovement.animator.SetBool("isAttacking1", false);
                            playerMovement.animator.SetBool("isAttacking2", true);
                            playerMovement.animator.SetBool("isAttacking3", false);

                            playerMovement.animator.SetBool("isWalking", false);
                            playerMovement.animator.SetBool("isRunning", false);
                            playerMovement.animator.SetBool("isSprinting", false);

                            playerMovement.animator.SetBool("isDashing", false);
                        }
                        else if (playerMovement.attackType == "Attack3")
                        {
                            playerMovement.animator.SetBool("isAttacking1", false);
                            playerMovement.animator.SetBool("isAttacking2", false);
                            playerMovement.animator.SetBool("isAttacking3", true);

                            playerMovement.animator.SetBool("isWalking", false);
                            playerMovement.animator.SetBool("isRunning", false);
                            playerMovement.animator.SetBool("isSprinting", false);

                            playerMovement.animator.SetBool("isDashing", false);
                        }
                    }
                }

                // Player Swimming
                if (playerMovement.isSwimming)
                {
                    playerMovement.animator.SetBool("isWalking", false);
                    playerMovement.animator.SetBool("isRunning", false);
                    playerMovement.animator.SetBool("isSprinting", false);
                    playerMovement.animator.SetBool("isJumping", false);
                    playerMovement.animator.SetBool("isFalling", false);
                    playerMovement.animator.SetBool("isDashing", false);
                    playerMovement.animator.SetBool("isSwimming", true);
                    playerMovement.animator.SetBool("isSwimIdle", false);
                }
            }

            // Not Moving Player
            if (!playerMovement.isMoving)
            {
                playerMovement.animator.SetBool("isWalking", false);
                playerMovement.animator.SetBool("isRunning", false);
                playerMovement.animator.SetBool("isSprinting", false);
                playerMovement.animator.SetBool("isJumping", false);
                playerMovement.animator.SetBool("isFalling", false);
                playerMovement.animator.SetBool("isSwimming", false);


                // Attacking Player
                if (!playerMovement.isAttacking)
                {
                    playerMovement.animator.SetBool("isAttacking1", false);
                    playerMovement.animator.SetBool("isAttacking2", false);
                    playerMovement.animator.SetBool("isAttacking3", false);
                }

                if (playerMovement.isAttacking)
                {
                    if (playerMovement.attackType == "Attack1")
                    {
                        playerMovement.animator.SetBool("isAttacking1", true);
                        playerMovement.animator.SetBool("isAttacking2", false);
                        playerMovement.animator.SetBool("isAttacking3", false);
                    }
                    else if (playerMovement.attackType == "Attack2")
                    {
                        playerMovement.animator.SetBool("isAttacking1", false);
                        playerMovement.animator.SetBool("isAttacking2", true);
                        playerMovement.animator.SetBool("isAttacking3", false);
                    }
                    else if (playerMovement.attackType == "Attack3")
                    {
                        playerMovement.animator.SetBool("isAttacking1", false);
                        playerMovement.animator.SetBool("isAttacking2", false);
                        playerMovement.animator.SetBool("isAttacking3", true);
                    }
                }

                // Jumping Player
                if (playerMovement.isJumping)
                {
                    playerMovement.animator.SetBool("isWalking", false);
                    playerMovement.animator.SetBool("isRunning", false);
                    playerMovement.animator.SetBool("isSprinting", false);
                    playerMovement.animator.SetBool("isJumping", true);
                    playerMovement.animator.SetBool("isFalling", false);
                }

                // Jumping Player
                if (!playerMovement.isJumping)
                {
                    playerMovement.animator.SetBool("isWalking", false);
                    playerMovement.animator.SetBool("isRunning", false);
                    playerMovement.animator.SetBool("isSprinting", false);
                    playerMovement.animator.SetBool("isJumping", false);
                    playerMovement.animator.SetBool("isFalling", false);
                }

                // Swimming Section
                if (playerMovement.isSwimming)
                {
                    playerMovement.animator.SetBool("isSwimIdle", true);
                }

                if (!playerMovement.isSwimming)
                {
                    playerMovement.animator.SetBool("isSwimIdle", false);
                }

                // Dashing Section
                if (playerMovement.isDashing)
                {
                    playerMovement.animator.SetBool("isWalking", false);
                    playerMovement.animator.SetBool("isRunning", false);
                    playerMovement.animator.SetBool("isSprinting", false);
                    playerMovement.animator.SetBool("isFalling", false);
                    playerMovement.animator.SetBool("isDashing", true);
                }

                if (!playerMovement.isDashing)
                {
                    playerMovement.animator.SetBool("isDashing", false);
                }
            }
        }

        if (playerMovement.isFalling)
        {
            playerMovement.animator.SetBool("isWalking", false);
            playerMovement.animator.SetBool("isRunning", false);
            playerMovement.animator.SetBool("isSprinting", false);
            playerMovement.animator.SetBool("isJumping", false);
            playerMovement.animator.SetBool("isFalling", true);
        }
    }
}
