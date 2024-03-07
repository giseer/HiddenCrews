using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] InputActionReference pause;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsCourse;
    [SerializeField] bool menuOpened;

    // Start is called before the first frame update
    private void Awake()
    {
        menuOpened = false;
    }
    private void OnEnable()
    {
        pause.action.performed += PauseGame;
        pause.action.Enable();
    }
    private void OnDisable()
    {
        pause.action.performed -= PauseGame;

        pause.action.Disable();
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        Cursor.visible = pauseMenu.gameObject.activeSelf;

        Cursor.lockState = pauseMenu.gameObject.activeSelf ? CursorLockMode.Confined : CursorLockMode.None;

        if (menuOpened)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            menuOpened = false;

        }
        else
        {
            menuOpened = true;
            ResumeGameplay();
        }
    }
    public void ResumeGameplay()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
    }

    public void Options()
    {
        optionsCourse.SetActive(true);
        pauseMenu.SetActive(false);
        Cursor.visible = optionsCourse.gameObject.activeSelf;

        Cursor.lockState = optionsCourse.gameObject.activeSelf ? CursorLockMode.Confined : CursorLockMode.None;
    }

}
