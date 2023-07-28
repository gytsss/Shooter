using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the player's health, health bar, damage taken from enemies, respawn point, and collision with enemy bullets.
/// </summary>
public class Player : MonoBehaviour
{
    public static event Action Destroyed;
    [SerializeField] private Slider healthBar;
    [SerializeField] public float maxHealth = 100;
    [SerializeField] private float enemyDamage = 10;
    [SerializeField] private string enemyBulletTag = "EnemyBullet";
    [SerializeField] private Transform respawnPoint;
    public float currentHealth;

    /// <summary>
    /// Sets the player's current health to the maximum health value when the game starts.
    /// </summary>
    private void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Decreases the player's health by the specified amount, respawns the player if health reaches zero, and updates the health bar.
    /// </summary>
    public void LoseHealth(float health)
    {
        PlayDamageSound();

        currentHealth -= health;

        if (currentHealth <= 0)
        {
            Destroyed?.Invoke();
            currentHealth = maxHealth;
            transform.position = respawnPoint.position;
        }

        CalculateHealthPercentage();
    }

    /// <summary>
    /// Calculates the player's health percentage based on the current health and maximum health, and calls UpdateHealthBar.
    /// </summary>
    private void CalculateHealthPercentage()
    {
        float healthPercentage = currentHealth / maxHealth;
        UpdateHealthBar(healthPercentage);
    }

    /// <summary>
    /// Updates the health bar value based on the given health percentage.
    /// </summary>
    private void UpdateHealthBar(float healthPercentage)
    {
        healthBar.value = healthPercentage;
    }

    /// <summary>
    /// Play damage sound
    /// </summary>
    private void PlayDamageSound()
    {
        FindObjectOfType<SoundManager>().Play("DamageTaken");
    }

    /// <summary>
    /// Handles collision events and applies damage to the player if colliding with an enemy bullet.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(enemyBulletTag))
            LoseHealth(enemyDamage);
    }
}
