using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInvCPL : MonoBehaviour
{
    [SerializeField] public GameObject InventoryGUI;
    [SerializeField] public bool onInventory;

    private void Awake()
    {
        InventoryGUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !onInventory)
        {
            Time.timeScale = 0;
            InventoryGUI.SetActive(true);
            onInventory = true;
        }
        else if (Input.GetKeyDown(KeyCode.B) && onInventory)
        {
            Time.timeScale = 1;
            InventoryGUI.SetActive(false);
            onInventory = false;
        }
    }
}
