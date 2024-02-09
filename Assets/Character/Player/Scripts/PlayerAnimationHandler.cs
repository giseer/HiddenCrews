using System;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void PerformMoveAnimation(Vector3 desiredDirection)
    {
        Debug.Log(desiredDirection.normalized);

        _animator.SetFloat("x", desiredDirection.normalized.x);
        _animator.SetFloat("z", desiredDirection.normalized.y);
    }
    
    public void PerformJumpAnimation()
    {
        _animator.SetTrigger("Jump");
    }
}
