using System;
using System.Collections;
using UnityEngine;

public class EnemyHealther : Healther
{
    public static int enemyCount;
    public GameObject canvasProduct;
    public GameObject canvasProductSmall;
    public GameObject canvasEnemy;
    public float timeDuration = 2f;

    private void Awake()
    {
        base.Awake();
        enemyCount++;
    }


    protected override void Die()
    {
        //Die animation - Temporalmente se usa destroy directamente pero lo que se tendria que hacer es activar
        //                  el trigger de la animacion de muerte y esta cuando acabara salir del juego al menu principal o algun otro

        enemyCount--;
        Destroy(gameObject);

        if(enemyCount <= 0)
        {
            canvasProduct.SetActive(true);
            canvasEnemy.SetActive(false);
        }
    }

    
}