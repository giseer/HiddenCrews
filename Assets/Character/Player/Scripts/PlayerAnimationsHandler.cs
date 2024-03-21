using UnityEngine;

public class PlayerAnimationsHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void AnimateMovement(Vector3 normalizedLocalDirection)
    {
        animator.SetFloat("x", normalizedLocalDirection.x);
        animator.SetFloat("z", normalizedLocalDirection.z);
    }

    public void AnimateJump()
    {
        animator.SetTrigger("Jump");
    }
}
