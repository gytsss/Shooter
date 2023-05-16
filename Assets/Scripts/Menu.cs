using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
   
    private bool isGamePaused = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            isGamePaused = !isGamePaused;
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if(isGamePaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
        }
    }
}
