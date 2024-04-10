using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform target; // El objeto a seguir (usualmente el jugador)
    public GameObject enemyPrefab; // Prefab del enemigo
    public GameObject starPrefab; // Prefab de la estrella
    public GameObject starPrefab1; // Prefab de la estrella
    public float detectionRange = 7f;
    public float spawnRadius = 5f;

    public float normalSpeed = 3f;
    public float boostedSpeed = 6f;
    public float boostDuration = 2f; // Duraci�n de la velocidad aumentada
    public float decreaseDuration = 2f; // Duraci�n de la disminuci�n de velocidad
    public float minSpeed = 1f; // Velocidad m�nima permitida
    public float decreaseRate = 1f; // Tasa de disminuci�n gradual

    public float moveSpeed = 5f;
    public Transform[] waypoints;
    private int currentWaypoint = 0;

    private bool isFollowingPlayer = false;

    public List<GameObject> enemyList = new List<GameObject>();
    

    void Start()
    {
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();

        navMeshAgent.speed = normalSpeed;

        StartCoroutine(ChangeSpeedOverTime());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SpawnEnemiesAndStars();
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget < detectionRange)
        {
            if (!isFollowingPlayer)
            {
                isFollowingPlayer = true;
                ActivateStar(); // Activar la estrella cuando el enemigo detecta al jugador
            }

            navMeshAgent.speed = boostedSpeed;
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            if (isFollowingPlayer)
            {
                isFollowingPlayer = false;
                DeactivateStar(); // Desactivar la estrella cuando el enemigo pierde al jugador
            }

            navMeshAgent.speed = normalSpeed;
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
            {
                SetDestinationToNextWaypoint();
            }
        }

        
    }

    IEnumerator ChangeSpeedOverTime()
    {
        while (true)
        {
            while (navMeshAgent.speed < boostedSpeed)
            {
                navMeshAgent.speed += (boostedSpeed - normalSpeed) / boostDuration * Time.deltaTime;
                yield return null;
            }

            navMeshAgent.speed = boostedSpeed;

            yield return new WaitForSeconds(boostDuration);

            while (navMeshAgent.speed > normalSpeed)
            {
                navMeshAgent.speed -= decreaseRate * Time.deltaTime;
                yield return null;
            }

            navMeshAgent.speed = normalSpeed;

            yield return new WaitForSeconds(decreaseDuration);
        }
    }

    void SetDestinationToNextWaypoint()
    {
        navMeshAgent.SetDestination(waypoints[currentWaypoint].position);

        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }

    void ActivateStar()
    {
        if (starPrefab != null)
        {
            starPrefab.SetActive(true);
        }
    }

    void DeactivateStar()
    {
        if (starPrefab != null)
        {
            starPrefab.SetActive(false);
        }
    }

    void SpawnEnemiesAndStars()
    {
        // Obtener todos los enemigos en la escena
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Si ya hay 2 o más enemigos, no generamos más
        if (enemies.Length >= 2)
        {
            Debug.Log("Ya hay 2 enemigos en la escena.");
            return;
        }

        // Obtenemos la posición actual del jugador
        Vector3 playerPosition = target.position;

        // Instanciamos dos enemigos cerca del jugador
        for (int i = 0; i < 2 - enemies.Length; i++)
        {
            // Generamos una posición aleatoria dentro de un radio especificado alrededor del jugador
            Vector3 spawnPosition = playerPosition + Random.insideUnitSphere * spawnRadius;

            // Aseguramos que la posición generada esté en el suelo
            spawnPosition.y = 0f;

            // Instanciamos el enemigo en la posición generada
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemyList.Add(newEnemy);
        }

        // Si ahora hay 2 enemigos, activamos dos estrellas
        if (enemyList.Count >= 1)
        {
            for (int i = 0; i < 2; i++)
            {
                starPrefab.SetActive(true);
                starPrefab1.SetActive(true);
            }
        }
    }
}
