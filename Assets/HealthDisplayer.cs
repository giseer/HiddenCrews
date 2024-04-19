using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplayer : MonoBehaviour
{
    private TextMeshProUGUI healthText;

    private void Awake()
    {
        healthText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void updateHealthHUD(int healthPoints)
    {
        healthText.text = $"{healthPoints}";
    }
}
