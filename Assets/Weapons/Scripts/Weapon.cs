using System;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float range;


    private void Update()
    {
        RaycastHit hitInfo;
        
        if (Physics.Raycast(transform.position, transform.position + transform.position * range, out hitInfo))
        {
            //Debug.Log(hitInfo.transform.tag);
        }
    }
}
