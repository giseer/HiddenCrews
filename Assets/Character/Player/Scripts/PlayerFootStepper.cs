using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FootRig
{
    public Transform knee;
    public Transform step;
}

public class PlayerFootStepper : MonoBehaviour
{
    [SerializeField] private FootRig[] footRigs = new FootRig[2]; 

    private void Update()
    {
        foreach(FootRig footRig in footRigs)
        {
            RaycastHit hitInfo;

            if(Physics.Raycast(footRig.knee.position, -transform.up, out hitInfo))
            {
                footRig.step.position = hitInfo.point;
                footRig.step.up = hitInfo.normal;
            }

        }
    }
}
