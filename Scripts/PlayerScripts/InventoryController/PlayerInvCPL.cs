using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvCPL : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;

    private int inventorySize = 10;

    private void Awake()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }
}
