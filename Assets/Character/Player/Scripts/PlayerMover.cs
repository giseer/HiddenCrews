using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] private float speed = 1.1f;

    [SerializeField] private float sprintSpeed = 3f;
    
    private Vector3 _velocity;
    
    [Header("Jump Values")]
    public float jumpForce = 1f;
    
    [Header("Camera Values")]
    public Transform mainCamera;

    [Header("Constant Values")] 
    private const float GRAVITY = -9.81f;
    
    [Header("Components")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;

    private void LateUpdate()
    {
        UpdateVerticalMovement();
    }

    private void UpdateVerticalMovement()
    {
        PerformGravity();
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
    
    public void Jump()
    {
        if (characterController.isGrounded)
        {
            _velocity.y = jumpForce;
        }
    }

    public void MoveAndRotate(Vector2 movementInput, bool isSprinting)
    {
        Vector3 movementDirection = CalculateMovementDirection(movementInput);
        
        MoveCharacter(movementDirection, speed, isSprinting);
    }
    
    private Vector3 CalculateMovementDirection(Vector2 input)
    {
        Vector3 normalizedInput = new Vector3(input.x, 0, input.y).normalized;
        Vector3 cameraRight = mainCamera.right;
        Vector3 cameraForward = mainCamera.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();
        
        Vector3 direction = 
            cameraRight * normalizedInput.x +
            cameraForward * normalizedInput.z;
        
        return direction;
    }
    
    private void MoveCharacter(Vector3 direction, float moveSpeed, bool isSprinting)
    {
        float currentSpeed = isSprinting ? sprintSpeed : speed;
        
        characterController.Move(direction * (currentSpeed * Time.deltaTime));

        Vector3 normalizedLocalDirection = transform.InverseTransformDirection(direction).normalized;

        normalizedLocalDirection = isSprinting ? normalizedLocalDirection * 2 : normalizedLocalDirection;
        
        animator.SetFloat("x", normalizedLocalDirection.x);
        animator.SetFloat("z", normalizedLocalDirection.z);
    }
}