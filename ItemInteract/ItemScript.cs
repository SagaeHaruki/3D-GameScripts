using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField] public string ItemName;
    [SerializeField] private string ItemDescription;
    [SerializeField] private int ItemQuantity;
    [SerializeField] public Sprite ItemSprite;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }
    public void Interact()
    {
        if (inventoryManager == null)
        {
            inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
        }

        inventoryManager.AddItem(ItemName, ItemDescription, ItemQuantity, ItemSprite);
        Destroy(gameObject);
    }
}
