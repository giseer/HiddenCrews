using UnityEngine;

public class EnemyWeaponReloader : MonoBehaviour
{
    public void Reload(Weapon weapon)
    {
        weapon.currentAmmo = weapon.clipSize;
    }
}
