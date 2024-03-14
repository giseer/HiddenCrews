using System.Collections;
using UnityEngine;

public class EnemyHealther : MonoBehaviour
{
    [SerializeField] private int maxHealthPoints;
    private int healthPoints;
    [SerializeField] private MeshRenderer renderer;

    private void Start()
    {
        healthPoints = maxHealthPoints;
    }

    [ContextMenu(nameof(TakeDamage))]
    public void TakeDamage()
    {
        healthPoints -= 10;
        if (healthPoints <= 0)
        {
            Die();
        }
        
        StartCoroutine(Blink());
    }

    private void Die()
    {
        //Animacion de muerte
        Destroy(gameObject);
    }

    IEnumerator Blink()
    {
        Color originalColor = renderer.material.color;
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        renderer.material.color = originalColor;
    }
}
