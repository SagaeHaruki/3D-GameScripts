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

    public event Action<ItemUI> OnItemClicked, OnRightMouseButtonClick;

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

    public void ResetData()
    {
        itemImage.gameObject.SetActive(false);
        empty = true;
    }

    public void DelesectItem()
    {
        borderImage.enabled = false;
    }

    public void SelectItem()
    {
        borderImage.enabled = true;
    }
    public void OnPointerClick(BaseEventData data)
    {
        if (empty)
        {
            return;
        }
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
