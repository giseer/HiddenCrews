using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] Transform sight;

    [SerializeField] Weapon weapon;

    [SerializeField] float shootDistance;
    [SerializeField] private float aimSpeed;

    [SerializeField] LayerMask targetLayerMask;

    private EnemyMovement mover;

    bool isfiring;

    Transform targetSightTransform;

    private EnemyWeaponReloader reloader;

    private void Awake()
    {
        mover = GetComponentInChildren<EnemyMovement>();
        reloader = GetComponentInChildren<EnemyWeaponReloader>();
    }

    private void FixedUpdate()
    {
        RaycastHit raycastHitInfo;

        Collider[] detectedColliders = Physics.OverlapSphere(
                                        weapon.raycastOrigin.position + transform.forward * shootDistance,
                                        shootDistance,
                                        targetLayerMask);

        if (detectedColliders.Length > 0)
        {
            foreach (Collider detectedCollider in detectedColliders)
            {
                targetSightTransform = detectedCollider.transform.GetComponentInChildren<PlayerController>().sight;

                if (Physics.Raycast(sight.position, targetSightTransform.position - sight.position, out raycastHitInfo, shootDistance * 2))
                {

                    if (LayerMask.GetMask(LayerMask.LayerToName(raycastHitInfo.transform.gameObject.layer)) == targetLayerMask)
                    {
                        isfiring = true;
                        mover.Stop();
                        weapon.StartFiring();
                    }
                    else
                    {
                        StopFiring();
                    }
                }
            }
        }
        else
        {
            StopFiring();
        }


        if (isfiring)
        {
            weapon.raycastDestination = targetSightTransform;
            AimTarget();
            weapon.UpdateFiring(Time.deltaTime);
            weapon.UpdateBullets(Time.deltaTime);

            if (weapon.currentAmmo <= 0)
            {
                reloader.Reload(weapon);
            }
        }
    }

    private void AimTarget()
    {
        Vector3 desiredDirection = targetSightTransform.position - sight.position;
        transform.forward = Vector3.Slerp(sight.transform.forward, desiredDirection, aimSpeed * Time.deltaTime);
    }

    private void StopFiring()
    {
        weapon.StopFiring();
        isfiring = false;
        mover.Move();
    }
}