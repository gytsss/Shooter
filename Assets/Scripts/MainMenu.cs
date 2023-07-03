using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string levelSelectorSceneName;
    [SerializeField] private string level1SceneName;
    [SerializeField] private string level2SceneName;
    [SerializeField] private string level3SceneName;
    [SerializeField] private string creditsSceneName;
    [SerializeField] private string mainMenuSceneName;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void SelectLevel()
    {
        LoadScene(levelSelectorSceneName);
    } 
    public void PlayLevel1()
    {
        LoadScene(level1SceneName);
    } 
    public void PlayLevel2()
    {
        LoadScene(level2SceneName);
    } 
    public void PlayLevel3()
    {
        LoadScene(level3SceneName);
    }
    public void ShowCredits()
    {
        LoadScene(creditsSceneName);
    }
    public void GoMainMenu()
    {
        LoadScene(mainMenuSceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
