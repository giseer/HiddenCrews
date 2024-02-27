using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    public float moveSpeed = 5f;
    public Transform[] waypoints;
    private int currentWaypoint = 0;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.speed = moveSpeed;

        SetDestinationToNextWaypoint();
    }

    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            SetDestinationToNextWaypoint();
        }
    }

    void SetDestinationToNextWaypoint()
    {
        navMeshAgent.SetDestination(waypoints[currentWaypoint].position);

        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }

}
