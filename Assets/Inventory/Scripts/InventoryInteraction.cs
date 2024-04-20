using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryInteraction : MonoBehaviour
{
    public GameObject inventoryCanvas;

    public GameObject player;

    public InputActionReference inventory;

    private void OnEnable()
    {
        inventory.action.Enable();
    }

    void Update()
    {
        if (inventory.action.WasPerformedThisFrame())
        {
            inventoryCanvas.gameObject.SetActive(!inventoryCanvas.gameObject.activeSelf);

            Cursor.lockState = inventoryCanvas.gameObject.activeSelf? CursorLockMode.None: CursorLockMode.Locked;
        }
    }
    
    private void OnDisable()
    {
        inventory.action.Disable();
    }
    
}
