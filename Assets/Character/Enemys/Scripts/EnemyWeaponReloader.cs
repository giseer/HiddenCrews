using UnityEngine;
using UnityEngine.Audio;

public class EnemyWeaponReloader : MonoBehaviour
{
    public void Reload(Weapon weapon)
    {
        weapon.audioSource.clip = weapon.reloadSound;
        weapon.audioSource.Play();
        weapon.currentAmmo = weapon.clipSize;
    }
}
