using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInvCPL : MonoBehaviour
{
    Movement playerMovement;

    [SerializeField] public GameObject InventoryGUI;
    [SerializeField] private InventoryManager InventoryScript;

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
        InventoryScript = InventoryGUI.GetComponent<InventoryManager>();
        InventoryGUI.SetActive(false);

        InventoryScript.DeselectAllSlots();
    }

    private void Update()
    {
        Updater();

        if (!playerMovement.isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.B) && !playerMovement.onInventory)
            {
                InventoryGUI.SetActive(true);
                playerMovement.onInventory = true;
                InventoryScript.DeselectAllSlots();
                InventoryScript.ResetItemDesc();
            }
            else if (Input.GetKeyDown(KeyCode.B) && playerMovement.onInventory)
            {
                InventoryGUI.SetActive(false);
                playerMovement.onInventory = false;
                InventoryScript.DeselectAllSlots();
                InventoryScript.ResetItemDesc();
            }
        }
    }

    private void Updater()
    {
        if (!playerMovement.isInteracting)
        {
            if (playerMovement.onInventory)
            {
                playerMovement.canJump = false;
                playerMovement.canMove = false;
                playerMovement.canAttack = false;
            }
            else if (!playerMovement.onInventory)
            {
                playerMovement.canJump = true;
                playerMovement.canMove = true;
                playerMovement.canAttack = true;
            }
        }
    }
}
