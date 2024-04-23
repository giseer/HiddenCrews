using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNavigation : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private void OnEnable()
    {
        GetComponentInChildren<Button>().onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        Time.timeScale = 1f;
        NavigatorManager.LoadScene(sceneToLoad);
    }

    private void Disable()
    {
        GetComponentInChildren<Button>().onClick.RemoveListener(LoadScene);
    }
}
