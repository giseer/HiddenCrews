using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealther : Healther
{
    [SerializeField] HealthDisplayer healthDisplayer;

    [SerializeField] GameObject dieCanvas;

    [SerializeField] string sceneToLoad;

    private void UpdateHealthHUD(int healthPoints)
    {
        healthDisplayer.updateHealthHUD(healthPoints);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        UpdateHealthHUD(currentHealth);
    }

    protected override void Die()
    {
        //Die animation - Temporalmente se usa destroy directamente pero lo que se tendria que hacer es activar
        //                  el trigger de la animacion de muerte y esta cuando acabara salir del juego al menu principal o algun otro

        Time.timeScale = 0.5f;
        dieCanvas.SetActive(true);

        Invoke(nameof(GoToMainMenu), 5f);
    }

    private void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneToLoad);
    }
}