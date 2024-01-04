using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : MonoBehaviour
{
    Movement playerMovement;

    private float fallDelay = -4.2f;

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
        if (!playerMovement.isGrounded)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerMovement.layerMasks))
            {
                float currentHeight = hit.distance;

                if (currentHeight >= playerMovement.FallingHeightDiff)
                {
                    Vector3 moveDirection = transform.forward;
                    playerMovement.charControl.Move(moveDirection * 1.2f * Time.deltaTime);
                    if (playerMovement.Velocity.y <= fallDelay)
                    {
                        playerMovement.isFalling = true;
                    }
                }
            }
        }
        else if (playerMovement.charControl.isGrounded)
        {
            playerMovement.isFalling = false;
        }
    }
}
