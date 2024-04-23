using UnityEngine;
using UnityEngine.InputSystem;

public class NavigateToAfterTimeOrPress : MonoBehaviour
{
    [SerializeField] string sceneToNavigateTo;
    [SerializeField] float waitTime = 5f;
    [SerializeField] InputActionReference skip;

    [SerializeField] bool unblockMouseAfterTransition = false;

    bool skipped = false;

    private void Start()
    {
        skipped = false;
        Invoke(nameof(NavigateToNextScene), waitTime);
    }

    private void OnEnable()
    {
        if(skip)
        {
            skip.action.Enable();
        }        
    }

    private void Update()
    {
        if (skip)
        {
            if (skip.action.WasPerformedThisFrame() && !skipped)
            {
                skipped = true;
                NavigateToNextScene();
            }
        }
    }

    private void OnDisable()
    {
        if (skip)
        {
            skip.action.Disable();
        }
    }

    void NavigateToNextScene()
    {
        Time.timeScale = 1f;

        NavigatorManager.LoadScene(sceneToNavigateTo); 

        if(unblockMouseAfterTransition)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
