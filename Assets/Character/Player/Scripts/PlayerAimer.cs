using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAimer : MonoBehaviour
{     
    [Header("Aim Settings")]
    [SerializeField] private float turnSpeed = 15f;

    private Camera mainCamera;

    [Header("Sensitivity Settings")] 
    [SerializeField] private float normalSensitivity;

    [SerializeField] private float aimSensitivity;
    
    [Header("Components")]
    private PlayerInputHandler inputHandler;
    [SerializeField] private CameraHandler cameraHandler;
    

    private void Awake()
    {
        mainCamera = Camera.main;
        inputHandler = GetComponentInChildren<PlayerInputHandler>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {        
        inputHandler.onAim.AddListener(OnAim);
        inputHandler.onReleaseAim.AddListener(OnReleaseAim);
    }

    private void Start()
    {
        mainCamera = Camera.main;
        cameraHandler.SetSensitivity(normalSensitivity);
    }

    private void LateUpdate()
    {
        float cameraRotation = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, cameraRotation, 0), turnSpeed * Time.fixedDeltaTime);
    }
    
    private void OnAim()
    {
        cameraHandler.SetSensitivity(aimSensitivity);
    }

    private void OnReleaseAim()
    {
       cameraHandler.SetSensitivity(normalSensitivity); 
    }


    private void OnDisable()
    {
        inputHandler.onAim.RemoveListener(OnAim);
        inputHandler.onReleaseAim.RemoveListener(OnReleaseAim);
    }
}
