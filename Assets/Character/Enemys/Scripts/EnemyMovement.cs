using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform target; // El objeto a seguir (usualmente el jugador)

    public float normalSpeed = 3f;
    public float boostedSpeed = 6f;
    public float boostDuration = 2f; // Duraci�n de la velocidad aumentada
    public float decreaseDuration = 2f; // Duraci�n de la disminuci�n de velocidad
    public float minSpeed = 1f; // Velocidad m�nima permitida
    public float decreaseRate = 1f; // Tasa de disminuci�n gradual

    public float moveSpeed = 5f;
    public Transform[] waypoints;
    private int currentWaypoint = 0;

    void Start()
    {
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();

        if (target == null)
        {
            Debug.LogError("El objeto a seguir no est� asignado en el Inspector.");
        }

        navMeshAgent.speed = normalSpeed;

        StartCoroutine(ChangeSpeedOverTime());
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget < 7f)
        {
            navMeshAgent.speed = boostedSpeed;
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
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
}
