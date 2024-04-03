using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] private Weapon weaponToPickup;

    private WeaponManager weaponManager;

    private void OnTriggerEnter(Collider other)
    {
        weaponManager = other.GetComponent<WeaponManager>();
        
        if(weaponManager)
        {
            Weapon weaponToInstantiate = Instantiate(weaponToPickup);
            weaponManager.ChangeWeapon(weaponToInstantiate);
        }
    }
}
