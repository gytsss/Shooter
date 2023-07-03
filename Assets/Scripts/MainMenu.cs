using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the navigation within the game's main menu, providing functions and events to load different scenes such as the level selector, levels, and credits.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private string levelSelectorSceneName;
    [SerializeField] private string level1SceneName;
    [SerializeField] private string level2SceneName;
    [SerializeField] private string level3SceneName;
    [SerializeField] private string creditsSceneName;
    [SerializeField] private string mainMenuSceneName;

    /// <summary>
    /// A function that loads a specified scene using SceneManager.LoadScene..
    /// </summary>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// An event that triggers the loading of the level selector scene using LoadScene.
    /// </summary>
    public void SelectLevel()
    {
        LoadScene(levelSelectorSceneName);
    }

    /// <summary>
    /// An event that triggers the loading of level 1 scene using LoadScene.
    /// </summary>
    public void PlayLevel1()
    {
        LoadScene(level1SceneName);
    }

    /// <summary>
    /// An event that triggers the loading of level 2 scene using LoadScene.
    /// </summary>
    public void PlayLevel2()
    {
        LoadScene(level2SceneName);
    }

    /// <summary>
    /// An event that triggers the loading of level 3 scene using LoadScene.
    /// </summary>
    public void PlayLevel3()
    {
        LoadScene(level3SceneName);
    }

    /// <summary>
    /// An event that triggers the loading of the credits scene using LoadScene.
    /// </summary>
    public void ShowCredits()
    {
        LoadScene(creditsSceneName);
    }

    /// <summary>
    /// An event that triggers the loading of the main menu scene using LoadScene.
    /// </summary>
    public void GoMainMenu()
    {
        LoadScene(mainMenuSceneName);
    }

    /// <summary>
    /// An event that quits the application using Application.Quit.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
