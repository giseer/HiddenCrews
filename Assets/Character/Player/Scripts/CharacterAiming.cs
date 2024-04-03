using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    public float aimDuration = 0.3f;
    public Rig aimLayer;
    private RaycastWeapon weapon;

    private void Awake()
    {
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    private void LateUpdate()
    {
        if (aimLayer)
        {
            if (Input.GetMouseButton(1))
            {
                aimLayer.weight += Time.deltaTime / aimDuration;
            }
            else
            {
                aimLayer.weight -= Time.deltaTime / aimDuration;
            }    
        }

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
}
