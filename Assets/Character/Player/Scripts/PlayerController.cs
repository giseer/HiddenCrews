using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerMover mover;
    [SerializeField] private PlayerAnimationsHandler animationsHandler;

    [Header("Visuals")]
    [SerializeField] private GameObject crosshair;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        inputHandler.onMove.AddListener(OnMove);
        inputHandler.onJump.AddListener(OnJump);
        inputHandler.onSprint.AddListener(OnSprint);
        inputHandler.onAim.AddListener(OnAim);
        inputHandler.onReleaseAim.AddListener(OnReleaseAim);
        inputHandler.onStopMove.AddListener(OnStopMove);
    }

    private void OnMove(Vector2 movementValues)
    {
        mover.MoveAndRotate(movementValues);
    }

    private void OnStopMove()
    {
        mover.MoveAndRotate(Vector2.zero);
    }

    private void OnJump()
    {
        mover.Jump();
    }

    private void OnSprint()
    {
        // Cambiar Velocidad;
    }

    private void OnAim()
    {
        CameraHandler.Instance.ActiveAimCamera();
        CameraHandler.Instance.DesactiveThirdPersonCamera();
        animationsHandler.ActiveAimingAnimations();
        crosshair.SetActive(true);
    }

    private void OnReleaseAim()
    {
        CameraHandler.Instance.ActiveThirdPersonCamera();
        CameraHandler.Instance.DesactiveAimCamera();
        animationsHandler.DesactiveAimingAnimations();
        crosshair.SetActive(false);
    }
    
    
    private void OnDisable()
    {   
        inputHandler.onMove.RemoveListener(OnMove);
        inputHandler.onJump.RemoveListener(OnJump);
        inputHandler.onSprint.RemoveListener(OnSprint);
        inputHandler.onAim.RemoveListener(OnReleaseAim);
        inputHandler.onReleaseAim.RemoveListener(OnReleaseAim);
    }
}
