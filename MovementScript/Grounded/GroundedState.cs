using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : MonoBehaviour
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

        GroundedPlayer();
        AddGravity();
        SlopeChecker();
    }

    private void GroundedPlayer()
    {
        //if (playerMovement.charControl.isGrounded)
        //{
        //    playerMovement.isGrounded = true;
        //}

        //if (!playerMovement.charControl.isGrounded)
        //{
        //    playerMovement.isGrounded = false;  
        //}
    }

    private void AddGravity()
    {
        float offset = 0.1f;

        if (playerMovement.Velocity.y <= -7.5f)
        {
            playerMovement.Velocity.y = -7.5f;
        }

        if (Physics.Raycast(transform.position + Vector3.up * offset, Vector3.down, out RaycastHit hit, offset + 0.1f) && !playerMovement.isJumping)
        {
            playerMovement.Velocity.y = -1f;
            playerMovement.isGrounded = true;
            playerMovement.isFalling = false;
        }
        else
        {
            playerMovement.isGrounded = false;
            playerMovement.Velocity.y -= playerMovement.Gravity * -2f * Time.deltaTime;
        }
        playerMovement.charControl.Move(playerMovement.Velocity * Time.deltaTime);
    }

    private void SlopeChecker()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.5f, playerMovement.layerMasks))
        {
            Vector3 groundNormal = hit.normal;
            playerMovement.slopeAngle = Vector3.Angle(groundNormal, Vector3.up);
            if (playerMovement.slopeAngle >= 1f)
            {
                playerMovement.onSlope = true;
            }
        }
        else
        {
            playerMovement.onSlope = false;
        }
    }
}
