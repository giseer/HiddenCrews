using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DoorSystem : MonoBehaviour
{
    public InputActionReference interactuar;
    public string sceneName;
    public GameObject canvasAlmacen;
    public GameObject canvasDoor;

    private void OnEnable()
    {
        interactuar.action.Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            canvasDoor.SetActive(true);
            canvasAlmacen.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasDoor.SetActive(false);
        }
    }


    private void Update()
    {
        if (canvasDoor.activeSelf && interactuar.action.WasPerformedThisFrame())
        {
            NavigatorManager.LoadScene(sceneName);
        }
    }
    
    private void OnDisable()
    {
        interactuar.action.Disable();
    }
}
