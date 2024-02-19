using System;
using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    public GameObject ThirdPersonCamera;
    public GameObject AimCamera;
    
    public static CameraHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }


    public void ActiveThirdPersonCamera()
    {
        ThirdPersonCamera.SetActive(true);
    }
    
    public void ActiveAimCamera()
    {
        AimCamera.SetActive(true);
    }
    
    public void DesactiveThirdPersonCamera()
    {
        ThirdPersonCamera.SetActive(false);
    }
    
    public void DesactiveAimCamera()
    {
        AimCamera.SetActive(false);
    }
}
