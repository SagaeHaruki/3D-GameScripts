using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInvCPL : MonoBehaviour
{
    [SerializeField] public bool isOpenInventory = false;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private InventorySO inventoryData;

    private void Awake()
    {
        PrepareUI();
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        this.inventoryUI.OnDescriptionRequested += HandleItemSelection;
    }

    private void HandleItemSelection(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            return;
        }
        else
        {
            ItemSO item = inventoryItem.item;
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
        }
    }

    private void HandleItemActionRequest(int itemIndex)
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                isOpenInventory = true;
                inventoryUI.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }
            }
            else
            {
                isOpenInventory = false;
                inventoryUI.Hide();
            }
        }
    }
}
