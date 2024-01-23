using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public TMP_Text itemDescription;
    [SerializeField] public Image itemImage;
    [SerializeField] public ItemSlot[] itemSlot;

    private void Awake()
    {
        itemImage.enabled = false;
        itemName.text = "";
        itemDescription.text = "";
    }

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

    public void DeselectAllSlots()
    {
        for(int i = 0;i < itemSlot.Length; i++) 
        {
            itemSlot[i].imageShader.color = new Color32((byte)0, (byte)0, (byte)0, (byte)150);
            itemSlot[i].SelectedItem = false;
        }
    }
}
