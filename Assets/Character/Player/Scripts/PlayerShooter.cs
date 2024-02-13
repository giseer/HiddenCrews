using System;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.forward, transform.forward * 10f);
    }
}
