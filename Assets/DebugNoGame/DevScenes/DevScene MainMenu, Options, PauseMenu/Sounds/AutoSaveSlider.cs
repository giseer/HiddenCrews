using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class AutoSaveSlider : MonoBehaviour
{
    [SerializeField] string ParameterName;
    [SerializeField] float defaultValue = 0.5f;
    Slider slider;
    // Start is called before the first frame update
    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        slider.onValueChanged.AddListener(onValueChanged);
        slider.value = PlayerPrefs.GetFloat(ParameterName, defaultValue);
    }

    private void Start()
    {
        InternalValueChanged(slider.value);
    }

    // Update is called once per frame
    void onValueChanged(float newValue)
    {
        PlayerPrefs.SetFloat(ParameterName, newValue);
        PlayerPrefs.Save();
        InternalValueChanged(newValue);
    }
    protected virtual void InternalValueChanged(float newValue) 
    {

    }
}
