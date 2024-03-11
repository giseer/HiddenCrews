using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reputation : MonoBehaviour
{
    public Text reputacionText;
    public Slider sliderReputacion;
    private int reputacion = 0;
    public int cantidadDeSubida = 10;
    public int valorMaximo = 0;
    public KeyCode teclaAumento = KeyCode.Space; 

    void Start()
    {
        AumentarReputacionOnClick();
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaAumento))
        {
            reputacion += cantidadDeSubida;
            AumentarReputacionOnClick();
        }
    }

    void AumentarReputacionOnClick()
    {
        if(reputacion <= valorMaximo)
        {
            reputacion = Mathf.Max(0, reputacion);

            reputacionText.text = "Reputación: " + reputacion;
            sliderReputacion.value = reputacion;
        }
        

        if(reputacion >= valorMaximo)
        {
            Debug.Log("Tienes el maximo de reputacion");
        }
    }

  

}
