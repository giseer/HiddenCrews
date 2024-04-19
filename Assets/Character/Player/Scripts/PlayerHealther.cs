using System;
using UnityEngine;

public class PlayerHealther : Healther
{
    [SerializeField] HealthDisplayer healthDisplayer;

    [SerializeField] GameObject dieCanvas;

    private void UpdateHealthHUD(int healthPoints)
    {
        healthDisplayer.updateHealthHUD(healthPoints);
    }

    protected override void Die()
    {
        //Die animation - Temporalmente se usa destroy directamente pero lo que se tendria que hacer es activar
        //                  el trigger de la animacion de muerte y esta cuando acabara salir del juego al menu principal o algun otro

        Time.timeScale = 0.5f;
        dieCanvas.SetActive(true);
    }
}