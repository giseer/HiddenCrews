using System;
using UnityEngine;

public class PlayerHealther : Healther
{
    protected override void Die()
    {
        //Die animation - Temporalmente se usa destroy directamente pero lo que se tendria que hacer es activar
        //                  el trigger de la animacion de muerte y esta cuando acabara salir del juego al menu principal o algun otro

        Destroy(transform.parent.gameObject);
    }
}