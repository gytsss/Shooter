using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName;
    [SerializeField] private string creditsSceneName;
    [SerializeField] private string mainMenuSceneName;

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    //TODO: Fix - Repeated code
    public void ShowCredits()
    {
        SceneManager.LoadScene(creditsSceneName);
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
