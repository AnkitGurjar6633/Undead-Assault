using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [Header("Menu UIs")]
    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;
    public GameObject objectivesUI;

    [Header("Misc")]
    public static bool isGameStopped;
    public GameObject aimCanvas;
    public GameObject tpsCanvas;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGameStopped){
                Pause();
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isGameStopped)
            {
                ShowObjectivesUI();
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                HideObjectivesUI();
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void ShowObjectivesUI()
    {
        objectivesUI.SetActive(true);
        aimCanvas.SetActive(false);
        tpsCanvas.SetActive(false);
        Time.timeScale = 0f;
        isGameStopped = true;
    }

    public void HideObjectivesUI()
    {
        objectivesUI.SetActive(false);
        aimCanvas.SetActive(true);
        tpsCanvas.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        isGameStopped = false;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        aimCanvas.SetActive(true);
        tpsCanvas.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        isGameStopped = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("UndeadAssault", LoadSceneMode.Single);
        Time.timeScale = 1f;
        gameOverMenuUI.SetActive(false);
        isGameStopped = false;
    }

    public void ShowMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        isGameStopped = false;

    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        aimCanvas.SetActive(false);
        tpsCanvas.SetActive(false);
        Time.timeScale = 0f;
        isGameStopped = true;
    }


}
