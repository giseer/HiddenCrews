using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Weapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }

    public WeaponManager.WeaponSlot weaponSlot;
    
    public bool isFiring = false;

    [SerializeField] private int fireRate = 25;

    [SerializeField] private float bulletSpeed = 1000f;

    [SerializeField] private float bulletDrop = 0f;

    public ParticleSystem muzzleFlash;

    public ParticleSystem hitEffect;

    public TrailRenderer tracerEffect;

    public string weaponName;

    [SerializeField] private Transform raycastOrigin;

    public Transform raycastDestination;

    public bool meleeWeapon;
    
    private Ray ray;
    private RaycastHit hitInfo;
    private float accumulatedTime;
    
    List<Bullet> bullets = new List<Bullet>();
    private float maxLifeTime = 3f;

    [Header("Magazine Settings")]
    public GameObject magazine;
    public int currentAmmo;
    public int clipSize;



    Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);
    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0f;
        bullet.tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
    }
    
    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0;
        FireBullet();
    }

    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while (accumulatedTime >= 0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }

    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0,p1,bullet);
        });
    }
    
    private void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time >= maxLifeTime);
    }

    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = (end - start).magnitude;
        ray.origin = start;
        ray.direction = direction;
        
        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            if (bullet.tracer)
            {
                bullet.tracer.transform.position = hitInfo.point;
                bullet.time = maxLifeTime;   
            }

            Rigidbody rigidbody = hitInfo.collider.GetComponent<Rigidbody>();
            if (rigidbody)
            {
                rigidbody.AddForceAtPosition(ray.direction * 20, hitInfo.point, ForceMode.Impulse);
            }
        }
        else
        {
            if (bullet.tracer)
            {
                bullet.tracer.transform.position = end;   
            }
        }
    }

    private void FireBullet()
    {
        if(currentAmmo <= 0)
        {
            return;
        }

        currentAmmo--;

        muzzleFlash.Emit(1);

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        Bullet bullet = CreateBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
