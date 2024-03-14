using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reputation : MonoBehaviour
{
    public Text reputacionText;
    public Slider sliderReputacionSubeBaja;
    public Slider sliderReputacionSube;
    private int reputacion = 0;
    public int cantidadDeSubida = 10;
    public int valorMaximo = 0;
    public KeyCode teclaAumento = KeyCode.Space;
    public KeyCode teclaBajar = KeyCode.DownArrow;

    public static Reputation Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        reputacion = PlayerPrefs.GetInt("reputacion");
        UpdateText();
    }


    void Update()
    {
        if (Input.GetKeyDown(teclaAumento))
        {
            if (reputacion < valorMaximo && sliderReputacionSubeBaja.value == sliderReputacionSube.value)
            {
                reputacion += cantidadDeSubida;
                AumentarReputacion();
            }
            else
            {
                Debug.Log("No puedes aumentar la reputación en este momento.");
            }
        }

        if (Input.GetKeyDown(teclaAumento))
        {
            sliderReputacionSubeBaja.value += cantidadDeSubida;
        }

        if (Input.GetKeyDown(teclaBajar))
        {
            sliderReputacionSubeBaja.value -= cantidadDeSubida;
        }
    }

    void AumentarReputacion()
    {
              
        reputacion = Mathf.Max(0, reputacion);
        UpdateText();
        
        PlayerPrefs.SetInt("reputacion", reputacion);
      
    }

    private void UpdateText()
    {
        reputacionText.text = "Reputación: " + reputacion;
        sliderReputacionSube.value = reputacion;
    }

    [ContextMenu(nameof(BorrarPlayerPrefs))]
    void BorrarPlayerPrefs()
    {
        PlayerPrefs.SetInt("reputacion", 0);
    }

}
