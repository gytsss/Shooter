using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a static enemy object in a game. It includes a Destroyed event that is invoked when the enemy is destroyed.
/// </summary>
public class StaticEnemy : MonoBehaviour
{
    public static event Action Destroyed;
    [SerializeField] private Slider healthBar;
    [SerializeField] private float maxHealth = 3f;
    private float currentHealth = 0;

    /// <summary>
    /// Initializes the current health to the maximum health value.
    /// </summary>
    private void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Reduces the current health by 1. If the health becomes zero or below, destroys the game object. Calls CalculateHealthPercentage() to update the health bar.
    /// </summary>
    public void TakeDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        CalculateHealthPercentage();
    }

    /// <summary>
    /// Invokes the Destroyed event.
    /// </summary>
    public void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    /// <summary>
    /// Calculates the current health percentage and calls UpdateHealthBar() to update the health bar accordingly.
    /// </summary>
    private void CalculateHealthPercentage()
    {
        float healthPercentage = currentHealth / maxHealth;
        UpdateHealthBar(healthPercentage);
    }

    /// <summary>
    /// Updates the health bar based on the given health percentage.
    /// </summary>
    private void UpdateHealthBar(float healthPercentage)
    {
        healthBar.value = healthPercentage;
    }
}
