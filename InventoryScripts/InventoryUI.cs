using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemUI itemUI;
    [SerializeField] private RectTransform cPanel;

    List<ItemUI> itemList = new List<ItemUI>();

    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            ItemUI uiItem = Instantiate(itemUI, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(cPanel);
            itemList.Add(uiItem);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
