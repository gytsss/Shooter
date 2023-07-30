using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages soldier enemy which use player weapons
/// </summary>
public class SoldierEnemy : MonoBehaviour
{
    public static event Action Destroyed;

    public float maxHealth = 100f;
    [SerializeField] private float range = 10f; 
    [SerializeField] private Transform player; 
    [SerializeField] private RaycastWeapon weapon;
    [SerializeField] private float shotCooldown = 3f; 
    [SerializeField] private Slider healthBar;
    private float currentHealth;
    private float timer = 0f;

    /// <summary>
    /// Initializes current health of the enemy.
    /// </summary>
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Checks distance to the player and shot cooldown
    /// </summary>
    private void Update()
    {
        timer += Time.deltaTime;

        if (Vector3.Distance(transform.position, player.position) <= range)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);

            if (timer >= shotCooldown)
            {
                Shoot(); 
                timer = 0f; 
            }
        }
    }

    /// <summary>
    /// Shoots raycast attacking the player
    /// </summary>
    private void Shoot()
    {
        weapon.OnEnemyFire(transform);
    }

    /// <summary>
    /// Called when the enemy takes damage, reducing its current health, checking if it is destroyed, and updating the health bar.
    /// </summary>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        CalculateHealthPercentage();
    }

    /// <summary>
    /// Calculates the current health percentage of the enemy.
    /// </summary>
    private void CalculateHealthPercentage()
    {
        float healthPercentage = currentHealth / maxHealth;
        UpdateHealthBar(healthPercentage);
    }

    /// <summary>
    /// Updates the health bar UI element with the provided health percentage.
    /// </summary>
    private void UpdateHealthBar(float healthPercentage)
    {
        healthBar.value = healthPercentage;
    }

    /// <summary>
    ///  Invokes the Destroyed event when the enemy is destroyed.
    /// </summary>
    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }
}
