using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject victoryPanel;
    [SerializeField] private int enemiesToKill = 1;
    [SerializeField] private TextMeshProUGUI remainingEnemiesText;
    private int remainingEnemies;

    private void Awake()
    {
        StaticEnemy.Destroyed += ReduceEnemy;
        Enemy.Destroyed += ReduceEnemy;
    }

    void Start()
    {
        remainingEnemies = enemiesToKill;
        victoryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        UpdateRemainingEnemiesText();
    }

    void Update()
    {
        if (remainingEnemies <= 0)
        {
            ShowVictoryPanel();
        }
    }

    private void ReduceEnemy()
    {
        remainingEnemies--;
        UpdateRemainingEnemiesText();
    }

    private void ShowVictoryPanel()
    {
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    private void UpdateRemainingEnemiesText()
    {
        remainingEnemiesText.text = "Enemies left: " + remainingEnemies.ToString();
    }

    private void OnDestroy()
    {
        StaticEnemy.Destroyed -= ReduceEnemy;
        Enemy.Destroyed -= ReduceEnemy;

    }
}
