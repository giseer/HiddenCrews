using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketInteraction : MonoBehaviour
{
    public GameObject marketCanvas;
    public List<Button> armaMarket1;
    public RawImage otroCanvas;
    public GameObject player;

    public List<Canvas> listaDeCanvasMensaje;

    private Button botonActivo;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleMarketVisibility();

        }
    }

    public void OnClick(Button armaMarket2)
    {
        if (armaMarket1.Contains(armaMarket2))
        {
            armaMarket2.gameObject.SetActive(false);

            armaMarket2.transform.SetParent(otroCanvas.transform, false);
            armaMarket2.gameObject.SetActive(true);

            int index = armaMarket1.IndexOf(armaMarket2);

            if (index >= 0 && index < listaDeCanvasMensaje.Count)
            {
                Canvas canvasMensaje = listaDeCanvasMensaje[index];

                

                ActivarCanvasMensaje(canvasMensaje);
            }
        }
    }

    void ToggleMarketVisibility()
    {
        player.SetActive(marketCanvas.gameObject.activeSelf);


        marketCanvas.gameObject.SetActive(!marketCanvas.gameObject.activeSelf);

        Cursor.visible = marketCanvas.gameObject.activeSelf;

        Cursor.lockState = marketCanvas.gameObject.activeSelf ? CursorLockMode.Confined : CursorLockMode.None;

        if (!marketCanvas.gameObject.activeSelf)
        {
            DesactivarTodosLosCanvasMensaje();
        }


    }

    public void ActivarCanvasMensaje(Canvas canvas)
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(true);
        }
    }

    public void DesactivarCanvasMensaje(Canvas canvas)
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    void DesactivarTodosLosCanvasMensaje()
    {
        foreach (Canvas canvasMensaje in listaDeCanvasMensaje)
        {
            DesactivarCanvasMensaje(canvasMensaje);
        }
    }
}
