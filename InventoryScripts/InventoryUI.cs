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
    [SerializeField] private MouseFollower mousefollow; 

    List<ItemUI> itemList = new List<ItemUI>();


    [SerializeField] public Sprite image, image2;
    [SerializeField] public int qtty;
    [SerializeField] public string title, desc;

    private int currentDraggedItem = -1;


    private void Awake()
    {
        Hide();
        mousefollow.Toggle(false);
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
    private void ShowItemAction(ItemUI invItemUI)
    {
        
    }

    private void ItemBeginDrag(ItemUI invItemUI)
    {
        int index = itemList.IndexOf(invItemUI);
        if (index == -1)
        {
            return;
        }

        currentDraggedItem = index;

        mousefollow.Toggle(true);
        mousefollow.SetItmData(index == 0 ? image : image2, qtty);
    }

    private void ItemEndDrag(ItemUI invItemUI)
    {
        mousefollow.Toggle(false);
    }

    private void ItemSwap(ItemUI invItemUI)
    {
        int index = itemList.IndexOf(invItemUI);
        if (index == -1)
        {
            mousefollow.Toggle(false);
            currentDraggedItem = -1;
            return;
        }

        itemList[currentDraggedItem].SetItemData(index == 0 ? image : image2, qtty);
        itemList[index].SetItemData(currentDraggedItem == 0 ? image : image2, qtty);
        mousefollow.Toggle(false);
        currentDraggedItem = -1;
    }

    private void ItemSelection(ItemUI invItemUI)
    {
        itemDescrpt.SetDescription(image, title, desc);
        itemList[0].SelectItem();
    }
    #endregion

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescrpt.ResetDescription();
        itemList[0].SetItemData(image, qtty);
        itemList[1].SetItemData(image2, qtty);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
