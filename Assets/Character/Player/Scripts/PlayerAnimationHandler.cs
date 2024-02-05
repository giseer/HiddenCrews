using System;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void PerformMoveAnimation(Vector2 movementValues)
    {
        if (movementValues == Vector2.zero)
        {
            _animator.SetBool("Walking", false);
            return;
        }
        
        _animator.SetBool("Walking", true);
    }
    
    public void PerformJumpAnimation()
    {
        _animator.SetTrigger("Jump");
    }
}
