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

            // Cambiar el padre del botón al nuevo canvas
            armaMarket2.transform.SetParent(otroCanvas.transform, false);

            // Activar el botón en el otro canvas
            armaMarket2.gameObject.SetActive(true);
        }
    }

    void ToggleMarketVisibility()
    {
        // Se activa o desactiva el canvas del mercado
        marketCanvas.gameObject.SetActive(!marketCanvas.gameObject.activeSelf);
    }
}
