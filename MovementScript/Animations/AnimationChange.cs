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
            if(playerMovement.isMoving)
            {
                if (playerMovement.isWalking)
                {
                    playerMovement.animator.SetBool("isWalking", true);
                    playerMovement.animator.SetBool("isRunning", false);
                    playerMovement.animator.SetBool("isSprinting", false);
                    playerMovement.animator.SetBool("isFalling", false);
                }

                if (playerMovement.isRunning)
                {
                    playerMovement.animator.SetBool("isWalking", false);
                    playerMovement.animator.SetBool("isRunning", true);
                    playerMovement.animator.SetBool("isSprinting", false);
                    playerMovement.animator.SetBool("isFalling", false);
                }

                if (playerMovement.isSprinting)
                {
                    playerMovement.animator.SetBool("isWalking", false);
                    playerMovement.animator.SetBool("isRunning", false);
                    playerMovement.animator.SetBool("isSprinting", true);
                    playerMovement.animator.SetBool("isFalling", false);
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

            if (!playerMovement.isMoving)
            {
                playerMovement.animator.SetBool("isWalking", false);
                playerMovement.animator.SetBool("isRunning", false);
                playerMovement.animator.SetBool("isSprinting", false);
                playerMovement.animator.SetBool("isJumping", false);
                playerMovement.animator.SetBool("isFalling", false);
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
