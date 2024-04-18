using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Product : MonoBehaviour
{
    // Evento que se activa cuando el producto se desactiva
    public UnityAction<GameObject> OnProductDeactivated;

    private void OnDisable()
    {
        // Llama al evento de desactivaci�n del producto y pasa este GameObject como argumento
        OnProductDeactivated?.Invoke(gameObject);
    }
}
