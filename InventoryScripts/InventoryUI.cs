using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemUI itemUI;
    [SerializeField] private RectTransform cPanel;
    [SerializeField] private InventoryDesc itemDescrpt;

    List<ItemUI> itemList = new List<ItemUI>();
    public event Action<int> OnDescriptionRequested, OnItemActionRequested;

    private void Awake()
    {
        Hide();
        itemDescrpt.ResetDescription();
    }

    public void InitializeInventoryUI(int invetorySize)
    {
        // Set the maximum size to the stated inventory size
        for (int i = 0; i < invetorySize; i++)
        {
            ItemUI uiItem = Instantiate(itemUI, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(cPanel);
            itemList.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
        }
    }

    internal void ResetAllItems()
    {
        foreach (var item in itemList)
        {
            item.ResetData();
            item.DelesectItem();
        }
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (itemList.Count > itemIndex)
        {
            itemList[itemIndex].SetItemData(itemImage, itemQuantity);
        }
    }

    internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        itemDescrpt.SetDescription(itemImage, name, description);
        DeselectAllItems();
        itemList[itemIndex].SelectItem();
    }


    private void HandleItemSelection(ItemUI inventoryItemUI)
    {
        int index = itemList.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }
        OnDescriptionRequested?.Invoke(index);
    }

    private void DeselectAllItems()
    {
        foreach (ItemUI item in itemList)
        {
            item.DelesectItem();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescrpt.ResetDescription();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
