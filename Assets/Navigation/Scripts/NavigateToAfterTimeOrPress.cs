using UnityEngine;
using UnityEngine.InputSystem;

public class NavigateToAfterTimeOrPress : MonoBehaviour
{
    [SerializeField] string sceneToNavigateTo;
    [SerializeField] float waitTime = 5f;
    [SerializeField] InputActionReference skip;

    private void Start()
    {
        Invoke(nameof(NavigateToNextScene), waitTime);
        //TODO: responder al skip
    }

    void NavigateToNextScene()
    {
        NavigatorManager.LoadScene(sceneToNavigateTo); 
    }

}
