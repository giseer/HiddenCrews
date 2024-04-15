using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] Transform sight;

    [SerializeField] Weapon weapon;

    [SerializeField] float shootDistance;

    [SerializeField] LayerMask targetLayerMask;

    private EnemyMovement mover;

    bool isfiring;

    Transform targetSightTransform;

    private void Awake()
    {
        mover = GetComponentInChildren<EnemyMovement>();
    }

    private void FixedUpdate()
    {
        RaycastHit hitInfo;
        RaycastHit raycastHitInfo;

        if (Physics.SphereCast(weapon.raycastOrigin.position, shootDistance, transform.forward, out hitInfo, shootDistance, targetLayerMask))
        {
            targetSightTransform = hitInfo.transform.GetComponentInChildren<PlayerController>().sight;
           
            //Debug.DrawLine(sight.position, targetSightTransform.position);
            if (Physics.Raycast(sight.position, targetSightTransform.position - sight.position, out raycastHitInfo, shootDistance * 2, targetLayerMask))
            {
                isfiring = true;
                mover.Stop();
                weapon.StartFiring();
                
                
            }else
            {
                StopFiring();
            }
        }
        else
        {
            StopFiring();
        }
        
        if (isfiring)
        {
            Debug.Log("Firing");
            weapon.raycastDestination = targetSightTransform;
            transform.forward = targetSightTransform.position - sight.position;
            weapon.UpdateFiring(Time.deltaTime);
            weapon.UpdateBullets(Time.deltaTime);
        }
    }

    private void StopFiring()
    {
        weapon.StopFiring();
        isfiring = false;
        mover.Move();
    }
}
