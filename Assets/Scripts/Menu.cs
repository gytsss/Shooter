using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private bool isGamePaused = false;
    //TODO: TP2 - Remove unused methods/variables/classes
    
    public void OnPause()
    {
        isGamePaused = !isGamePaused;
        PauseGame();
    }

    private void PauseGame()
    {
        if(isGamePaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //TODO: Fix - Hardcoded value
        if (scene.name == "World")
        {
            PauseGame();
        }
    }
}
