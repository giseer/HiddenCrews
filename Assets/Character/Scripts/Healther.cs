using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Healther : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;

    SkinnedMeshRenderer meshRenderer;
    Color originalColor;

    protected void Awake()
    {
        currentHealth = maxHealth;
        meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);

        Debug.Log("They damage: " + amount);

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