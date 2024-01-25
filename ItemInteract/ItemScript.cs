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

        for (int i = 0; i < inventoryManager.itemSlot.Length; i++)
        {
            if (inventoryManager.itemSlot[i].ItemName != ItemName)
            {
                if (inventoryManager.itemSlot[i].isFull == false)
                {
                    print(" i ran this");
                    inventoryManager.AddItem(ItemName, ItemDescription, ItemQuantity, ItemSprite);
                    Destroy(gameObject);
                    return;
                }
            }
            else if (inventoryManager.itemSlot[i].ItemName == ItemName)
            {
                print(i);
                inventoryManager.itemSlot[i].UpdateItem(ItemName, ItemDescription, ItemQuantity, ItemSprite);
                Destroy(gameObject);
                print("Same");
                return;
            }
        }
    }
}
