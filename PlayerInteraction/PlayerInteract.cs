using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
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

        PlayerNPCInteraction();
    }

    private void PlayerNPCInteraction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            float interactRange = 2.4f;

            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray) 
            {
                if(collider.TryGetComponent(out NPC_1 npcInteract))
                {
                    npcInteract.Interact();
                }
            }
        }
    }
}
