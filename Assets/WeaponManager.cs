using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEditor.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class WeaponManager : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1,
        Tertiary = 2,
        Quaternary = 3,
        Quinary = 4,
        Senary = 5
    }
    
    [SerializeField] private InputActionReference shoot;
    [SerializeField] private InputActionReference saveWeapon;
    
    [SerializeField] private Transform crossHairTarget;
    public Animator rigController;
    
    [SerializeField] private Transform[] weaponSlots;

    private Weapon[] ownedWeapons;

    private int activeWeaponIndex;

    
    private void Awake()
    {
        Weapon existingWeapon = GetComponentInChildren<Weapon>();
        if (existingWeapon)
        {
            ChangeWeapon(existingWeapon);
        }
        
        ownedWeapons = new Weapon[weaponSlots.Length];
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

    private Weapon GetWeaponByIndex(int index)
    {
        if (index < 0 || index >= ownedWeapons.Length)
        {
            return null;
        }
        return ownedWeapons[index];
    }

    private void LateUpdate()
    {
        Weapon weapon = GetWeaponByIndex(activeWeaponIndex);
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
        int weaponSlotIndex = (int)newWeapon.weaponSlot;

        Weapon weapon = GetWeaponByIndex(weaponSlotIndex);
        
        if(weapon)
        {
            Destroy(weapon.gameObject);
        }

        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.transform.parent = weaponSlots[weaponSlotIndex];
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        rigController.Play("equip_" + weapon.weaponName);

        ownedWeapons[weaponSlotIndex] = weapon;
        activeWeaponIndex = weaponSlotIndex;
    }

    private void OnDisable()
    {
        saveWeapon.action.Disable();
    }
}
