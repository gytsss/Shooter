using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject victoryPanel;
    [SerializeField] private int enemiesToKill = 1;

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
    }

    private void ShowVictoryPanel()
    {
        victoryPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
