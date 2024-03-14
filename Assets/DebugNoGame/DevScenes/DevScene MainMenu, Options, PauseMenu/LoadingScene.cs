using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScene : MonoBehaviour
{
    [SerializeField]string nextScene;

    public void GoToScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
