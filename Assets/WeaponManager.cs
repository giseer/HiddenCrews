using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEditor.Animations;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private InputActionReference shoot;
    [SerializeField] private InputActionReference saveWeapon;
    
    [SerializeField] private Transform crossHairTarget;
    [SerializeField] private Rig handIk;
    [SerializeField] private Transform weaponContainer;
    [SerializeField] private Transform LeftGrip;
    [SerializeField] private Transform RightGrip;
    public Animator rigController;


    private Weapon weapon;

    
    private void Awake()
    {
        Weapon existingWeapon = GetComponentInChildren<Weapon>();
        if (existingWeapon)
        {
            ChangeWeapon(existingWeapon);
        }
    }

    private void OnEnable()
    {
        saveWeapon.action.Enable();
    }

    private void Start()
    {
        if(rigController)
        {
            rigController.updateMode = AnimatorUpdateMode.Fixed;
            rigController.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
            rigController.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            rigController.updateMode = AnimatorUpdateMode.Normal;
        }
    }

    private void LateUpdate()
    {
        if (weapon)
        {
            if (!weapon.meleeWeapon)
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

           if(saveWeapon.action.WasPerformedThisFrame())
            {
                bool weaponSaved = rigController.GetBool("save_weapon");
                rigController.SetBool("save_weapon", !weaponSaved);
            }
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
        rigController.Play("equip_" + weapon.weaponName);
    }

    private void OnDisable()
    {
        saveWeapon.action.Disable();
    }
}
