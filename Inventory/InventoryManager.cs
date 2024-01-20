using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public ItemSlot[] itemSlot;

    public void AddItem(string itemName, string itemDescm, int quantity, Sprite itemSprite)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, itemDescm, quantity, itemSprite);
                return;
            }
        }
    }
}
