using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ProductItem", menuName = "Items/Productos")]
public class Products : CustomMenuAsset
{
    public string ID;
    public string nombre;
    public Sprite icono;
    [TextArea] public string descripcion;

    public string tipoDeProducto;
    public int cantidad;
}
