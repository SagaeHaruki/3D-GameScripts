using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    Movement playerMovement;
    // Item Interaction
    [SerializeField] private GameObject itmPanel;
    [SerializeField] private TMP_Text itmPickName;
    [SerializeField] private Image itmImage;

    // NPC Interaction
    [SerializeField] private GameObject npcPanel;
    [SerializeField] private TMP_Text npcName;

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
        itmPanel.gameObject.SetActive(false);
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
                    itmPanel.gameObject.SetActive(true);
                    itmPickName.text = closestItem.ItemName;
                    itmImage.sprite = closestItem.ItemSprite;
                }
            }
        }

        if (closestItem == null)
        {
            itmPanel.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (closestItem != null)
            {
                closestItem.Interact();
            }
        }
    }

    private void PlayerNPCInteraction()
    {
        float interactRange = 2.9f;

        NPC_1 closestNPC = null;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        float closestDistance = float.MaxValue;

        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPC_1 npcInteract))
            {
                float distance = Vector3.Distance(transform.position, npcInteract.transform.position);
           
                if (distance < closestDistance)
                {
                    closestNPC = npcInteract;
                    closestDistance = distance;
                    npcPanel.gameObject.SetActive(true);
                    npcName.text = npcInteract.NPCName;

                    if (closestNPC.isInteracting)
                    {
                        playerMovement.canMove = false;
                    }
                    else
                    {
                        playerMovement.canMove = true;
                    }
                }
            }
        }

        if (closestNPC == null)
        {
            npcPanel.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (closestNPC !=  null)
            {
                closestNPC.Interact();
            }
        }
    }
}
