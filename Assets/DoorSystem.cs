using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSystem : MonoBehaviour
{

    public string sceneName;
    public GameObject canvasAlmacen;
    public GameObject canvasDoor;

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            canvasDoor.SetActive(true);
            canvasAlmacen.SetActive(false);
            
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasDoor.SetActive(false);
        }
    }


    private void Update()
    {
        if (canvasDoor.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
