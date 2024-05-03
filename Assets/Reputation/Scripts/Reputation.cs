using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reputation : MonoBehaviour
{
    //public Text reputacionText;
    public Slider sliderReputacionSubeBaja;
    public Slider sliderReputacionSube;
    private int reputacion = 0;
    public int cantidadDeSubida = 10;
    public int valorMaximo = 0;

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

    public void AumentarReputacion()
    {
        Debug.Log("SUBIENDO REPUTACION!");

        reputacion = Mathf.Max(0, reputacion);
        reputacion += cantidadDeSubida;
        sliderReputacionSube.value = reputacion; // Actualiza el valor del Slider
        PlayerPrefs.SetInt("reputacion", reputacion);

    }

    private void UpdateText()
    {
        //reputacionText.text = "Reputacion: " + reputacion;
        sliderReputacionSube.value = reputacion;
    }

    [ContextMenu(nameof(BorrarPlayerPrefs))]
    void BorrarPlayerPrefs()
    {
        PlayerPrefs.SetInt("reputacion", 0);
    }

}
