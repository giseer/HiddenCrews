using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [Header("Movements Values")]
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float jumpSpeed = 5f;
    
    [Header("Components")]
    private CharacterController _characterController;
    private Camera _mainCamera;
    [SerializeField] private PlayerAnimationHandler animatorHandler;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
    }

    public float verticalVelocity;
    
    private Vector3 _smoothedMoveXZLocal = Vector3.zero;
    
    [HideInInspector] public Vector3 currentVelocity = Vector3.zero;
    
    private float _movementSmoothingSpeed = 5f;
    
    public void UpdateHorizontalMovement(Vector2 rawMove)
    {
        // Coloca las coordenadas de la cámara como un plano en el suelo,
        // esto permite aplicar el movimiento correctamente.

        Vector3 moveVector = CalculateMoveVector(rawMove);
        MoveCharacter(moveVector);
        SmoothMovement(moveVector);
    }
    
    private Vector3 CalculateMoveVector(Vector2 rawMoveInput)
    {
        Vector3 cameraForward = GetCameraForwardOnGround();
        Vector3 moveXZ = CalculateMoveXZ(rawMoveInput, cameraForward);
    
        return moveXZ.normalized * movementSpeed;
    }
    
    private Vector3 GetCameraForwardOnGround()
    {
        Vector3 cameraForward = _mainCamera.transform.forward;
        cameraForward.y = 0f;
        return cameraForward.normalized;
    }
    
    private Vector3 CalculateMoveXZ(Vector2 rawMoveInput, Vector3 cameraForward)
    {
        return cameraForward * rawMoveInput.y +
               _mainCamera.transform.right * rawMoveInput.x;
    }

    private void MoveCharacter(Vector3 moveVector)
    {
        currentVelocity = moveVector;
        _characterController.Move(currentVelocity * Time.deltaTime);
    }

    private void SmoothMovement(Vector3 moveVector)
    {
        Vector3 moveXZLocal = TransformDirectionToLocal(moveVector);
        Vector3 smoothingDirection = CalculateSmoothingDirection(moveXZLocal);
    
        float smoothingToApply = CalculateSmoothingAmount(smoothingDirection);
        _smoothedMoveXZLocal += smoothingDirection.normalized * smoothingToApply;
        animatorHandler.PerformMoveAnimation(_smoothedMoveXZLocal);
    }

    private Vector3 TransformDirectionToLocal(Vector3 worldDirection)
    {
        return transform.InverseTransformDirection(worldDirection);
    }

    private Vector3 CalculateSmoothingDirection(Vector3 moveXZLocal)
    {
        return moveXZLocal - _smoothedMoveXZLocal;
    }

    private float CalculateSmoothingAmount(Vector3 smoothingDirection)
    {
        float smoothingToApply = _movementSmoothingSpeed * Time.deltaTime;
        return Mathf.Min(smoothingToApply, smoothingDirection.magnitude);
    }
    

    private float _gravity = -9.8f;
    public void UpdateVerticalMovement()
    {
        ApplyGravity();
        PerformJump();
    }

    private void PerformJump()
    {
        if (_characterController.isGrounded)
        {
            verticalVelocity = jumpSpeed;
        }
    }

    private void ApplyGravity()
    {
        verticalVelocity += _gravity * Time.deltaTime;
        
        _characterController.Move(Vector3.up * (verticalVelocity * Time.deltaTime));

        if (!_characterController.isGrounded)
        {
            return;
        }
            
        verticalVelocity = 0f;
    }
}
