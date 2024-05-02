using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyType
    {
        Police,
        BandMember
    }

    public EnemyType enemyType;

    private NavMeshAgent navMeshAgent;

    [Header("Components")]
    public PlayerInputHandler playerInputHandler;
    public Transform target; 
    public GameObject enemyPrefab; 
    public StarManager starManager;

    [Header("Detection Settings")]
    public float detectionRange = 7f; // Rango de detección del enemigo con el jugador
    public float spawnRadius = 5f; // Aparición de los enemigos 

    [Header("Speed Settings")]
    public float normalSpeed = 3f; // Velocidad normal del enemigo
    public float boostedSpeed = 6f; // Velocidad aumentada del enemigo
    public float boostDuration = 2f; // Duración de la velocidad aumentada
    public float decreaseDuration = 2f; // Duración de la disminución de velocidad
    public float contactDistance = 1f; // Distancia entre el enemigo y el jugador
    public float minSpeed = 1f; // Velocidad mínima permitida
    public float decreaseRate = 1f; // Disminución gradual

    private float previousSpeed;
    public Transform[] waypoints;
    private int currentWaypoint = 0;

    [Header("UI Elements")]
    public GameObject canvasSoborno;
    public GameObject canvasArrow;
    public GameObject canvasTextPolice;
    public GameObject controllerDoor;
    public CanvasController canvasController;

    private bool isFollowingPlayer = false;

    public TextMeshProUGUI dineroText;

    [Header("Animations")]
    public Animator animator;

    [Header("Enemy List")]
    public List<GameObject> enemyList = new List<GameObject>();


    void Start()
    {
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();

        navMeshAgent.speed = normalSpeed;

        StartCoroutine(ChangeSpeedOverTime());
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if(navMeshAgent != null && navMeshAgent.enabled)
        {
            if (distanceToTarget < detectionRange)
            {
                if (!isFollowingPlayer && enemyType == EnemyType.Police)
                {
                    animator.SetBool("IsRunning", true);
                    isFollowingPlayer = true;
                    ActivateStar();
                }

                navMeshAgent.speed = boostedSpeed;
                navMeshAgent.SetDestination(target.position);

                if (distanceToTarget < contactDistance && enemyType == EnemyType.Police)
                {
                    canvasSoborno.SetActive(true);

                    StopEnemy();

                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }


            }
            else
            {
                if (isFollowingPlayer && enemyType == EnemyType.Police)
                {
                    isFollowingPlayer = false;
                    DeactivateStar();
                }

                navMeshAgent.speed = normalSpeed;
                if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
                {
                    SetDestinationToNextWaypoint();
                }

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
        playerInputHandler.enabled = false;
        animator.SetBool("IsRunning", false);
        target.GetComponentInChildren<PlayerAnimationsHandler>().StopAnimateMovement();
    }

    public void OnClick()
    {
        canvasSoborno.gameObject.SetActive(false);
        canvasArrow.gameObject.SetActive(true);
        starManager.starPrefab.SetActive(false);
        starManager.starPrefab1.SetActive(false);
        starManager.starPrefab2.SetActive(false);
        canvasTextPolice.gameObject.SetActive(false);
        controllerDoor.gameObject.SetActive(true);
        playerInputHandler.enabled = true;
        detectionRange = 0f;
        dineroText.text = "$0";
        isFollowingPlayer = false;
        navMeshAgent.isStopped = false;
        canvasController.enabled = true;
        SetDestinationToNextWaypoint();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
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

        if (enemyType == EnemyType.Police)
        {
            animator.SetBool("IsRunning", true);
        }
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
