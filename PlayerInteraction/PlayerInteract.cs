using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    Movement playerMovement;
    CameraZoom cameraZoom;
    DialogueBox dialogueBox;

    // Item Interaction
    [SerializeField] private GameObject itmPanel;
    [SerializeField] private TMP_Text itmPickName;
    [SerializeField] private Image itmImage;

    // NPC Interaction
    [SerializeField] private GameObject npcPanel;
    [SerializeField] private TMP_Text npcName;

    private Vector3 originalPos = new Vector3(0, 1.5f, 0);

    private bool TriggerOnce;

    private void Awake()
    {
        cameraZoom = GetComponent<CameraZoom>();
        playerMovement = GetComponent<Movement>();
        dialogueBox = GameObject.Find("DialogueBox").GetComponent<DialogueBox>();
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

    private void OnAnimatorIK()
    {
        float interactRange = 2.2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        float closestDistance = float.MaxValue;
        if (!playerMovement.onInventory)
        {
            foreach (Collider collider in colliderArray)
            {
                if (collider.gameObject.transform.Find("LookAtPoint"))
                {
                    Transform lookPointTransform = collider.gameObject.transform.Find("LookAtPoint");
                    Vector3 lookAtPointPos = lookPointTransform.position;
                    float distance = Vector3.Distance(transform.position, collider.transform.position);

                    // Check if the collider is within the cone of detection
                    Vector3 directionToCollider = (collider.transform.position - transform.position).normalized;
                    Vector3 raycastOrigin = transform.position + Vector3.up * 1.5f;
                    float angle = Vector3.Angle(transform.forward, directionToCollider);

                    if (angle <= 60f)
                    {
                        // Perform a raycast to ensure line of sight
                        RaycastHit hit;
                        if (Physics.Raycast(raycastOrigin, directionToCollider, out hit, interactRange))
                        {
                            if (hit.collider == collider)
                            {
                                // Collider is within the cone and there is line of sight
                                if (distance < closestDistance)
                                {
                                    closestDistance = distance;
                                    playerMovement.animator.SetLookAtWeight(2f);
                                    playerMovement.animator.SetLookAtPosition(lookAtPointPos);
                                    print("found it!");
                                }
                                else
                                {
                                    playerMovement.animator.SetLookAtWeight(0);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void ItemInteraction()
    {
        float interactRange = 2.4f;

        ItemScript closestItem = null;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        float closestDistance = float.MaxValue;

        if (!playerMovement.onInventory)
        {
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

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (closestItem != null)
                {
                    closestItem.Interact();
                }
            }

        }
        else
        {
            itmPanel.gameObject.SetActive(false);
        }

        if (closestItem == null)
        {
            itmPanel.gameObject.SetActive(false);
        }
    }

    private void PlayerNPCInteraction()
    {
        float interactRange = 2.2f;

        NPC_1 closestNPC = null;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        float closestDistance = float.MaxValue;

        if (!playerMovement.onInventory)
        {
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
                            playerMovement.isInteracting = true;
                            cameraZoom.currentTargetDistance = 2.5f;
                            TriggerOnce = true;

                            Vector3 direction = closestNPC.transform.position - transform.position;
                            direction.y = 0;
                            Quaternion targetRot = Quaternion.LookRotation(direction);
                            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 2.5f * Time.deltaTime);
                        }
                        else if (!closestNPC.isInteracting)
                        {
                            playerMovement.isInteracting = false;
                            ResetAngle();
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                if (closestNPC !=  null)
                {
                    closestNPC.Interact();
                }
            }
        }
        else
        {
            npcPanel.gameObject.SetActive(false);
        }

        if (closestNPC == null)
        {
            npcPanel.gameObject.SetActive(false);
        }
    }

    private void ResetAngle()
    {
        if (TriggerOnce)
        {
            TriggerOnce = false;
            cameraZoom.currentTargetDistance = 6.0f;
        }
    }
}
