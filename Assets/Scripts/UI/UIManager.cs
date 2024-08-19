using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject laserPointer;

    public GameObject pauseMenu;

    private void Start()
    {
        isPaused = false;
    }
    public void PauseGame(InputAction.CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        laserPointer.SetActive(false);
        PlayerController.instance.canMove = false;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        laserPointer.SetActive(true);
        PlayerController.instance.canMove = true;
        isPaused = false;
    }

    public void LoadScene(int scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }




}
