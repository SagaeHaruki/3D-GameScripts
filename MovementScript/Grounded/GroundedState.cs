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

    public void GroundedPlayer()
    {

    }

    public void AddGravity()
    {
        if (playerMovement.Velocity.y <= -7.5f)
        {
            playerMovement.Velocity.y = -7.5f;
        }

        if (playerMovement.charControl.isGrounded)
        {
            playerMovement.isGrounded = true;
            playerMovement.Velocity.y = -1f;
            playerMovement.isFalling = false;
        }
        else
        {
            playerMovement.isGrounded = false;
            playerMovement.Velocity.y -= playerMovement.Gravity * -2f * Time.deltaTime;
        }
        playerMovement.charControl.Move(playerMovement.Velocity * Time.deltaTime);
    }

    public void SlopeChecker()
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
