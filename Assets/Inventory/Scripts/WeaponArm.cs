using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmaItem", menuName = "Items/Armas")]
public class WeaponArm : CustomMenuAsset
{
    public string ID;
    public string nombre;
    public Sprite icono;
    [TextArea] public string descripcion;

    public string tipoDeArma;

    [Header("Estadísticas del Arma")]
    public float daño;
    public float cadenciaDeDisparo;
    public float alcance;
}
