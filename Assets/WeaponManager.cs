using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEditor.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using System.Collections;

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

    [Header("Input Actions")]
    [SerializeField] private InputActionReference shoot;
    [SerializeField] private InputActionReference saveWeapon;
    [SerializeField] private InputActionReference Weapon1;
    [SerializeField] private InputActionReference Weapon2;
    [SerializeField] private InputActionReference Weapon3;
    [SerializeField] private InputActionReference Weapon4;
    [SerializeField] private InputActionReference WeaponScroll;

    [Header("Weapon settings")]
    [SerializeField] private Transform crossHairTarget;
    public Animator rigController;
    
    [SerializeField] private Transform[] weaponSlots;

    public Weapon[] ownedWeapons;

    public int currentWeaponIndex;

    private bool isWeaponSaved = false;

    [Header("Display Settings")]
    [SerializeField] AmmoDisplayer ammoDisplayer;
    
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
        shoot.action.Enable();
        saveWeapon.action.Enable();
        Weapon1.action.Enable();
        Weapon2.action.Enable();
        Weapon3.action.Enable();
        Weapon4.action.Enable();
        WeaponScroll.action.Enable();    
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
        Weapon weapon = GetWeaponByIndex(currentWeaponIndex);
        if (weapon && !isWeaponSaved)
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
        }
        
        if(saveWeapon.action.WasPerformedThisFrame())
        {
            ToggleSelectedWeapon();
        }

        if(Weapon1.action.WasPerformedThisFrame())
        {
            SelectWeapon(WeaponSlot.Primary);
        }
        
        if(Weapon2.action.WasPerformedThisFrame())
        {
            SelectWeapon(WeaponSlot.Secondary);
        }

        if (Weapon3.action.WasPerformedThisFrame())
        {
            SelectWeapon(WeaponSlot.Tertiary);
        }

        if (Weapon4.action.WasPerformedThisFrame())
        {
            SelectWeapon(WeaponSlot.Quaternary);
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
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        ownedWeapons[weaponSlotIndex] = weapon;

        SelectWeapon(newWeapon.weaponSlot);

        ammoDisplayer.updateAmmoHUD(weapon.currentAmmo, weapon.clipSize);
    }

    private void ToggleSelectedWeapon()
    {
        bool isSaved = rigController.GetBool("save_weapon");
        if(isSaved)
        {
            StartCoroutine(SelectedWeapon(currentWeaponIndex));
        }
        else
        {
            StartCoroutine(SaveWeapon(currentWeaponIndex));
        }
    }

    private void SelectWeapon(WeaponSlot weaponSlot)
    {
        int saveWeaponIndex = currentWeaponIndex;
        int selectedWeaponIndex = (int)weaponSlot;
        
        if(saveWeaponIndex == selectedWeaponIndex)
        {
            saveWeaponIndex = -1;
        }

        StartCoroutine(SwitchWeapon(saveWeaponIndex, selectedWeaponIndex));
    }

    private void SelectWeaponByScroll()
    {
        Vector2 prevNextWeaponValue = WeaponScroll.action.ReadValue<Vector2>();
        if (prevNextWeaponValue.y > 0)
        {
            SelectWeapon((WeaponSlot)currentWeaponIndex + 1);
        }
        else if(prevNextWeaponValue.y < 0)
        {
            SelectWeapon((WeaponSlot)currentWeaponIndex - 1);
        }
    }

    IEnumerator SwitchWeapon(int saveWeaponIndex, int selectedWeaponIndex)
    {
        yield return StartCoroutine(SaveWeapon(saveWeaponIndex));
        yield return StartCoroutine(SelectedWeapon(selectedWeaponIndex));
        currentWeaponIndex = selectedWeaponIndex;
    }

    IEnumerator SaveWeapon(int index)
    {
        isWeaponSaved = true;
        yield return new WaitForSeconds(0.1f);

        Weapon weapon = GetWeaponByIndex(index);
        if(weapon)
        {
            rigController.SetBool("save_weapon", true);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
    }
    IEnumerator SelectedWeapon(int index)
    {
        Weapon weapon = GetWeaponByIndex(index);
        if (weapon)
        {
            rigController.SetBool("save_weapon", false);
            rigController.Play("equip_" + weapon.weaponName);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
            isWeaponSaved = false;
        }
    }

    public Weapon GetCurrentWeapon()
    {
        return GetWeaponByIndex(currentWeaponIndex);
    }

    private void OnDisable()
    {
        shoot.action.Disable();
        saveWeapon.action.Disable();
        Weapon1.action.Disable();
        Weapon2.action.Disable();
        Weapon3.action.Disable();
        Weapon4.action.Disable();
        WeaponScroll.action.Disable();
    }
}
