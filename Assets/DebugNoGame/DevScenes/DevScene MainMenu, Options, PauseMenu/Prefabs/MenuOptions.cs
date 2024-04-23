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

    // Start is called before the first frame update
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
        if (pauseMenu.activeSelf == false)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
        }
        else
        {
            ResumeGameplay();
        }
    }
    public void ResumeGameplay()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
    }

}
