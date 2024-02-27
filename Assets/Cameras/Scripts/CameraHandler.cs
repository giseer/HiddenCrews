using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraHandler : MonoBehaviour
{

    public CinemachineFreeLook thirdPersonCamera;
    private float thirdPersonCameraZoom;
    
    [SerializeField] private float aimCameraZoom;

    private CinemachineCameraOffset cameraOffset;
    
    
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
        
        cameraOffset = thirdPersonCamera.GetComponentInChildren<CinemachineCameraOffset>();
    }

    private void Start()
    {
        thirdPersonCameraZoom = thirdPersonCamera.GetComponentInChildren<CinemachineCameraOffset>().m_Offset.y;
    }

    
    
    public void ActiveThirdPersonCamera()
    {
        DOTween.To(() => cameraOffset.m_Offset.z, x => cameraOffset.m_Offset.z = x, thirdPersonCameraZoom, 1f);
    }
    
    public void ActiveAimCamera()
    {
        DOTween.To(() => cameraOffset.m_Offset.z, x => cameraOffset.m_Offset.z = x, aimCameraZoom, 1f);
    }
}
