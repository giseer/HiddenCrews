using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecollectMission : Mission
{
    private Products product;
    private Transform location;

    [SerializeField] private float missionTime;

    private bool missionFinished;

    [Header("Canvas Settings")]
    [SerializeField] private Transform failedCanvas;
    [SerializeField] private Transform passedCanvas;


    public float radio = 3f;
    public GameObject mensajeTexto;
    public GameObject cubo;
    public Canvas inventoryCanvas;
    public RawImage rawImageInventory;
    private bool dentroDelArea = false;
    private bool cuboDesaparecido = false;

    public float distanciaDelante = 2f;
    public float distanciaPermitida = 2f;
    public float alturaCubo = 1f;
    public float distanciaFlecha = 3f;

    private Vector3 posicionInicialCubo;


    public GameObject flechaPrefab;
    private GameObject flechaInstance;
    public GameObject puntoFinal;

    private Transform jugador;


    private void Awake()
    {
        currentTime = missionTime;
        missionFinished = false;
        rawImageInventory.gameObject.SetActive(false);
    }

    void Start()
    {
        posicionInicialCubo = cubo.transform.position;
        GameObject jugadorGO = GameObject.FindGameObjectWithTag("Player");
        jugador = jugadorGO.transform;
        flechaInstance = Instantiate(flechaPrefab, jugador.position, Quaternion.identity);
    }

    private void Update()
    {
        CheckCubeDisappearance();
        MoverFlechaConJugador();

        AparecerCubo();

        

    }

    protected override void checkFinishConditions()
    {
        CheckMissionTime();
        checkFinishConditions();
    }


    private void AparecerCubo()
    {
        float distanciaAlPuntoFinal = Vector3.Distance(jugador.position, puntoFinal.transform.position);

        if (distanciaAlPuntoFinal < distanciaPermitida && Input.GetKeyDown(KeyCode.F))
        {
            Vector3 posicionDelante = jugador.position + jugador.forward * distanciaDelante;
            Vector3 posicionFinal = new Vector3(posicionDelante.x, jugador.position.y + alturaCubo, posicionDelante.z);
            cubo.transform.position = posicionFinal;
            cubo.SetActive(true);
            OnPassed();
        }
    }
    

    private void CheckMissionTime()
    {
        if (!missionFinished)
        {
            if (currentTime <= 0)
            {
                OnFailed();
                currentTime = 0;
                missionFinished = true;
            }
            else
            {
                currentTime -= Time.deltaTime;
            }
        }
    }

    private void CheckCubeDisappearance()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radio);

        dentroDelArea = false;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                dentroDelArea = true;
                break;
            }
        }

        if (dentroDelArea && !cuboDesaparecido)
        {
            mensajeTexto.SetActive(true);
        }
        else
        {
            mensajeTexto.SetActive(false);
        }

        if (mensajeTexto.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            cubo.SetActive(false);
            cuboDesaparecido = true;
            rawImageInventory.gameObject.SetActive(true);
            flechaInstance.gameObject.SetActive(false);
            puntoFinal.gameObject.SetActive(true);


            
        }
    }

    private void MoverFlechaConJugador()
    {
        flechaInstance.transform.position = jugador.position;

        if (cubo != null && cubo.activeSelf)
        {
            Vector3 directionToCube = (cubo.transform.position - flechaInstance.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToCube);
            flechaInstance.transform.rotation = lookRotation;
        }
    }

    protected override void OnPassed()
    {
        passedCanvas.gameObject.SetActive(true);
        Time.timeScale = 0.5f;
    }

    protected override void OnFailed()
    {
        failedCanvas.gameObject.SetActive(true);
        Time.timeScale = 0.5f;
    }
}