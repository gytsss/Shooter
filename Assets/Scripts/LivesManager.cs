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
    [SerializeField] GameObject lossPanel;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject button;
    [SerializeField] private int maxLives = 3;
    private int currentLives = 0;

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
    void Start()
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
    void Update()
    {
        if (currentLives <= 0)
        {
            ShowLossPanel();
        }
    }

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

    /// <summary>
    /// Unsubscribes from the Destroyed events of Player class.
    /// </summary>
    private void OnDestroy()
    {
        Player.Destroyed -= ReduceLife;
    }
}
