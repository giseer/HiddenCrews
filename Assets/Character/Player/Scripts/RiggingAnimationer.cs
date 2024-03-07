﻿using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RiggingAnimationer : MonoBehaviour
{
    [Header("Transition Settings")]
    [SerializeField] private float transitionWeaponDuration = 0.3f;

    [Header("Rig Builder")] 
    [SerializeField] private RigBuilder rigBuilder;

    [Header("Place Holder Riggings")] 
    [SerializeField] private Transform commonPlaceHolder;
    [SerializeField] private MultiParentConstraint placeHolderParentConstraint;
    [SerializeField] private MultiPositionConstraint placeHolderPositionConstraint;
    
    [Header("Aim Holder Riggings")]
    [SerializeField] private Rig aimLayer;

    [SerializeField] private Transform commonAimHolder;
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

    private void LateUpdate()
    {
        rigBuilder.SyncLayers();
    }

    private void ActiveWeaponAnimationRiggs(Weapon activeWeapon)
    {
        // Constraints del PlaceHolder
        placeHolderParentConstraint.data.constrainedObject = activeWeapon.gameObject.transform;
        commonPlaceHolder.position = activeWeapon.WeaponPlaceHolder.transform.position;
        commonPlaceHolder.rotation = activeWeapon.WeaponPlaceHolder.transform.rotation;
        
        placeHolderPositionConstraint.data.constrainedObject = activeWeapon.WeaponPlaceHolder.transform;
        
        //Constraints del AimHolder 
        aimHolderAimConstraint.data.constrainedObject = activeWeapon.WeaponAimHolder.transform;
        
        aimHolderParentConstraint.data.constrainedObject = activeWeapon.transform;
        commonAimHolder.position = activeWeapon.WeaponAimHolder.transform.position;
        commonAimHolder.rotation = activeWeapon.WeaponAimHolder.transform.rotation;
        
        aimHolderPositionConstraint.data.constrainedObject = activeWeapon.transform;
        
        //Constraints de las manos
        leftHandK.data.target = activeWeapon.LeftHandGrip.transform;
        rightHandK.data.target = activeWeapon.RightHandGrip.transform;
        
        rigBuilder.Build();
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