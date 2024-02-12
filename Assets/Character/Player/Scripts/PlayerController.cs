using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Header("Input values")] 
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference sprint;

    [Header("Movement Values")]
    [SerializeField] private float speed = 1.1f;
    [SerializeField] private float sprintSpeed = 5f;
    
    private Vector3 _velocity;
    
    private bool isSprinting;
    
    private float _targetRotation;
    private float _horizontalInput;
    private float _verticalInput;
    
    [FormerlySerializedAs("turnCalmTime")] public float smoothTurnTime = 0.1f;
    private float smoothTurnVelocity;
    
    [Header("Jump Values")]
    public float jumpForce = 1f;
    
    [Header("Collision Values")] 
    [SerializeField] private CharacterController characterController;
    
    [Header("Camera Values")]
    public Transform playerCamera;

    [Header("Constant Values")] 
    private const float GRAVITY = -9.81f;
    
    [Header("Components")]
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        sprint.action.Enable();
    }

    private void Update()
    {
        UpdateVerticalMovement();

        if (isSprinting)
        {
            Sprint();
            return;
        }
        
        MoveAndRotate(speed);
        
        animator.SetFloat("x", GetMovementInput().x);
        animator.SetFloat("z", GetMovementInput().y);
    }

    private void UpdateVerticalMovement()
    {
        PerformGravity();
        Jump();
        characterController.Move(Vector3.up * (_velocity.y * Time.deltaTime));
    }

    private void PerformGravity()
    {
        if (characterController.isGrounded)
        {
        
            _velocity.y = 0f;
            return;
        }
        
        _velocity.y += GRAVITY * Time.deltaTime;  
        
    }
    
    private void Jump()
    {
        if (jump.action.WasPerformedThisFrame() && characterController.isGrounded)
        {
            _velocity.y = jumpForce;
        }
    }

    private void MoveAndRotate(float moveSpeed)
    {
        Vector2 movementInput = GetMovementInput();
        
        Vector3 movementDirection = CalculateMovementDirection(movementInput);

        RotateTowardsMovementDirection(movementDirection);
        
        MoveCharacter(movementDirection, moveSpeed);
    }
    
    private Vector2 GetMovementInput()
    {
        return move.action.ReadValue<Vector2>();
    }
    
    private Vector3 CalculateMovementDirection(Vector2 input)
    {
        Vector3 direction = new Vector3(input.x, 0, input.y).normalized;
        
        return direction;
    }
    
    private void RotateTowardsMovementDirection(Vector3 direction)
    {
        if (direction.magnitude >= 0.1f)
        {
            _targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;

            float smoothDampAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation,
                ref smoothTurnVelocity, smoothTurnTime);

            transform.rotation = Quaternion.Euler(0, smoothDampAngle, 0);
        }
    }

    private void MoveCharacter(Vector3 direction, float moveSpeed)
    {
        Vector3 normalizedMovementDirection = Quaternion.Euler(0f, _targetRotation, 0f) * Vector3.forward;

        characterController.Move(normalizedMovementDirection * (moveSpeed * Time.deltaTime));
    }

    private void Sprint()
    {
        if (sprint.action.WasPerformedThisFrame() && move.action.ReadValue<Vector2>().y > 0.1f && characterController.isGrounded)
        {
            isSprinting = true;
        
            MoveAndRotate(sprintSpeed);
        }
        
        isSprinting = false;
    }
    
    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
        sprint.action.Disable();
    }
}
