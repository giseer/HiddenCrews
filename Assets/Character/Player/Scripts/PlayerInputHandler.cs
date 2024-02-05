using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action References")]
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference shoot;

    [Header("Input Action Events")]
    public UnityEvent<Vector2> onMove;
    public UnityEvent onJump;
    public UnityEvent onShoot;

    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        shoot.action.Enable();
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (move.action.IsPressed())
        {
            Debug.Log("Estoy Moviendome");
            onMove?.Invoke(move.action.ReadValue<Vector2>());
        }

        if (jump.action.WasPerformedThisFrame())
        {
            Debug.Log("Estoy Saltando");
            onJump?.Invoke();
        }

        if (shoot.action.WasPerformedThisFrame())
        {
            Debug.Log("Estoy Disparando");
            onShoot?.Invoke();
        }
    }
    
    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
        shoot.action.Disable();
    }
}
