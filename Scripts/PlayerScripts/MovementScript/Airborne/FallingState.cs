using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : MonoBehaviour
{
    Movement playerMovement;

    private float fallDelay = -4.2f;
    private float fallDelay2 = -6.5f;

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

        CheckFallingState();
    }

    private void CheckFallingState()
    {
        if (!playerMovement.isGrounded && !playerMovement.isSwimming)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerMovement.layerMasks))
            {
                float currentHeight = hit.distance;

                if (currentHeight >= playerMovement.FallingHeightDiff)
                {
                    Vector3 moveDirection = transform.forward;
                    if (playerMovement.Velocity.y <= fallDelay && !playerMovement.isWalking)
                    {
                        playerMovement.isFalling = true;
                        playerMovement.charControl.Move(moveDirection * 1.2f * Time.deltaTime);
                    }

                    if (playerMovement.Velocity.y <= fallDelay2 && playerMovement.isWalking)
                    {
                        playerMovement.isFalling = true;
                        playerMovement.charControl.Move(moveDirection * 1.2f * Time.deltaTime);
                    }
                }
            }
        }
        else if (playerMovement.charControl.isGrounded)
        {
            playerMovement.isFalling = false;
        }
        else if (playerMovement.isSwimming)
        {
            playerMovement.isFalling = false;
        }
    }
}
