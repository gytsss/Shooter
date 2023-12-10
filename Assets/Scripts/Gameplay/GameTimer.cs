using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
///Manages the game's timer by tracking the remaining time, updating it continuously, and displaying it in the UI.
/// </summary>
public class GameTimer : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private GameObject lossPanel;
    [SerializeField] private TextMeshProUGUI remainingTimeText;
    [SerializeField] private float timeToWin = 15f;
    [SerializeField] private GameObject button;

    #endregion

    #region PRIVATE_FIELDS

    private float remainingTime;
    private bool hasPlayedLoseSound = false;

    #endregion

    #region UNITY_CALLS

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

    #endregion

    #region PRIVATE_METHODS

    /// <summary>
    /// Pauses the game's time scale, displays the loss panel, and releases the cursor lock.
    /// </summary>
    private void ShowLossPanel()
    {
        if (!hasPlayedLoseSound)
        {
            PlayLoseSound();
            hasPlayedLoseSound = true;
        }

        EventSystem.current.SetSelectedGameObject(button);

        Time.timeScale = 0f;
        lossPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Play lose sound
    /// </summary>
    private void PlayLoseSound()
    {
        SoundManager.instance.PlayLose();
    }

    #endregion

    #region PUBLIC_METHODS

    /// <summary>
    /// Set remaining time from the cheats manager
    /// </summary>
    public void SetRemainingTime(float time)
    {
        remainingTime = time;
    }

    /// <summary>
    /// Start the remaining time with the default value
    /// </summary>
    public void ResetRemainingTime()
    {
        remainingTime = timeToWin;
    }

    #endregion
}