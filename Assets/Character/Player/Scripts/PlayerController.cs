using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")] 
    private PlayerInputHandler _playerInput;
    private PlayerMover _mover;
    private PlayerOrientationer _orientationer;
    private PlayerAnimationHandler _animationHandler;

    private void Awake()
    {
        _playerInput = GetComponentInChildren<PlayerInputHandler>();
        _mover = GetComponentInChildren<PlayerMover>();
        _orientationer = GetComponentInChildren<PlayerOrientationer>();
        _animationHandler = GetComponentInChildren<PlayerAnimationHandler>();
    }

    private void OnEnable()
    {
        _playerInput.onMove.AddListener(PerformMovement);
        _playerInput.onJump.AddListener(PerformJump);
    }
    
    private void PerformMovement(Vector2 movementValues)
    {
        _mover.UpdateHorizontalMovement(movementValues);
        _animationHandler.PerformMoveAnimation(movementValues);
    }
    
    private void PerformJump()
    {
        _mover.UpdateVerticalMovement();
        _animationHandler.PerformJumpAnimation();
    }
    
    private void OnDisable()
    {
        _playerInput.onMove.RemoveListener(_mover.UpdateHorizontalMovement);
        _playerInput.onJump.RemoveListener(_mover.UpdateVerticalMovement);
    }
}
