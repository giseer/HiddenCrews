using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySetter : MonoBehaviour
{
    [Header("X Sensitivity Settings")]
    [SerializeField] private Slider XSensSlider;
    [SerializeField] private float minXSens;
    [SerializeField] private float maxXSens;

    [Header("Y Sensitivity Settings")]
    [SerializeField] private Slider YSensSlider;
    [SerializeField] private float minYSens;
    [SerializeField] private float maxYSens;

    [Header("Camera Settings")]
    [SerializeField] private CinemachineFreeLook camera;

    private void Awake()
    {
        LoadPlayerPrefsValues();
    }

    private void OnEnable()
    {
        XSensSlider.onValueChanged.AddListener(ChangeXPlayerPrefs);
        YSensSlider.onValueChanged.AddListener(ChangeYPlayerPrefs);
    }

    private void LoadPlayerPrefsValues()
    {
        XSensSlider.value = PlayerPrefs.GetFloat("XSens");
        YSensSlider.value = PlayerPrefs.GetFloat("YSens");
    }

    private void ChangeXPlayerPrefs(float newValue)
    {
        PlayerPrefs.SetFloat("XSens", newValue);
        camera.m_XAxis.m_MaxSpeed = newValue;
    }

    private void ChangeYPlayerPrefs(float newValue)
    {
        PlayerPrefs.SetFloat("YSens", newValue);
        camera.m_YAxis.m_MaxSpeed = newValue;
    }
}
