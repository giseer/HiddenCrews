using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RiggingAnimationer : MonoBehaviour
{
    [Header("Transition Settings")]
    [SerializeField] private float transitionWeaponDuration = 0.3f;
    
    [Header("Riggings")]
    [SerializeField] private Rig aimLayer;

    [SerializeField] private TwoBoneIKConstraint rightHandK;
    [SerializeField] private TwoBoneIKConstraint leftHandK;
    
    public void PerformAim()
    {
        aimLayer.weight += Time.deltaTime / transitionWeaponDuration;
        DOTween.To(()=> leftHandK.weight, x=> leftHandK.weight = x, 1f, 0.5f);
    }

    public void PerformReleaseAim()
    {
       aimLayer.weight -= Time.deltaTime / transitionWeaponDuration;
       DOTween.To(()=> leftHandK.weight, x=> leftHandK.weight = x, 0f, 0.5f);
    }
}