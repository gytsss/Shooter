using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;
using TMPro;


public class GameTimer : MonoBehaviour
{
    [SerializeField] GameObject lossPanel;
    [SerializeField] private TextMeshProUGUI remainingTimeText;
    [SerializeField] private float timeToWin = 15f;
     private float remainingTime;

    private void Start()
    {
        remainingTime = timeToWin;
        lossPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        remainingTime -= Time.deltaTime;

        remainingTimeText.text = "Time left: " + remainingTime.ToString("0.#");

        if (remainingTime <= 0)
        {
            ShowLossPanel();
        }

    }

    private void ShowLossPanel()
    {
        Time.timeScale = 0f;
        lossPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
