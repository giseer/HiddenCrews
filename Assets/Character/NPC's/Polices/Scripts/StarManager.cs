using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    public GameObject starPrefab;
    public GameObject starPrefab1;
    public GameObject starPrefab2;
    //public GameObject starPrefab3;
    //public GameObject starPrefab4;
    private bool isPlayerInsideRange = false;

    
    public void ActivateStars()
    {
        int numEnemies = enemyMovement.enemyList.Count;
        if (numEnemies == 1)
        {
            starPrefab.SetActive(true);
            
        }
        if (numEnemies == 2)
        {
            starPrefab.SetActive(true);
            starPrefab1.SetActive(true);
            
        }
        if (numEnemies == 3)
        {
            starPrefab.SetActive(true);
            starPrefab1.SetActive(true);
            
        }
        if (numEnemies == 4)
        {
            starPrefab.SetActive(true);
            starPrefab1.SetActive(true);
            starPrefab2.SetActive(true);
            //starPrefab3.SetActive(true);
            
        }

        else
        {
            starPrefab.SetActive(true);
        }
            
    }

    public void DeactivateStars()
    {
        starPrefab.SetActive(false);
        starPrefab1.SetActive(false);
        starPrefab2.SetActive(false);
        //starPrefab3.SetActive(false);
        //starPrefab4.SetActive(false);

    }

    public void SpawnTwoEnemiesAndStars()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length >= 2)
        {
            Debug.Log("Ya hay 2 enemigos en la escena.");
            return;
        }

        Vector3 playerPosition = enemyMovement.target.position;

        for (int i = 0; i < 2 - enemies.Length; i++)
        {
            Vector3 spawnPosition = playerPosition + Random.insideUnitSphere * enemyMovement.spawnRadius;

            spawnPosition.y = 0f;

            GameObject newEnemy = Instantiate(enemyMovement.enemyPrefab, spawnPosition, Quaternion.identity);
            enemyMovement.enemyList.Add(newEnemy);
        }

        int numEnemies = enemyMovement.enemyList.Count;
        if (numEnemies >= 1)
        {
            
            starPrefab1.SetActive(true);
        }
        
    }

    public void SpawnThreeEnemiesAndStars()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length >= 3)
        {
            Debug.Log("Ya hay 3 enemigos en la escena.");
            return;
        }

        Vector3 playerPosition = enemyMovement.target.position;

        for (int i = 0; i < 3 - enemies.Length; i++)
        {
            Vector3 spawnPosition = playerPosition + Random.insideUnitSphere * enemyMovement.spawnRadius;

            spawnPosition.y = 0f;

            GameObject newEnemy = Instantiate(enemyMovement.enemyPrefab, spawnPosition, Quaternion.identity);
            enemyMovement.enemyList.Add(newEnemy);
        }

        int numEnemies = enemyMovement.enemyList.Count;
        if (numEnemies >= 2)
        {
            starPrefab2.SetActive(true);
        }

    }

    public void SpawnFourEnemiesAndStars()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length >= 4)
        {
            Debug.Log("Ya hay 4 enemigos en la escena.");
            return;
        }

        Vector3 playerPosition = enemyMovement.target.position;

        for (int i = 0; i < 4 - enemies.Length; i++)
        {
            Vector3 spawnPosition = playerPosition + Random.insideUnitSphere * enemyMovement.spawnRadius;

            spawnPosition.y = 0f;

            GameObject newEnemy = Instantiate(enemyMovement.enemyPrefab, spawnPosition, Quaternion.identity);
            enemyMovement.enemyList.Add(newEnemy);
        }

        int numEnemies = enemyMovement.enemyList.Count;
        if (numEnemies >= 3)
        {
            //starPrefab3.SetActive(true);
        }

    }

    public void SpawnFiveEnemiesAndStars()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length >= 5)
        {
            Debug.Log("Ya hay 5 enemigos en la escena.");
            return;
        }

        Vector3 playerPosition = enemyMovement.target.position;

        for (int i = 0; i < 5 - enemies.Length; i++)
        {
            Vector3 spawnPosition = playerPosition + Random.insideUnitSphere * enemyMovement.spawnRadius;

            spawnPosition.y = 0f;

            GameObject newEnemy = Instantiate(enemyMovement.enemyPrefab, spawnPosition, Quaternion.identity);
            enemyMovement.enemyList.Add(newEnemy);
        }

        int numEnemies = enemyMovement.enemyList.Count;
        if (numEnemies >= 4)
        {
            //starPrefab4.SetActive(true);
        }

    }
}
