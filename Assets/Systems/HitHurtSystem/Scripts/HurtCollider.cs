using UnityEngine;
using UnityEngine.Events;

public class HurtCollider : MonoBehaviour
{
    [SerializeField] public UnityEvent<HitCollider,HurtCollider> onHitRecived;

    internal void NotifyHit(HitCollider hitCollider)
    {
        onHitRecived.Invoke(hitCollider, this);
    }
}
