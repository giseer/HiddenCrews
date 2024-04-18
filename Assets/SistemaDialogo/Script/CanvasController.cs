using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject canvasInstructions;
    public float activationDuration = 4f;

    public void ActivateCanvasForDuration()
    {
        canvasInstructions.SetActive(true);

        Invoke("DeactivateCanvas", activationDuration);
    }

    void DeactivateCanvas()
    {
        canvasInstructions.SetActive(false);
    }

    

}
