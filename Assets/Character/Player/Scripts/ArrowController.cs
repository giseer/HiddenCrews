using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Transform player; 
    public Transform target; 
    public Vector3 offset; 

    void Update()
    {
        if (player != null && target != null)
        {
            Vector3 directionToTarget = (target.position - player.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            transform.rotation = Quaternion.Euler(90f, targetRotation.eulerAngles.y, 0f);

            Vector3 posicionAdelante = player.position + player.forward * offset.x + player.up * offset.y + player.right * offset.z;

            transform.position = posicionAdelante;
        }
    }
}
