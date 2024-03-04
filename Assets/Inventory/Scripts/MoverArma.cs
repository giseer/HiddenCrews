using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class MoverArma : MonoBehaviour
{
    public GameObject armaTienda;
    public Canvas canvasTienda;
    public Canvas canvasInventory;
    public GameObject armaInventario;
    public void OnPointerClick(PointerEventData eventData)
    {
        // Obtener la posición del click en el canvas "Tienda".
        Vector2 posicionClick = eventData.position;

        Camera camera = Camera.main;


        // Convertir la posición del click a coordenadas del espacio local del canvas "Tienda".
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasTienda.transform as RectTransform,
            posicionClick, camera, out Vector2 posicionLocal
        );

        // Si la posición del click está dentro del área del arma, moverla al canvas "Inventario".
        if (RectTransformUtility.RectangleContainsScreenPoint(armaTienda.transform as RectTransform, posicionClick))
        {
            RectTransform armaRectTransform = armaInventario.transform as RectTransform;
            armaRectTransform.anchoredPosition = posicionLocal;

            // Cambiar la visibilidad del arma en los canvas.
            armaTienda.SetActive(false);
            armaInventario.SetActive(true);
            // Desactivar el arma en el canvas "Tienda".
            armaTienda.SetActive(false);

            // Activar el arma en el canvas "Inventario".
            armaInventario.SetActive(true);
        }
    }
}
