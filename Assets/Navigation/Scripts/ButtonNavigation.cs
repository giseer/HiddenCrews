using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNavigation : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    
    [SerializeField] private bool shutdownMusicWhenPress = false;
    
    private void OnEnable()
    {
        GetComponentInChildren<Button>().onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        Time.timeScale = 1f;
        
        if (shutdownMusicWhenPress)
        {
            AudioSource[] musicControllers = FindObjectsOfType<AudioSource>();

            foreach (AudioSource audioSource in musicControllers)
            {
                audioSource.Stop();
            }
        }
        
        NavigatorManager.LoadScene(sceneToLoad);
    }

    private void Disable()
    {
        GetComponentInChildren<Button>().onClick.RemoveListener(LoadScene);
    }
}
