using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField] private string ItemName;
    [SerializeField] private string ItemDescription;
    [SerializeField] private int ItemQuantity;
    [SerializeField] private Sprite ItemSprite;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }
    public void Interact()
    {
        inventoryManager.AddItem(ItemName, ItemDescription, ItemQuantity, ItemSprite);
        Destroy(gameObject);
    }
}
