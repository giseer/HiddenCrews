using System;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float range;


    private void Update()
    {
        RaycastHit hitInfo;
        
        if (Physics.Raycast(transform.position, transform.position + transform.position * range, out hitInfo))
        {
            Debug.Log(hitInfo.transform.tag);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, target.position);
    }
}
