using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraHandler : MonoBehaviour
{

    public CinemachineFreeLook thirdPersonCamera;
    public CinemachineFreeLook aimCamera;
    
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
        thirdPersonCamera.Priority = 100;
    }
    
    public void ActiveAimCamera()
    {
        aimCamera.Priority = 100;
    }
    
    public void DesactiveThirdPersonCamera()
    {
        thirdPersonCamera.Priority = 10;
    }
    
    public void DesactiveAimCamera()
    {
        aimCamera.Priority = 10;
    }
}
