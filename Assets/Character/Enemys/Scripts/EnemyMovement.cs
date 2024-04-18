using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private PlayerMover playerMover;
    public Transform target; // El objeto a seguir (usualmente el jugador)
    public GameObject enemyPrefab; // Prefab del enemigo
    public StarManager starManager;
    public float detectionRange = 7f;
    public float spawnRadius = 5f;

    public float normalSpeed = 3f;
    public float boostedSpeed = 6f;
    public float boostDuration = 2f; // Duraci�n de la velocidad aumentada
    public float decreaseDuration = 2f; // Duraci�n de la disminuci�n de velocidad
    public float contactDistance = 1f;
    public float minSpeed = 1f; // Velocidad m�nima permitida
    public float decreaseRate = 1f; // Tasa de disminuci�n gradual

    private float previousSpeed;
    public Transform[] waypoints;
    private int currentWaypoint = 0;

    public GameObject canvasSoborno;
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
            starManager.SpawnTwoEnemiesAndStars();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            starManager.SpawnThreeEnemiesAndStars();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            starManager.SpawnFourEnemiesAndStars();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            starManager.SpawnFiveEnemiesAndStars();
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

            if (distanceToTarget < contactDistance)
            {
                canvasSoborno.SetActive(true);
                StopEnemy();
                
                detectionRange = 0f;
                Cursor.visible = true;
                Cursor.lockState = canvasSoborno.gameObject.activeSelf ? CursorLockMode.Confined : CursorLockMode.None;


            }


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

    public void Stop()
    {
        previousSpeed = normalSpeed;
        normalSpeed = 0f;
        boostedSpeed = 0f;
    }

    public void StopEnemy()
    {
        navMeshAgent.isStopped = true;
    }

    public void OnClick()
    {
        // Desactivar el canvas de soborno
        canvasSoborno.gameObject.SetActive(false);

        // Detener el seguimiento del jugador
        isFollowingPlayer = false;

        // Reactivar el movimiento del enemigo y establecer el destino al siguiente punto del camino predeterminado
        navMeshAgent.isStopped = false;
        SetDestinationToNextWaypoint();

        // Hacer el cursor visible
        Cursor.visible = true;
        // Restaurar el estado del cursor dependiendo de si el canvas de soborno está activo o no
        Cursor.lockState = canvasSoborno.gameObject.activeSelf ? CursorLockMode.Confined : CursorLockMode.None;
    }

    public void Move()
    {
        normalSpeed = previousSpeed;
        //boostedSpeed = 3f;
    }

    void SetDestinationToNextWaypoint()
    {
        navMeshAgent.SetDestination(waypoints[currentWaypoint].position);

        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }

    void ActivateStar()
    {
        starManager.ActivateStars();
    }

    void DeactivateStar()
    {
        starManager.DeactivateStars();
    }
}
