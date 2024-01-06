using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementChange : MonoBehaviour
{
    Movement playerMovement;

    // Dash Movement
    [SerializeField] private float dashDistance = 1.2f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1.2f;
    [SerializeField] private float dashTime;
    [SerializeField] private bool canDash = true;

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
        if (Input.GetKeyDown(KeyCode.LeftShift) && !playerMovement.isJumping && canDash && !playerMovement.isSwimming)
        {
            if (playerMovement.isGrounded)
            {
                playerMovement.isDashing = true;
                Vector3 dashDirection = transform.forward * dashDistance;
                StartCoroutine(Dash(dashDirection));
                StartCoroutine(DashCooldown());
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && !playerMovement.isDashing)
        {
            playerMovement.isSprinting = true;
        }
        else if(!playerMovement.isMoving && playerMovement.isSprinting)
        {
            playerMovement.isSprinting = false;
        }

        if (playerMovement.isDashing)
        {
            dashTime += Time.deltaTime;
            if (dashTime >= 0.3f)
            {
                playerMovement.isDashing = false;
                dashTime = 0f;
            }
        }
    }

    IEnumerator Dash(Vector3 dashDirection)
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            playerMovement.charControl.Move(dashDirection * Time.deltaTime / dashDuration);
            yield return null;
        }
    }

    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        playerMovement.isDashing = false;
    }

    private void ToggleWalkAndRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !playerMovement.isJumping)
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
        if (!playerMovement.onSlope)
        {
            if (playerMovement.isWalking)
            {
                playerMovement.speedModifier = 0.3f;
            }

            if (playerMovement.isRunning)
            {
                playerMovement.speedModifier = 0.8f;
            }

            if (playerMovement.isSprinting)
            {
                playerMovement.speedModifier = 1.7f;
            }
        }

        if (playerMovement.onSlope)
        {
            // Increase when going down
            if (playerMovement.goingDown)
            {
                if (playerMovement.isWalking)
                {
                    playerMovement.speedModifier = 0.3f;
                }

                if (playerMovement.isRunning)
                {
                    playerMovement.speedModifier = 0.7f;
                }

                if (playerMovement.isSprinting)
                {
                    playerMovement.speedModifier = 1.6f;
                }
            }

            // Reduce when going up
            if (playerMovement.goingUp)
            {
                if (playerMovement.isWalking)
                {
                    playerMovement.speedModifier = 0.3f;
                }

                if (playerMovement.isRunning)
                {
                    playerMovement.speedModifier = 0.6f;
                }

                if (playerMovement.isSprinting)
                {
                    playerMovement.speedModifier = 1.5f;
                }
            }
        }
    }
}
