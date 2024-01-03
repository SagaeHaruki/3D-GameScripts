using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : MonoBehaviour
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

        NormalJumping();
    }

    private void NormalJumping()
    {
        if (playerMovement.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerMovement.animator.SetBool("isJumping", true);
                playerMovement.isJumping = true;
            }
        }
        else
        {
            playerMovement.isJumping = false;
            playerMovement.animator.SetBool("isJumping", false);
        }
    }
}
