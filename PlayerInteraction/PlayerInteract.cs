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
        ItemInteraction();
    }

    private void ItemInteraction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            float interactRange = 2.4f;

            ItemScript closestItem = null;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            float closestDistance = float.MaxValue;

            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out ItemScript itmScript))
                {
                    // Calculate the distance between the player and the current item
                    float distance = Vector3.Distance(transform.position, itmScript.transform.position);

                    // Check if the current item is closer than the previously found closest item
                    if (distance < closestDistance)
                    {
                        closestItem = itmScript;
                        closestDistance = distance;
                    }
                }
            }

            // Interact with the closest item, if one is found
            if (closestItem != null)
            {
                closestItem.Interact();
            }
        }
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
