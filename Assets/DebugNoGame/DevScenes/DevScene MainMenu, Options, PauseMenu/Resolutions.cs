using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Resolutions : MonoBehaviour
{
    [SerializeField] public UnityEvent onOptionsMenuClosed;
    [SerializeField] TMP_Dropdown qualityDropdown;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    void Start()
    {
        int currentResolution = -1;

        List<TMP_Dropdown.OptionData> options = new(); // o as� es lo mismo: List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < QualitySettings.count; i++)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = QualitySettings.names[i];
            options.Add(option);
        }

        List<TMP_Dropdown.OptionData> resolutionOptions = new();
        for (int resolutionIndex = 0; resolutionIndex < Screen.resolutions.Length; resolutionIndex++)
        {
            TMP_Dropdown.OptionData resolutionOption = new();
            resolutionOption.text = $"{Screen.resolutions[resolutionIndex].width}x" +
                $"{Screen.resolutions[resolutionIndex].height}-" +
                $"{Mathf.RoundToInt((float)Screen.resolutions[resolutionIndex].refreshRateRatio.value)}Hz";

            resolutionOptions.Add(resolutionOption);
            if ((Screen.currentResolution.width == Screen.resolutions[resolutionIndex].width) &&
                Screen.currentResolution.height == Screen.resolutions[resolutionIndex].height &&
                Screen.currentResolution.refreshRateRatio.value == Screen.resolutions[resolutionIndex].refreshRateRatio.value)
            {
                currentResolution = resolutionIndex;
            }
        }

        qualityDropdown.options = options;
        resolutionDropdown.options = resolutionOptions;


        qualityDropdown.onValueChanged.AddListener(OnQualityDropDownValueChanged);
        resolutionDropdown.onValueChanged.AddListener(OnResolutionDropDownValueChanged);

        canvas.enabled = true;
    }

    private void OnResolutionDropDownValueChanged(int value)
    {
        Screen.SetResolution(
            Screen.resolutions[value].width,
            Screen.resolutions[value].height,
            FullScreenMode.Windowed,
            Screen.resolutions[value].refreshRateRatio
            );
    }

    private void OnQualityDropDownValueChanged(int value)
    {
        QualitySettings.SetQualityLevel(value);
    }
}