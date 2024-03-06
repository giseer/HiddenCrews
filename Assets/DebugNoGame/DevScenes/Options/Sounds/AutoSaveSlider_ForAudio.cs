using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AutoSaveSlider_ForAudio : AutoSaveSlider
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string exposedParameterName;

    // Start is called before the first frame update
    protected override void InternalValueChanged(float newValue)
    {
        audioMixer.SetFloat(exposedParameterName, LinearToDecibel(newValue));
    }


    // Update is called once per frame
    public void SetSliderValue(float soundlevel)
    {
        audioMixer.SetFloat(exposedParameterName, soundlevel);
        audioMixer.SetFloat(exposedParameterName, LinearToDecibel(soundlevel));
        PlayerPrefs.Save();
    }
    // https://forum.unity.com/threads/how-to-calculate-db-correct.546128/
    private float LinearToDecibel(float linear)
    {
        float dB;
        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;
        return dB;
    }

}
