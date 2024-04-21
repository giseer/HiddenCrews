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
    }

    public void StopAnimateMovement()
    {
        DOTween.To(() => animator.GetFloat("x"),
            x => animator.SetFloat("x", x),
            0, 1f);

        DOTween.To(() => animator.GetFloat("z"),
            x => animator.SetFloat("z", x),
            0, 1f);
    }

    public void AnimateJump()
    {
        animator.SetTrigger("Jump");
    }
}
