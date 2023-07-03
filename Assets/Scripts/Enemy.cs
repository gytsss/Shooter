using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
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


    private void Awake()
    {
        player = PlayerFactory.CreatePlayer().transform;
        currentHealth = maxHealth;
    }

    private void Start()
    {
        currentState = EnemyState.Patrol;
        UpdateDestination();
    }

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

    private void Patroling()
    {
        if (Vector3.Distance(transform.position, target) < 2f)
        {
            IterateWalkPointIndex();
            UpdateDestination();
        }

    }

    private void UpdateDestination()
    {
        target = walkPoints[walkPointIndex].position;
        enemy.SetDestination(target);
    }

    private void IterateWalkPointIndex()
    {
        walkPointIndex++;

        if (walkPointIndex == walkPoints.Length)
        {
            walkPointIndex = 0;
        }
    }

    private void ChasePlayer()
    {
        enemy.SetDestination(player.position);
    }

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

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        CalculateHealthPercentage();
    }

    private void CalculateHealthPercentage()
    {
        float healthPercentage = currentHealth / maxHealth;
        UpdateHealthBar(healthPercentage);
    }

    private void UpdateHealthBar(float healthPercentage)
    {
        healthBar.value = healthPercentage;
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }

}
