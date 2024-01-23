using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private InventoryManager inventoryManager;

    public string ItemName;
    public string ItemDescription;
    public int ItemQuantity;
    public Sprite ItemSprite;
    public bool isFull;

    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image ImageItem;
    [SerializeField] public Image imageShader;
    public bool SelectedItem;

    private void Awake()
    {
        inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }

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
        ImageItem.enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SetItemDesc();
            OnLeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {

        }
    }

    public void SetItemDesc()
    {
        inventoryManager.itemImage.sprite = ItemSprite;
        inventoryManager.itemImage.enabled = true;
        inventoryManager.itemName.text = ItemName;
        inventoryManager.itemDescription.text = ItemDescription;
    }

    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        imageShader.color = new Color32((byte)155, (byte)155, (byte)155, (byte)15);
        SelectedItem = true;
    }
}
