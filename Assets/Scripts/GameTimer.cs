using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;
using TMPro;


/// <summary>
///Manages the game's timer by tracking the remaining time, updating it continuously, and displaying it in the UI.
/// </summary>
public class GameTimer : MonoBehaviour
{
    [SerializeField] GameObject lossPanel;
    [SerializeField] private TextMeshProUGUI remainingTimeText;
    [SerializeField] private float timeToWin = 15f;
     private float remainingTime;

    /// <summary>
    /// Initializes the timer by setting the initial remaining time, enabling normal time scale, hiding the loss panel, and locking the cursor.
    /// </summary>
    private void Start()
    {
        remainingTime = timeToWin;
        Time.timeScale = 1f;
        lossPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Updates the remaining time by subtracting the elapsed time from the total time. Updates the UI text to display the remaining time. If the remaining time reaches zero, it calls the ShowLossPanel() function.
    /// </summary>
    private void Update()
    {
        remainingTime -= Time.deltaTime;

        remainingTimeText.text = "Time left: " + remainingTime.ToString("0.#");

        if (remainingTime <= 0)
        {
            ShowLossPanel();
        }

    }

    /// <summary>
    /// Pauses the game's time scale, displays the loss panel, and releases the cursor lock.
    /// </summary>
    private void ShowLossPanel()
    {
        Time.timeScale = 0f;
        lossPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
