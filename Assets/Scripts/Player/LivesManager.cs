using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Manages player lives
/// </summary>
public class LivesManager : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private GameObject lossPanel;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject button;
    [SerializeField] private int maxLives = 3;

    #endregion

    #region PRIVATE_FIELDS

    private int currentLives = 0;

    #endregion

    #region UNITY_CALLS

    /// <summary>
    /// Subscribes to the Destroyed events of Player class.
    /// </summary>
    private void Awake()
    {
        Player.Destroyed += ReduceLife;
    }

    /// <summary>
    /// Sets up initial game state, such as remaining enemies and UI elements.
    /// </summary>
    private void Start()
    {
        Time.timeScale = 1f;
        lossPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        currentLives = maxLives;
        UpdateLivesText();
    }

    /// <summary>
    /// Checks if all enemies have been destroyed and shows victory panel if true.
    /// </summary>
    private void Update()
    {
        if (currentLives <= 0)
        {
            ShowLossPanel();
        }
    }

    /// <summary>
    /// Unsubscribes from the Destroyed events of Player class.
    /// </summary>
    private void OnDestroy()
    {
        Player.Destroyed -= ReduceLife;
    }

    #endregion

    #region PRIVATE_METHODS

    /// <summary>
    /// Reduces the count of remaining enemies.
    /// </summary>
    private void ReduceLife()
    {
        currentLives--;
        UpdateLivesText();
    }

    /// <summary>
    /// Shows the victory panel and pauses the game.
    /// </summary>
    private void ShowLossPanel()
    {
        EventSystem.current.SetSelectedGameObject(button);

        Time.timeScale = 0f;
        lossPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Updates the remaining enemies text on the UI.
    /// </summary>
    private void UpdateLivesText()
    {
        livesText.text = "Lives: " + currentLives.ToString();
    }

    #endregion
}