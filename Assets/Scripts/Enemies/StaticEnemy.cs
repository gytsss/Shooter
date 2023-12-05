using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a static enemy object in a game. It includes a Destroyed event that is invoked when the enemy is destroyed.
/// </summary>
public class StaticEnemy : MonoBehaviour
{
    public static event Action Destroyed;

    [SerializeField] private Slider healthBar;

    public HealthComponent healthComponent;


    private void Awake()
    {
        healthComponent.OnDecrease_Health += TakeDamage;
    }


    /// <summary>
    /// Initializes the current health to the maximum health value.
    /// </summary>
    private void Start()
    {
        healthComponent._health = healthComponent._maxHealth;
    }

    /// <summary>
    /// Reduces the current health by 1. If the health becomes zero or below, destroys the game object. Calls CalculateHealthPercentage() to update the health bar.
    /// </summary>
    public void TakeDamage()
    {
        if (healthComponent._health <= 0)
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
        healthComponent.OnDecrease_Health -= TakeDamage;
        Destroyed?.Invoke();
    }

    /// <summary>
    /// Calculates the current health percentage and calls UpdateHealthBar() to update the health bar accordingly.
    /// </summary>
    private void CalculateHealthPercentage()
    {
        float healthPercentage = healthComponent._health / healthComponent._maxHealth;
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