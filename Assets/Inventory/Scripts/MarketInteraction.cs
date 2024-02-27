using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketInteraction : MonoBehaviour
{
    public GameObject marketCanvas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            marketCanvas.gameObject.SetActive(!marketCanvas.gameObject.activeSelf);
        }
    }
}
