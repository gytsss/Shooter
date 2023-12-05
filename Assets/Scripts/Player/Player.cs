using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the player's health, health bar, damage taken from enemies, respawn point, and collision with enemy bullets.
/// </summary>
public class Player : MonoBehaviour
{
    public static event Action Destroyed;

    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Slider healthBar;
    [SerializeField] private float enemyDamage = 10;
    [SerializeField] private string enemyBulletTag = "EnemyBullet";

    public HealthComponent healthComponent;


    private void Awake()
    {
        healthComponent.OnDecrease_Health += TakeDamage;
    }


    /// <summary>
    /// Sets the player's current health to the maximum health value when the game starts.
    /// </summary>
    private void Start()
    {
        healthComponent._health = healthComponent._maxHealth;
    }

    /// <summary>
    /// Decreases the player's health by the specified amount, respawns the player if health reaches zero, and updates the health bar.
    /// </summary>
    public void TakeDamage()
    {
        PlayDamageSound();

        if (healthComponent._health <= 0)
        {
            Destroyed?.Invoke();
            healthComponent._health = healthComponent._maxHealth;
            transform.position = respawnPoint.position;
        }

        CalculateHealthPercentage();
    }

    /// <summary>
    /// Calculates the player's health percentage based on the current health and maximum health, and calls UpdateHealthBar.
    /// </summary>
    private void CalculateHealthPercentage()
    {
        float healthPercentage = healthComponent._health / healthComponent._maxHealth;
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
    /// Plays damage sound
    /// </summary>
    private void PlayDamageSound()
    {
        SoundManager.instance.PlayDamageTaken();
    }

    /// <summary>
    /// Handles collision events and applies damage to the player if colliding with an enemy bullet.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(enemyBulletTag))
            healthComponent.DecreaseHealth(enemyDamage);
    }
}