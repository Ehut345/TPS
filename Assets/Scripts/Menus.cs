using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [Header("All menus's")]
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject endGameMenu;
    [SerializeField] public GameObject objectiveMenu;
    [SerializeField] public GameObject CH;

    public static bool gameIsPaused = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            if (gameIsPaused)
            {
                RemoveObjectives();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else 
            {
                ShowObjectives();
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    public void Resume()
    {
        CH.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        gameIsPaused = false;
    }
    public void Pause()
    {
        CH.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        gameIsPaused = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowObjectives()
    {
        CH.SetActive(false);
        objectiveMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void RemoveObjectives()
    {
        CH.SetActive(true);
        objectiveMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        gameIsPaused = false;
    }
}
