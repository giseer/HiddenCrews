using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEditor.Animations;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private InputActionReference shoot;
    
    [SerializeField] private Transform crossHairTarget;
    [SerializeField] private Rig handIk;
    [SerializeField] private Transform weaponContainer;
    [SerializeField] private Transform LeftGrip;
    [SerializeField] private Transform RightGrip;


    private Weapon weapon;

    private Animator animator;
    private AnimatorOverrideController weaponOverrideAnimator;
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        
        weaponOverrideAnimator = animator.runtimeAnimatorController as AnimatorOverrideController;

        Weapon existingWeapon = GetComponentInChildren<Weapon>();
        if (existingWeapon)
        {
            ChangeWeapon(existingWeapon);
        }
    }

    private void LateUpdate()
    {
        if (weapon)
        {
            if (shoot.action.WasPerformedThisFrame())
            {
                weapon.StartFiring();
            }

            if (weapon.isFiring)
            {
                weapon.UpdateFiring(Time.deltaTime);
            }
        
            weapon.UpdateBullets(Time.deltaTime);
        
            if (shoot.action.WasReleasedThisFrame())
            {
                weapon.StopFiring();   
            }   
        }
        else
        {
            handIk.weight = 0.0f;
            animator.SetLayerWeight(1, 0.0f);
        }
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        if(weapon)
        {
            Destroy(weapon.gameObject);
        }

        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.transform.parent = weaponContainer;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        
        handIk.weight = 1.0f;
        animator.SetLayerWeight(1, 1.0f);
        Invoke(nameof(SetWeaponAnimation), 0.01f);
    }

    private void SetWeaponAnimation()
    {
        weaponOverrideAnimator["weapon_Empty"] = weapon.weaponAnimation;
    }


    [ContextMenu("Save weapon pose")]
    void SaveWeaponPose()
    {
        GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
        recorder.BindComponentsOfType<Transform>(weaponContainer.gameObject, false);
        recorder.BindComponentsOfType<Transform>(LeftGrip.gameObject, false);
        recorder.BindComponentsOfType<Transform>(RightGrip.gameObject, false);
        recorder.TakeSnapshot(0.0f);
        recorder.SaveToClip(weapon.weaponAnimation);
        UnityEditor.AssetDatabase.SaveAssets();
    }
}
