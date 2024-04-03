using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Transform crossHairTarget;
    [SerializeField] private Rig handIk;
    [SerializeField] private Transform weapontContainer;
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
        if(weapon)
        {
            Destroy(weapon.gameObject);
        }

        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.transform.parent = weapontContainer;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        handIk.weight = 1f;
    }
}
