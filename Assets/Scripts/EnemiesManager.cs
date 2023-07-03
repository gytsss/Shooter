using TMPro;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject victoryPanel;
    [SerializeField] private int enemiesToKill = 1;
    [SerializeField] private TextMeshProUGUI remainingEnemiesText;
    private int remainingEnemies;

    /// <summary>
    /// Subscribes to the Destroyed events of StaticEnemy and Enemy classes.
    /// </summary>
    private void Awake()
    {
        StaticEnemy.Destroyed += ReduceEnemy;
        Enemy.Destroyed += ReduceEnemy;
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
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);
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
    /// Unsubscribes from the Destroyed events of StaticEnemy and Enemy classes.
    /// </summary>
    private void OnDestroy()
    {
        StaticEnemy.Destroyed -= ReduceEnemy;
        Enemy.Destroyed -= ReduceEnemy;

    }
}
