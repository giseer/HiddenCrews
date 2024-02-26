using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input values")] 
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference sprint;
    [SerializeField] private InputActionReference aim;
    [SerializeField] private InputActionReference shoot;

    [Header("InputEvents")] 
    [SerializeField] public UnityEvent<Vector2, bool> onMove;
    [SerializeField] public UnityEvent onStopMove;
    [SerializeField] public UnityEvent onJump;
    [SerializeField] public UnityEvent onSprint;
    [SerializeField] public UnityEvent onAim;
    [SerializeField] public UnityEvent onReleaseAim;
    [SerializeField] public UnityEvent onShoot;
    [SerializeField] public UnityEvent onStopShoot;
    
    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        sprint.action.Enable();
        aim.action.Enable();
        shoot.action.Enable();
    }

    private void Update()
    {
        if (move.action.IsPressed())
        {
            onMove.Invoke(move.action.ReadValue<Vector2>(), sprint.action.IsPressed());
        }
        else
        {
            onStopMove.Invoke();
        }

        if (jump.action.WasPerformedThisFrame())
        {
            onJump.Invoke();
        }

        if (sprint.action.IsPressed())
        {
            onSprint.Invoke();
        }

        if (aim.action.IsPressed())
        {
            onAim.Invoke();
        }
        else
        {
         onReleaseAim.Invoke();   
        }

        if (shoot.action.WasPressedThisFrame())
        {
            onShoot.Invoke();
        }
        else if(shoot.action.WasReleasedThisFrame())
        {
            onStopShoot.Invoke();
        }
    }

    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
        sprint.action.Disable();
        aim.action.Disable();
        shoot.action.Enable();
    }
}
