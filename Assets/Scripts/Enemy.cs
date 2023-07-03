using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// Enemy class manages the behavior and state of an enemy in the game. It includes functionalities for patrol,chasing and attacking the player.
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Event triggered when an enemy is destroyed
    /// </summary>
    public static event Action Destroyed;
    private enum EnemyState
    {
        Patrol,
        Chase,
        Attack
    }

    private EnemyState currentState;

    [SerializeField] private Slider healthBar;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    private Transform player;

    [Header("Patroling")]
    [SerializeField] private Transform[] walkPoints;
    private int walkPointIndex;
    private Vector3 target;

    [Header("Attack")]
    [SerializeField] private float attackCooldown;
    private bool alreadyAttacked;

    [Header("States")]
    [SerializeField] private float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;
    private float distanceToPlayer;

    /// <summary>
    /// Initializes the player reference and current health of the enemy.
    /// </summary>
    private void Awake()
    {
        player = PlayerFactory.CreatePlayer().transform;
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Sets the initial state of the enemy to Patrol and updates the destination.
    /// </summary>
    private void Start()
    {
        currentState = EnemyState.Patrol;
        UpdateDestination();
    }
    /// <summary>
    /// Updates the enemy's state based on the distance to the player.
    /// </summary>
    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
            playerInAttackRange = false;
        else
            playerInAttackRange = true;

        if (distanceToPlayer > sightRange)
            playerInSightRange = false;
        else
            playerInSightRange = true;


        switch (currentState)
        {
            case EnemyState.Patrol:
                if (playerInSightRange && !playerInAttackRange)
                {
                    currentState = EnemyState.Chase;
                    ChasePlayer();
                }
                else if (playerInSightRange && playerInAttackRange)
                {
                    currentState = EnemyState.Attack;
                    AttackPlayer();
                }
                else
                {
                    Patroling();
                }
                break;
            case EnemyState.Chase:
                if (!playerInSightRange)
                {
                    currentState = EnemyState.Patrol;
                    Patroling();
                }
                else if (playerInSightRange && playerInAttackRange)
                {
                    currentState = EnemyState.Attack;
                    AttackPlayer();
                }
                else
                {
                    ChasePlayer();
                }
                break;
            case EnemyState.Attack:
                if (!playerInAttackRange)
                {
                    if (playerInSightRange)
                    {
                        currentState = EnemyState.Chase;
                        ChasePlayer();
                    }
                    else
                    {
                        currentState = EnemyState.Patrol;
                        Patroling();
                    }
                }
                else
                {
                    AttackPlayer();
                }
                break;
        }
    }

    /// <summary>
    /// Handles the enemy's patrolling behavior by checking the distance to the target and updating it if the enemy is close enough.
    /// </summary>
    private void Patroling()
    {
        if (Vector3.Distance(transform.position, target) < 2f)
        {
            IterateWalkPointIndex();
            UpdateDestination();
        }

    }

    /// <summary>
    /// Updates the target destination for the enemy's navigation agent based on the current walk point index.
    /// </summary>
    private void UpdateDestination()
    {
        target = walkPoints[walkPointIndex].position;
        enemy.SetDestination(target);
    }

    /// <summary>
    /// Increments the walk point index and resets it to 0 if it reaches the end of the walk point array.
    /// </summary>
    private void IterateWalkPointIndex()
    {
        walkPointIndex++;

        if (walkPointIndex == walkPoints.Length)
        {
            walkPointIndex = 0;
        }
    }

    /// <summary>
    /// Sets the target destination of the enemy's navigation agent to the player's position, making the enemy chase the player.
    /// </summary>
    private void ChasePlayer()
    {
        enemy.SetDestination(player.position);
    }

    /// <summary>
    /// Handles the enemy's attack behavior, including setting the target destination, rotating the enemy to face the player, and shooting a projectile.
    /// </summary>
    protected void AttackPlayer()
    {
        enemy.SetDestination(transform.position);

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);

        if (!alreadyAttacked)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20f + transform.up, ForceMode.Impulse);

            alreadyAttacked = true;
            StartCoroutine(ResetAttack());
        }
    }

    /// <summary>
    /// Resets the enemy's attack state after a certain cooldown, allowing it to attack again.
    /// </summary>
    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        alreadyAttacked = false;
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
        /// <summary>
        /// Event triggered when an enemy is destroyed.
        /// </summary>
        Destroyed?.Invoke();
    }

}
