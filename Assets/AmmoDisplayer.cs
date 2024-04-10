using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoDisplayer : MonoBehaviour
{
    private TextMeshProUGUI ammoText;

    private void Awake()
    {
        ammoText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void updateAmmoHUD(int currentAmmo, int clipSize)
    {
        ammoText.text = $"{currentAmmo}/{clipSize}";
    }
}
