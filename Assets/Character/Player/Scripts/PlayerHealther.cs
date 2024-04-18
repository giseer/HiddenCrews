using System;
using UnityEngine;

public class PlayerHealther : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        Mathf.Max(currentHealth - amount, 0);
        
        if(currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Die animation - Temporalmente se usa destroy directamente pero lo que se tendria que hacer es activar
        //                  el trigger de la animacion de muerte y esta cuando acabara salir del juego al menu principal o algun otro
        
        Destroy(transform.parent.gameObject);
    }
}
