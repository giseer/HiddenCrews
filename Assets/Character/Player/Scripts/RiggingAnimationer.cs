using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RiggingAnimationer : MonoBehaviour
{
    [Header("Transition Settings")]
    [SerializeField] private float transitionWeaponDuration = 0.3f;

    [Header("Place Holder Riggings")] 
    [SerializeField] private MultiParentConstraint placeHolderParentConstraint;
    [SerializeField] private MultiPositionConstraint placeHolderPositionConstraint;
    
    
    [Header("Aim Holder Riggings")]
    [SerializeField] private Rig aimLayer;

    [SerializeField] private MultiAimConstraint aimHolderAimConstraint;
    [SerializeField] private MultiParentConstraint aimHolderParentConstraint;
    [SerializeField] private MultiPositionConstraint aimHolderPositionConstraint;
    
    [Header("Weapon Riggings")]
    [SerializeField] private TwoBoneIKConstraint rightHandK;
    [SerializeField] private TwoBoneIKConstraint leftHandK;

    [Header("Weapon Settings")] 
    private PlayerAimer aimer;

    private void Awake()
    {
        aimer = GetComponentInChildren<PlayerAimer>();
    }

    private void OnEnable()
    {
        aimer.onWeaponChange.AddListener(ActiveWeaponAnimationRiggs);
    }

    private void ActiveWeaponAnimationRiggs()
    {
        Debug.Log("Wha happen");
        
        // Constraints del PlaceHolder
        placeHolderParentConstraint.data.constrainedObject = aimer.activeWeapon.gameObject.transform;
        placeHolderParentConstraint.data.sourceObjects.Clear();
        placeHolderParentConstraint.data.sourceObjects.Add(new WeightedTransform(aimer.activeWeapon.WeaponPlaceHolder.transform, 1f));
        //placeHolderParentConstraint.data.sourceObjects[0] = new WeightedTransform(aimer.activeWeapon.WeaponPlaceHolder.transform,1f);
        
        placeHolderPositionConstraint.data.constrainedObject = aimer.activeWeapon.WeaponPlaceHolder.transform;
        
        //Constraints del AimHolder 
        aimHolderAimConstraint.data.constrainedObject = aimer.activeWeapon.WeaponAimHolder.transform;
        aimHolderParentConstraint.data.constrainedObject = aimer.activeWeapon.transform;
        aimHolderParentConstraint.data.sourceObjects.Clear();
        aimHolderParentConstraint.data.sourceObjects.Add(new WeightedTransform(aimer.activeWeapon.WeaponAimHolder.transform, 1f));
        aimHolderPositionConstraint.data.constrainedObject = aimer.activeWeapon.transform;

    }
    
    public void PerformAim()
    {
        aimLayer.weight += Time.deltaTime / transitionWeaponDuration;
        DOTween.To(()=> leftHandK.weight, x=> leftHandK.weight = x, 1f, 0.5f);
    }

    public void PerformReleaseAim()
    {
       aimLayer.weight -= Time.deltaTime / transitionWeaponDuration;
       DOTween.To(()=> leftHandK.weight, x=> leftHandK.weight = x, 1f, 0.5f);
    }

    private void OnDisable()
    {
        aimer.onWeaponChange.RemoveListener(ActiveWeaponAnimationRiggs);
    }
}