using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Manages the game's enemy system and keeping track of remaining enemies.
/// </summary>
public class EnemiesManager : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private int enemiesToKill = 1;
    [SerializeField] private TextMeshProUGUI remainingEnemiesText;
    [SerializeField] private GameObject button;
    [SerializeField] private PauseController pauseController;

    #endregion

    #region PRIVATE_FIELDS

    private bool hasPlayedWinSound = false;

    #endregion

    #region PUBLIC_FIELDS

    public int remainingEnemies;

    #endregion

    #region UNITY_CALLS

    /// <summary>
    /// Subscribes to the Destroyed events of StaticEnemy, Enemy and SoldierEnemy classes.
    /// </summary>
    private void Awake()
    {
        StaticEnemy.Destroyed += ReduceEnemy;
        Enemy.Destroyed += ReduceEnemy;
        SoldierEnemy.Destroyed += ReduceEnemy;
        DroneEnemy.Destroyed += ReduceEnemy;
    }

    /// <summary>
    /// Sets up initial game state, such as remaining enemies and UI elements.
    /// </summary>
    void Start()
    {
        remainingEnemies = enemiesToKill;
        Time.timeScale = 1f;
        victoryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        UpdateRemainingEnemiesText();
    }

    /// <summary>
    /// Checks if all enemies have been destroyed and shows victory panel if true.
    /// </summary>
    void Update()
    {
        if (remainingEnemies <= 0)
        {
            ShowVictoryPanel();
        }
    }

    /// <summary>
    /// Unsubscribes from the Destroyed events of StaticEnemy and Enemy classes.
    /// </summary>
    private void OnDestroy()
    {
        StaticEnemy.Destroyed -= ReduceEnemy;
        Enemy.Destroyed -= ReduceEnemy;
        SoldierEnemy.Destroyed -= ReduceEnemy;
        DroneEnemy.Destroyed -= ReduceEnemy;
    }

    #endregion

    #region PRIVATE_METHODS

    /// <summary>
    /// Reduces the count of remaining enemies.
    /// </summary>
    private void ReduceEnemy()
    {
        remainingEnemies--;
        UpdateRemainingEnemiesText();
    }

    /// <summary>
    /// Shows the victory panel and pauses the game.
    /// </summary>
    private void ShowVictoryPanel()
    {
        if (!hasPlayedWinSound)
        {
            PlayWinSound();
            hasPlayedWinSound = true;
        }

        EventSystem.current.SetSelectedGameObject(button);

        Time.timeScale = 0f;
        victoryPanel.SetActive(true);
        pauseController.SetPauseGame(true);
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Updates the remaining enemies text on the UI.
    /// </summary>
    private void UpdateRemainingEnemiesText()
    {
        remainingEnemiesText.text = "Enemies left: " + remainingEnemies.ToString();
    }

    /// <summary>
    /// Play win sound
    /// </summary>
    private void PlayWinSound()
    {
        SoundManager.instance.PlayWin();
    }

    #endregion
}