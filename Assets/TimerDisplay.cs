using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        float remainingTime = MurderMission.currentTime;
        tmp.text = $"Time: {Mathf.FloorToInt(remainingTime)}";
    }
}
