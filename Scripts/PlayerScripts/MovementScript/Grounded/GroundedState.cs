using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : MonoBehaviour
{
    Movement playerMovement;

    private float previousYPosition;

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

        AddGravity();
        SlopeChecker();
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
            playerMovement.Velocity.y -= playerMovement.Gravity * -1.5f * Time.deltaTime;
        }
        playerMovement.charControl.Move(playerMovement.Velocity * Time.deltaTime);
    }

    private void SlopeChecker()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.5f, playerMovement.layerMasks))
        {
            Vector3 groundNormal = hit.normal;
            playerMovement.slopeAngle = Vector3.Angle(groundNormal, Vector3.up);
            float currentYPosition = transform.position.y;
            if (playerMovement.slopeAngle >= 1f)
            {
                playerMovement.onSlope = true;
                if (playerMovement.isMoving)
                {
                    if (currentYPosition > previousYPosition)
                    {
                        playerMovement.goingUp = true;
                        playerMovement.goingDown = false;
                    }
                    else if (currentYPosition < previousYPosition)
                    {
                        playerMovement.goingUp = false;
                        playerMovement.goingDown = true;
                    }
                    else
                    {
                        playerMovement.goingDown = false;
                        playerMovement.goingUp = false;
                        return;
                    }
                }
                else
                {
                    playerMovement.goingDown = false;
                    playerMovement.goingUp = false;
                }
                previousYPosition = currentYPosition;
            }
        }
        else
        {
            playerMovement.onSlope = false;
            playerMovement.goingDown = false;
            playerMovement.goingUp = false;
        }
    }
}
