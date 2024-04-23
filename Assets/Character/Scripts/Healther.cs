using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Healther : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int currentHealth;

    protected UnityEvent<int> OnHealthChanged = new();

    SkinnedMeshRenderer meshRenderer;
    Color originalColor;

    protected void Awake()
    {
        currentHealth = maxHealth;
        meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);

        Debug.Log("Me estan haciendo da�oo");

        StartCoroutine(Blood());

        if (currentHealth == 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }

    private IEnumerator Blood()
    {
        meshRenderer.material.color = Color.red;

        yield return new WaitForSeconds(1f);

        meshRenderer.material.color = originalColor;
    }
}