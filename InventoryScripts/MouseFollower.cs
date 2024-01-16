using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera maincam;
    [SerializeField] private ItemUI item;

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        maincam = Camera.main;
        item = GetComponentInChildren<ItemUI>();
    }

    public void SetItmData(Sprite sprite, int quantity)
    {
        item.SetItemData(sprite, quantity);
    }

    public void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        gameObject.SetActive(val);  
    }
}
