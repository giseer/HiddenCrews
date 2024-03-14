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
    public void TakeDamage(int amount = 10)
    {
        healthPoints -= amount;
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        Color originalColor = renderer.material.color;
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        renderer.material.color = originalColor;
    }
}
