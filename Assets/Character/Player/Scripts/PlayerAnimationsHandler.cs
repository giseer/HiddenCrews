using UnityEngine;

public class PlayerAnimationsHandler : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private Animator animator;
    
    public void ActiveAimingAnimations()
    {   
        animator.SetBool("Aiming", true);
    }
    
    public void DesactiveAimingAnimations()
    {   
        animator.SetBool("Aiming", false);
    }
}
