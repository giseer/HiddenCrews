using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInteraction : MonoBehaviour
{
    public GameObject inventoryCanvas;

    public GameObject player;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //player.SetActive(inventoryCanvas.gameObject.activeSelf);

            inventoryCanvas.gameObject.SetActive(!inventoryCanvas.gameObject.activeSelf);

            

        }
    }
    

    
}
