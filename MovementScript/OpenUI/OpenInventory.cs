using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject Inventory;
    public Movement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
        Inventory = GameObject.FindWithTag("InventoryUI");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            playerMovement.allowAttack = false;
            playerMovement.canMove = false;
            playerMovement.canDash = false;

            Inventory.SetActive(!Inventory.activeSelf);
        }
    }
}
