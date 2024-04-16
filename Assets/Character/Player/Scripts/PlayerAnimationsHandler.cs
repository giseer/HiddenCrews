using UnityEngine;
using DG.Tweening;

public class PlayerAnimationsHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void AnimateMovement(Vector3 normalizedLocalDirection)
    {
        DOTween.To(() => animator.GetFloat("x"),
            x => animator.SetFloat("x", x),
            normalizedLocalDirection.x, 1f);

        DOTween.To(() => animator.GetFloat("z"),
            x => animator.SetFloat("z", x),
            normalizedLocalDirection.z, 1f);

        //animator.SetFloat("x", normalizedLocalDirection.x);
        //animator.SetFloat("z", normalizedLocalDirection.z);
    }

    public void AnimateJump()
    {
        animator.SetTrigger("Jump");
    }
}
