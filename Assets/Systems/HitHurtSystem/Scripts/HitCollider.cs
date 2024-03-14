using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitCollider : MonoBehaviour
{
    [HideInInspector] public UnityEvent<HitCollider, HurtCollider> onHitDelivered; 
    
    [SerializeField] private List<string> hittableTags = new List<string> {"Untagged"};

    private void OnCollisionEnter(Collision collision)
    {
        CheckHit(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckHit(other);
    }

    private void CheckHit(Collider other)
    {
        if (hittableTags.Contains(other.tag))
        {
            other.GetComponent<HurtCollider>()?.NotifyHit(this);
            onHitDelivered?.Invoke(this, other.GetComponent<HurtCollider>());
        }
    }
}
