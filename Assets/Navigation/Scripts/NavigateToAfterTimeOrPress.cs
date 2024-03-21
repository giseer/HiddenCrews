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
    }

    private void OnEnable()
    {
        skip.action.Enable();
    }

    private void Update()
    {
        if(skip.action.WasPerformedThisFrame())
        {
            NavigateToNextScene();

        }
    }

    private void OnDisable()
    {
        skip.action.Disable();
    }

    void NavigateToNextScene()
    {
        NavigatorManager.LoadScene(sceneToNavigateTo); 
    }

}
