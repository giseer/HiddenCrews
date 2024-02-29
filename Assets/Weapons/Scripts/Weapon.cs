using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float range;
    
        [Header("Grips Settings")] 
        public GameObject RightHandGrip;
        public GameObject LeftHandGrip;
        public GameObject HandPlaceHolder;
        
        [Header("Particles Settings")]
        public ParticleSystem muzzleFlash;
}
