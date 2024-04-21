using System;
using System.Collections;
using UnityEngine;

public class EnemyHealther : Healther
{
    [Header("Enemy Count")]
    public static int enemyCount;

    [Header("GameObjects Canvas")]
    public GameObject canvasProduct;
    public GameObject canvasProductSmall;
    public GameObject canvasEnemy;
    public GameObject doorController;

    [Header("Time")]
    public float timeDuration = 2f;

    private void Awake()
    {
        base.Awake();
        enemyCount++;
    }


    protected override void Die()
    {
        enemyCount--;
        Destroy(gameObject);

        if(enemyCount <= 0)
        {
            canvasProduct.SetActive(true);
            canvasEnemy.SetActive(false);
            doorController.SetActive(true);
        }
    }

    
}