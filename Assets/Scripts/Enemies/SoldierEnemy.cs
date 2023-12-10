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
    #region EVENTS

    public static event Action Destroyed;

    #endregion

    #region EXPOSED_FIELDS

    [SerializeField] private float range = 10f;
    [SerializeField] private Transform player;
    [SerializeField] private RaycastWeapon weapon;
    [SerializeField] private float shotCooldown = 3f;
    [SerializeField] private Slider healthBar;

    #endregion

    #region PRIVATE_FIELDS

    private float timer = 0f;

    #endregion

    #region PUBLIC_METHODS

    public HealthComponent healthComponent;

    #endregion

    #region UNITY_CALLS

    /// <summary>
    /// Initializes current health of the enemy.
    /// </summary>
    private void Awake()
    {
        healthComponent.OnDecrease_Health += TakeDamage;
        healthComponent._health = healthComponent._maxHealth;
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
    ///  Invokes the Destroyed event when the enemy is destroyed.
    /// </summary>
    private void OnDestroy()
    {
        healthComponent.OnDecrease_Health -= TakeDamage;
        Destroyed?.Invoke();
    }

    #endregion

    #region PRIVATE_METHODS

    /// <summary>
    /// Shoots raycast attacking the player
    /// </summary>
    private void Shoot()
    {
        weapon.OnEnemyFire(transform);
    }


    /// <summary>
    /// Calculates the current health percentage of the enemy.
    /// </summary>
    private void CalculateHealthPercentage()
    {
        float healthPercentage = healthComponent._health / healthComponent._maxHealth;
        UpdateHealthBar(healthPercentage);
    }

    /// <summary>
    /// Updates the health bar UI element with the provided health percentage.
    /// </summary>
    private void UpdateHealthBar(float healthPercentage)
    {
        healthBar.value = healthPercentage;
    }

    #endregion

    #region PUBLIC_METHODS

    /// <summary>
    /// Called when the enemy takes damage, reducing its current health, checking if it is destroyed, and updating the health bar.
    /// </summary>
    public void TakeDamage()
    {
        if (healthComponent._health <= 0)
        {
            Destroy(gameObject);
        }

        CalculateHealthPercentage();
    }

    #endregion
}