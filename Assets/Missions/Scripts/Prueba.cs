using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prueba : MonoBehaviour
{
    public float radio = 3f; // Radio del �rea de detecci�n
    public GameObject mensajeTexto; // Referencia al objeto Text en el Canvas
    public GameObject cubo; // Referencia al cubo
    public Canvas inventoryCanvas; // Referencia al canvas de inventario
    public RawImage rawImageInventory; // Referencia a la RawImage en el canvas de inventario
    private bool dentroDelArea = false; // Indicador de si el jugador est� dentro del �rea
    private bool cuboDesaparecido = false; // Indicador de si el cubo ha desaparecido

    void Start()
    {
        // Buscar la RawImage dentro del canvas de inventario
        rawImageInventory = inventoryCanvas.GetComponentInChildren<RawImage>();
        // Desactivar la RawImage al inicio
        rawImageInventory.gameObject.SetActive(false);
    }

    void Update()
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
}
