using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prueba : MonoBehaviour
{
    public Transform jugador; // Referencia al transform del jugador
    public GameObject cubo; // Referencia al objeto cubo
    public GameObject flecha; // Referencia al objeto flecha
    public GameObject objetivo; // Referencia al GameObject objetivo
    public KeyCode teclaDesaparecer = KeyCode.R; // Tecla para desaparecer el cubo
    public KeyCode teclaAparecer = KeyCode.D; // Tecla para aparecer el cubo
    public KeyCode teclaDejar = KeyCode.F; // Tecla para dejar el cubo
    public float distanciaDelante = 2f; // Distancia delante del jugador donde aparecerá el cubo
    public float alturaCubo = 1f; // Altura del cubo por encima del jugador
    public float distanciaFlecha = 3f; // Distancia a la que aparecerá la flecha delante del jugador

    private Vector3 posicionInicialCubo; // Posición inicial del cubo

    void Start()
    {
        // Guardamos la posición inicial del cubo
        posicionInicialCubo = cubo.transform.position;
    }

    void Update()
    {
        // Si se presiona la tecla para desaparecer el cubo
        if (Input.GetKeyDown(teclaDesaparecer))
        {
            // Desactivamos el cubo (lo hacemos invisible)
            cubo.SetActive(false);
            // Activamos la flecha
            flecha.SetActive(true);
            // Posicionamos la flecha en la posición del objetivo
            flecha.transform.position = objetivo.transform.position;
        }

        // Si se presiona la tecla para aparecer el cubo
        if (Input.GetKeyDown(teclaAparecer))
        {
            // Calculamos la posición delante y encima del jugador
            Vector3 posicionDelante = jugador.position + jugador.forward * distanciaDelante;
            Vector3 posicionFinal = new Vector3(posicionDelante.x, jugador.position.y + alturaCubo, posicionDelante.z);
            // Asignamos la posición del cubo
            cubo.transform.position = posicionFinal;
            // Activamos el cubo (lo hacemos visible)
            cubo.SetActive(true);
            // Desactivamos la flecha
            flecha.SetActive(false);
        }

        // Si se presiona la tecla para dejar el cubo
        if (Input.GetKeyDown(teclaDejar))
        {
            // Movemos el cubo a la posición del objetivo
            cubo.transform.position = objetivo.transform.position;
            // Desactivamos el cubo (lo hacemos invisible)
            cubo.SetActive(false);
            // Desactivamos la flecha
            flecha.SetActive(false);
        }
    }
}
