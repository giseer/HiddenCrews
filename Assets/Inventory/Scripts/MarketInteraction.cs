using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketInteraction : MonoBehaviour
{
    public GameObject marketCanvas;
    public List<Canvas> listCanvas;
    public List<Button> WeaponMarket1;
    public RawImage otherCanvas;
    public GameObject player;

    private Canvas canvasActive;

    void Start()
    {
        foreach (var canvas in listCanvas)
        {
            Button button = canvas.GetComponentInChildren<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnClick(canvas));
            }
        }

        if (listCanvas.Count > 0)
        {
            canvasActive = listCanvas[0];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleMarketVisibility();
        }
    }

    public void OnClick(Canvas newCanvas)
    {
        DisableAllCanvas();

        ActivateCanvasMessage(newCanvas);

        canvasActive = newCanvas;
    }

    public void OnClickButton(Button armaMarket2)
    {
        if (WeaponMarket1.Contains(armaMarket2))
        {
            armaMarket2.GetComponent<Button>().enabled = false;

            armaMarket2.onClick = new Button.ButtonClickedEvent();

            armaMarket2.transform.SetParent(otherCanvas.transform, false);
            armaMarket2.gameObject.SetActive(true);

            int index = WeaponMarket1.IndexOf(armaMarket2);

            if (index >= 0 && index < listCanvas.Count)
            {
                Canvas canvasMensaje = listCanvas[index];

                ActivateCanvasMessage(canvasMensaje);
            }
        }
    }

    void DisableAllCanvas()
    {
        foreach (var canvas in listCanvas)
        {
            DisableCanvasMessage(canvas);
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
            DisableCanvasMessage(canvasActive);
            DisableAllCanvas();
        }
    }

    public void ActivateCanvasMessage(Canvas canvas)
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(true);
        }
    }

    public void DisableCanvasMessage(Canvas canvas)
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
