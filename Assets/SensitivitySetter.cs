using System;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SensitivitySetter : MonoBehaviour
{
    [Header("X Sensitivity Settings")]
    [SerializeField] private Slider NormalSensSlider;
    [SerializeField] private float minNormalSens;
    [SerializeField] private float maxNormalSens;
    
    [Header("Y Sensitivity Settings")]
    [SerializeField] private Slider AimSensSlider;
    
    [SerializeField] private float minAimSens;
    [SerializeField] private float maxAimSens;

    [Header("Player Settings")]
    [SerializeField] private PlayerAimer aimer;

    private void Awake()
    {
        AddSliderListeners();
        SetSliderLimits();
        LoadPlayerPrefsValues();
    }
    
    private void AddSliderListeners()
    {
        NormalSensSlider.onValueChanged.AddListener(ChangeNormalSensPlayerPrefs);
        AimSensSlider.onValueChanged.AddListener(ChangeAimSensPlayerPrefs);
    }

    private void SetSliderLimits()
    {
        NormalSensSlider.value = aimer.normalSensitivity;
        AimSensSlider.value = aimer.aimSensitivity; 
        
        NormalSensSlider.minValue = minNormalSens;
        NormalSensSlider.maxValue = maxNormalSens;
        
        AimSensSlider.minValue = minAimSens;
        AimSensSlider.maxValue = maxAimSens;
    }
    
    private void LoadPlayerPrefsValues()
    {
        NormalSensSlider.value = PlayerPrefs.GetFloat("normalSensitivity");
        AimSensSlider.value = PlayerPrefs.GetFloat("aimSensitivity");
        UpdateSens();
    }

    private void ChangeNormalSensPlayerPrefs(float newValue)
    {   
        PlayerPrefs.SetFloat("normalSensitivity", newValue);
        UpdateSens();
    }

    private void ChangeAimSensPlayerPrefs(float newValue)
    {
        PlayerPrefs.SetFloat("aimSensitivity", newValue);
        UpdateSens();
    }
    
    private void UpdateSens()
    {
        if (aimer != null)
        {
               aimer.normalSensitivity = PlayerPrefs.GetFloat("normalSensitivity");
               aimer.aimSensitivity = PlayerPrefs.GetFloat("aimSensitivity");
        }
    }
}
