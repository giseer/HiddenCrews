using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Transform jugador; // Referencia al personaje que la flecha debe seguir
    public GameObject objetivo; // Referencia al objetivo al que la flecha debe apuntar
    private Vector3 posicionInicial; // Posición inicial de la flecha
    private GameObject flechaInstance;
    public GameObject flechaPrefab;

    void Start()
    {
        GameObject jugadorGO = GameObject.FindGameObjectWithTag("Player");
        jugador = jugadorGO.transform;
        flechaInstance = Instantiate(flechaPrefab, jugador.position, Quaternion.identity);
    }

    void Update()
    {
        MoverFlechaConJugador();
    }

    private void MoverFlechaConJugador()
    {
        flechaInstance.transform.position = jugador.position;

        if (objetivo != null && objetivo.activeSelf)
        {
            Vector3 directionToCube = (objetivo.transform.position - flechaInstance.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToCube);
            flechaInstance.transform.rotation = lookRotation;
        }
    }
}
