using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    [SerializeField] private Mission mision;

    private void Awake()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        float remainingTime = mision.currentTime;
        tmp.text = $"Time: {Mathf.FloorToInt(remainingTime)}";
    }
}
