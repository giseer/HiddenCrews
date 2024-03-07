using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketInteraction : MonoBehaviour
{
    public GameObject marketCanvas;
    public List<Canvas> listaDeCanvas;
    public List<Button> armaMarket1;
    public RawImage otroCanvas;
    public GameObject player;

    private Canvas canvasActivo;

    void Start()
    {
        foreach (var canvas in listaDeCanvas)
        {
            Button button = canvas.GetComponentInChildren<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnClick(canvas));
            }
        }

        if (listaDeCanvas.Count > 0)
        {
            canvasActivo = listaDeCanvas[0];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleMarketVisibility();
        }
    }

    public void OnClick(Canvas nuevoCanvas)
    {
        DesactivarTodosLosCanvas();

        ActivarCanvasMensaje(nuevoCanvas);

        canvasActivo = nuevoCanvas;
    }

    public void Mensaje()
    {
        Debug.Log("HOLA"); 
    }

    public void OnClickButton(Button armaMarket2)
    {
        if (armaMarket1.Contains(armaMarket2))
        {
            armaMarket2.GetComponent<Button>().enabled = false;

            armaMarket2.onClick = new Button.ButtonClickedEvent();

            armaMarket2.transform.SetParent(otroCanvas.transform, false);
            armaMarket2.gameObject.SetActive(true);

            int index = armaMarket1.IndexOf(armaMarket2);

            if (index >= 0 && index < listaDeCanvas.Count)
            {
                Canvas canvasMensaje = listaDeCanvas[index];

                ActivarCanvasMensaje(canvasMensaje);
            }
        }
    }

    void DesactivarTodosLosCanvas()
    {
        foreach (var canvas in listaDeCanvas)
        {
            DesactivarCanvasMensaje(canvas);
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
            DesactivarCanvasMensaje(canvasActivo);
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
}
