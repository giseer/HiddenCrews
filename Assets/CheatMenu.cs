using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatMenu : MonoBehaviour
{
   [SerializeField] private InputActionReference cheatMenu;
   [SerializeField] private GameObject cheatMenuCanvas;
   [SerializeField] private bool keepShowCursorOnClose = false;

   private void OnEnable()
   {
      cheatMenu.action.Enable();
   }

   private void Update()
   {
      if (cheatMenu.action.WasPerformedThisFrame())
      {
         cheatMenuCanvas.SetActive(!cheatMenuCanvas.activeSelf);

         if (cheatMenuCanvas.activeSelf)
         {
            Cursor.lockState = CursorLockMode.Confined;
         }
         else
         {
            if (!keepShowCursorOnClose)
            {
               Cursor.lockState = CursorLockMode.Locked;
            }
         }
      }
   }
   
   private void OnDisable()
   {
      cheatMenu.action.Disable();
   }
}
