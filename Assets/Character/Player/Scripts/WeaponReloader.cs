using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponReloader : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference reload;

    [Header("Magazine Settings")]
    GameObject magazine;

    [Header("Weapon Settings")]
    [SerializeField] private WeaponManager weaponManager;

    [Header("Rigs Settings")]
    [SerializeField] private Animator rigController;
    [SerializeField] private Transform leftHand;

    [Header("Display Settings")]
    [SerializeField] AmmoDisplayer ammoDisplayer;

    [Header("Events")]
    public WeaponAnimationEventer animationEventer;

    private void OnEnable()
    {
        reload.action.Enable();
        animationEventer.weaponAnimationEvent.AddListener(OnAnimationEvent);
    }

    private void Update()
    {
        Weapon currentWeapon = weaponManager.GetCurrentWeapon();
        
        if (currentWeapon) 
        {
            if (reload.action.WasPerformedThisFrame() || currentWeapon.currentAmmo <= 0 && !currentWeapon.meleeWeapon)
            {
                rigController.SetTrigger("reload_weapon");
            }

            if (currentWeapon.isFiring)
            {
                ammoDisplayer.updateAmmoHUD(currentWeapon.currentAmmo, currentWeapon.clipSize);
            }
        }
    }


    private void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "AttachMagazine":
                AttachMagazine();
                break;

            case "DetachMagazine":
                DetachMagazine();
                break;

            case "GetMagazine":
                GetMagazine();
                break;

            case "DropMagazine":
                DropMagazine();
                break;
        }
    }


    private void AttachMagazine()
    {
        Weapon currentWeapon = weaponManager.GetCurrentWeapon();
        currentWeapon.magazine.SetActive(true);

        if (magazine)
        {
            Destroy(magazine);
        }

        currentWeapon.currentAmmo = currentWeapon.clipSize;
        rigController.ResetTrigger("reload_weapon");
        ammoDisplayer.updateAmmoHUD(currentWeapon.currentAmmo, currentWeapon.clipSize);
    }

    private void DetachMagazine()
    {
        Weapon currentWeapon = weaponManager.GetCurrentWeapon();

        magazine = Instantiate(currentWeapon.magazine, leftHand, true);
        currentWeapon.magazine.SetActive(false);
    }

    private void GetMagazine()
    {
        if (!magazine)
        {
            Weapon currentWeapon = weaponManager.GetCurrentWeapon();
            magazine = Instantiate(currentWeapon.magazine, leftHand, true);
        }
        
        magazine.SetActive(true);
    }

    private void DropMagazine()
    {
        GameObject droppedMagazine = Instantiate(magazine, magazine.transform.position, magazine.transform.rotation);
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.AddComponent<BoxCollider>();
        magazine.SetActive(false);

        if (droppedMagazine)
        {
            Destroy(droppedMagazine, 15f);   
        }
    }


    private void OnDisable()
    {
        reload.action.Disable();
        animationEventer.weaponAnimationEvent.RemoveListener(OnAnimationEvent);
    }
}
