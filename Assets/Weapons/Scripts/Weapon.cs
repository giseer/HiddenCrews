using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")] 
    public string name;
    public float range;
    
    [Header("Grips Settings")] 
    public GameObject RightHandGrip;
    public GameObject LeftHandGrip;
    
    public GameObject WeaponPlaceHolder;
    
    public GameObject WeaponAimHolder;
    
    [Header("Particles Settings")]
    public ParticleSystem muzzleFlash;
}
