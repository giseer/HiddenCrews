using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    public float aimDuration = 0.3f;
    public Rig aimLayer;

    private void LateUpdate()
    {
        if (aimLayer)
        {
            // if (Input.GetMouseButton(1))
            // {
            //     aimLayer.weight += Time.deltaTime / aimDuration;
            // }
            // else
            // {
            //     aimLayer.weight -= Time.deltaTime / aimDuration;
            // }    
            aimLayer.weight = 1f;
        }
    }
}
