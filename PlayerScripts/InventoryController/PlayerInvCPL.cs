using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInvCPL : MonoBehaviour
{
    Movement playerMovement;

    [SerializeField] public GameObject InventoryGUI;

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
        InventoryGUI.SetActive(false);
    }

    private void Update()
    {
        Updater();

        if (Input.GetKeyDown(KeyCode.B) && !playerMovement.onInventory)
        {
            InventoryGUI.SetActive(true);
            playerMovement.onInventory = true;
        }
        else if (Input.GetKeyDown(KeyCode.B) && playerMovement.onInventory)
        {
            InventoryGUI.SetActive(false);
            playerMovement.onInventory = false;
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
