using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponManager : MonoBehaviour
{
    public Transform crossHairTarget;
    public Rig handIk;
    private Weapon weapon;
    
    private void Awake()
    {
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
            if (Input.GetButtonDown("Fire1"))
            {
                weapon.StartFiring();
            }

            if (weapon.isFiring)
            {
                weapon.UpdateFiring(Time.deltaTime);
            }
        
            weapon.UpdateBullets(Time.deltaTime);
        
            if (Input.GetButtonUp("Fire1"))
            {
                weapon.StopFiring();   
            }   
        }
        else
        {
            handIk.weight = 0f;
        }
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        handIk.weight = 1f;
    }
}
