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


    [SerializeField] public Sprite image;
    [SerializeField] public int qtty;
    [SerializeField] public string title, desc;

    private void Awake()
    {
        Hide();
        itemDescrpt.ResetDescription();
    }

    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            ItemUI uiItem = Instantiate(itemUI, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(cPanel);
            itemList.Add(uiItem);

            uiItem.OnItemClicked += ItemSelection;
            uiItem.OnItemBeginDrag += ItemBeginDrag;
            uiItem.OnItemDroppedOn += ItemSwap;
            uiItem.OnItemEndDrag += ItemEndDrag;
            uiItem.OnRightMouseButtonClick += ShowItemAction;
        }
    }
    #region Item Actions
    private void ShowItemAction(ItemUI obj)
    {
        Debug.Log("Item Actions");
    }

    private void ItemEndDrag(ItemUI obj)
    {
        Debug.Log("Item End Drag");
    }

    private void ItemSwap(ItemUI obj)
    {
        Debug.Log("Item Swap");
    }

    private void ItemBeginDrag(ItemUI obj)
    {
        Debug.Log("Item Begin Drag");
    }

    private void ItemSelection(ItemUI obj)
    {
        itemDescrpt.SetDescription(image, title, desc);
    }
    #endregion

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescrpt.ResetDescription();
        itemList[0].SetItemData(image, qtty);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
