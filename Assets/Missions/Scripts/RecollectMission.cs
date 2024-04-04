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

    // Variables espec�ficas de Prueba
    public float radio = 3f; // Radio del �rea de detecci�n
    public GameObject mensajeTexto; // Referencia al objeto Text en el Canvas
    public GameObject cubo; // Referencia al cubo
    public Canvas inventoryCanvas; // Referencia al canvas de inventario
    public RawImage rawImageInventory; // Referencia a la RawImage en el canvas de inventario
    private bool dentroDelArea = false; // Indicador de si el jugador est� dentro del �rea
    private bool cuboDesaparecido = false; // Indicador de si el cubo ha desaparecido

    // Flecha
    public GameObject flechaPrefab; // Prefab de la flecha
    private GameObject flechaInstance; // Instancia de la flecha

    private Transform jugador;

    private void Awake()
    {
        currentTime = missionTime;
        missionFinished = false;
        // Buscar la RawImage dentro del canvas de inventario
        rawImageInventory = inventoryCanvas.GetComponentInChildren<RawImage>();
        // Desactivar la RawImage al inicio
        rawImageInventory.gameObject.SetActive(false);
    }

    void Start()
    {
        // Buscar el GameObject del jugador por su etiqueta "Player"
        GameObject jugadorGO = GameObject.FindGameObjectWithTag("Player");
        if (jugadorGO != null)
        {
            jugador = jugadorGO.transform;
            Debug.Log("Jugador encontrado: " + jugador.name);
        }
        else
        {
            Debug.LogError("No se encontr� al jugador. Aseg�rate de que haya un GameObject con la etiqueta 'Player'.");
        }

        // Inicializar la flecha
        if (flechaPrefab != null)
        {
            // Crear una instancia de la flecha en la posici�n inicial del jugador
            flechaInstance = Instantiate(flechaPrefab, jugador.position, Quaternion.identity);
            Debug.Log("Flecha inicializada");
        }
        else
        {
            Debug.LogError("Falta asignar el prefab de la flecha en el Inspector de Unity.");
        }
    }

    private void Update()
    {
        checkFinishConditions();
        CheckCubeDisappearance();
        MoverFlechaConJugador();
    }

    protected override void checkFinishConditions()
    {
        CheckEnemyCount();
        CheckMissionTime();
    }

    private void CheckEnemyCount()
    {
        // Aqu� puedes implementar la l�gica para verificar el recuento de enemigos si es necesario
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
        // Se obtienen todos los colliders dentro del �rea esf�rica
        Collider[] colliders = Physics.OverlapSphere(transform.position, radio);

        // Se verifica si el jugador est� dentro del �rea
        dentroDelArea = false;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                dentroDelArea = true;
                break;
            }
        }

        // Actualizar el estado del Canvas
        if (dentroDelArea && !cuboDesaparecido)
        {
            mensajeTexto.SetActive(true); // Activar el Canvas cuando el jugador est� dentro del �rea y el cubo no ha desaparecido
        }
        else
        {
            mensajeTexto.SetActive(false); // Desactivar el Canvas permanentemente cuando el cubo ha desaparecido
        }

        // Desactivar el cubo al presionar la tecla R
        if (mensajeTexto.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            cubo.SetActive(false);
            cuboDesaparecido = true; // Establecer el indicador de que el cubo ha desaparecido
            // Activar la RawImage de inventario
            rawImageInventory.gameObject.SetActive(true);
        }
    }

    private void MoverFlechaConJugador()
    {
        // Actualizar la posici�n de la flecha para que coincida con la posici�n del jugador
        flechaInstance.transform.position = jugador.position;

        // Calcular la direcci�n desde la flecha hacia el cubo
        if (cubo != null)
        {
            Vector3 directionToCube = (cubo.transform.position - flechaInstance.transform.position).normalized;
            // Calcular la rotaci�n para mirar hacia la direcci�n del cubo
            Quaternion lookRotation = Quaternion.LookRotation(directionToCube);
            // Aplicar la rotaci�n a la flecha
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