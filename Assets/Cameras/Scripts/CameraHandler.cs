using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraHandler : MonoBehaviour
{

    public CinemachineFreeLook thirdPersonCamera;
    private float thirdPersonCameraZoom;
    [SerializeField] private float aimCameraZoom;
    
    
    
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

    private void Start()
    {
        thirdPersonCameraZoom = thirdPersonCamera.GetComponentInChildren<CinemachineCameraOffset>().m_Offset.y;
    }

    public void ActiveThirdPersonCamera()
    {
        thirdPersonCamera.GetComponentInChildren<CinemachineCameraOffset>().m_Offset.z = thirdPersonCameraZoom;
    }
    
    public void ActiveAimCamera()
    {
        thirdPersonCamera.GetComponentInChildren<CinemachineCameraOffset>().m_Offset.z = aimCameraZoom;
    }
}
