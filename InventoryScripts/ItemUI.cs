using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text quantityTxt;
    [SerializeField] private Image borderImage;

    public event Action<ItemUI> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseButtonClick;

    private bool empty = true;

    private void Awake()
    {
        ResetItemData();
        DelesectItem();

    }

    public void ResetItemData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }

    public void SetItemData(Sprite sprite, int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityTxt.text = quantity + "";
        empty = false;
    }

    public void DelesectItem()
    {
        borderImage.enabled = false;
    }

    public void SelectItem()
    {
        borderImage.enabled = true;
    }

    public void OnBeginDrag()
    {
        if (empty)
        {
            return;
        }

        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnItemDrop()
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseButtonClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
