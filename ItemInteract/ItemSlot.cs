using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public string ItemName;
    public string ItemDescription;
    public int ItemQuantity;
    public Sprite ItemSprite;
    public bool isFull;

    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image ImageItem;

    public void AddItem(string itemName, string itemDescm, int quantity, Sprite itemSprite)
    {
        this.ItemName = itemName;
        this.ItemDescription = itemDescm;
        this.ItemQuantity = quantity;
        this.ItemSprite = itemSprite;

        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        ImageItem.sprite = ItemSprite;
    }
}
