using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Attacks the player by shooting a laser and deals damage within a certain range.
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class DroneEnemy : MonoBehaviour
{
    public static event Action Destroyed;
    
    [SerializeField] private Transform player;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private Slider healthBar;
    [SerializeField] private float shootingRange = 10f;
    [SerializeField] private float visionRange = 10f;
    [SerializeField] private float shootingDamage = 10f;
    [SerializeField] private float laserDuration = .05f;
    [SerializeField] private float fireRate = .2f;

    private float fireTimer;
    private LineRenderer laserLine;

    public HealthComponent healthComponent;

    /// <summary>
    /// Initializes the LineRenderer component reference.
    /// </summary>
    private void Awake()
    {
        healthComponent.OnDecrease_Health += TakeDamage;
        healthComponent._health = healthComponent._maxHealth;
        laserLine = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Checks if the player is in vision range and attacks if the fire rate allows it.
    /// </summary>
    private void Update()
    {
        fireTimer += Time.deltaTime;

        if (IsPlayerInVisionRange() && fireTimer > fireRate)
        {
            fireTimer = 0;
            AttackPlayer();
        }
    }

    /// <summary>
    /// Attacks the player by shooting a laser and dealing damage if the player is in range.
    /// </summary>
    public void AttackPlayer()
    {
        laserLine.SetPosition(0, rayPoint.position);

        Vector3 direction = player.position - transform.position;
        Ray ray = new Ray(transform.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, shootingRange))
        {
            if (hit.collider.TryGetComponent(out Player player))
            {
                laserLine.SetPosition(1, hit.point);
                player.healthComponent.DecreaseHealth(shootingDamage);
            }
        }

        StartCoroutine(ShootLaser());
    }

    /// <summary>
    /// Checks if the player is within the vision range of the drone enemy.
    /// </summary>
    /// <returns>True if the player is within the vision range, false otherwise.</returns>
    private bool IsPlayerInVisionRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= visionRange;
    }

    public void TakeDamage()
    {
        if (healthComponent._health <= 0)
        {
            Destroy(gameObject);
        }
        CalculateHealthPercentage();
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
    
    /// <summary>
    /// Coroutine to display the laser for a short duration and then disable it.
    /// </summary>
    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }

    private void OnDestroy()
    {
        healthComponent.OnDecrease_Health -= TakeDamage;
        Destroyed?.Invoke();
    }
}