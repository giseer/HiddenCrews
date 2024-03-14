using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class AutoSave_CheckBox : MonoBehaviour
{
    [SerializeField] string ParameterName;
    [SerializeField] bool defaultValue = false;
    Toggle toggle;
    // Start is called before the first frame update
    private void Awake()
    {
        toggle = GetComponentInChildren<Toggle>();
        toggle.onValueChanged.AddListener(onValueChanged);
        int defaultValeuInt = 0;
        if(defaultValue == true) { defaultValeuInt = 1; }
        if(PlayerPrefs.GetInt(ParameterName, defaultValeuInt) == 1)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        //toggle.isOn = PlayerPrefs.GetInt(ParameterName,defaultValeuInt ? 1 : 0 == 1);
    }

    private void Start()
    {
        InternalValueChanged(toggle.isOn);
    }

    // Update is called once per frame
    void onValueChanged(bool newValue)
    {
        int newValueInt = 0;
        if(newValue == true) {  newValueInt = 1; }
        PlayerPrefs.SetInt(ParameterName,newValueInt);
        PlayerPrefs.Save();
        InternalValueChanged(newValue);
    }
    protected virtual void InternalValueChanged(bool newValue)
    {

    }
}