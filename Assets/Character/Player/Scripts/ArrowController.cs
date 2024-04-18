using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public Transform target; // Objetivo al que la flecha debe apuntar
    public Vector3 offset; // Desplazamiento de la flecha respecto al jugador

    void Update()
    {
        // Si el jugador y el objetivo están definidos
        if (player != null && target != null)
        {
            // Calculamos la dirección hacia el objetivo desde la posición del jugador
            Vector3 directionToTarget = (target.position - player.position).normalized;

            // Calculamos la rotación hacia el objetivo
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Aplicamos la rotación a la flecha, pero solo en el eje X
            transform.rotation = Quaternion.Euler(90f, targetRotation.eulerAngles.y, 0f);

            // Calculamos la posición adelante del jugador con un desplazamiento
            Vector3 posicionAdelante = player.position + player.forward * offset.x + player.up * offset.y + player.right * offset.z;

            // Posicionamos la flecha adelante del jugador con el desplazamiento
            transform.position = posicionAdelante;
        }
    }
}
