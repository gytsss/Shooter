using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the pausing and resuming of the game, as well as adjusting the time scale, activating and deactivating the pause panel, and managing the cursor lock state.
/// </summary>
public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject button;
    [SerializeField] private string World;
    private bool isGamePaused = false;

    public bool IsGamePaused => isGamePaused;

    /// <summary>
    /// Toggles the game pause status and calls the PauseGame method.
    /// </summary>
    public void OnPause()
    {
        isGamePaused = !isGamePaused;
        PauseGame();
    }

    /// <summary>
    /// Pauses or resumes the game based on the current pause status, adjusts the time scale, activates or deactivates the pause panel, and manages the cursor lock state.
    /// </summary>
    private void PauseGame()
    {
        if(isGamePaused)
        {
            EventSystem.current.SetSelectedGameObject(button);
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

    /// <summary>
    /// Subscribes to the SceneManager's sceneLoaded event when the script is enabled.
    /// </summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Unsubscribes from the SceneManager's sceneLoaded event when the script is disabled.
    /// </summary>
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Called when a new scene is loaded and checks if the loaded scene matches the specified world name to pause the game if necessary.
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == World)
        {
            PauseGame();
        }
    }
}
