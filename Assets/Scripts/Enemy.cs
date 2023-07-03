using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private Transform respawnPoint;
    private float currentHealth;

    private Transform player;
    private GameObject mapObject;

    [Header("Patroling")]
    [SerializeField] private Vector3 walkPoint;
    [SerializeField] private float walkPointRange;

    [Header("Attack")]
    [SerializeField] private float attackCooldown;
    private bool alreadyAttacked;

    [Header("States")]
    [SerializeField] private float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;
    private float distanceToPlayer;
    private const float distanceMargin = .1f;


    private void Awake()
    {
        //TODO: TP2 - Factory Method
        player = GameObject.Find("Player").transform;
        currentHealth = maxHealth;
    }

    private void Start()
    {
        currentState = EnemyState.Patrol;
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
        if (enemy.remainingDistance < distanceMargin)
        {
            walkPoint = GetNewRandomPointInsideBounds();
            enemy.SetDestination(walkPoint);
        }
    }

    private Vector3 GetNewRandomPointInsideBounds()
    {
        Vector3 mapCenter = Vector3.zero;
        Vector3 mapSize = Vector3.one;


        //TODO: Fix - Cache value/s
        mapObject = GameObject.Find("Room");

        mapObject = GameObject.Find("Floor Tile");

        if (mapObject != null)
        {
            Renderer mapRenderer = mapObject.GetComponent<Renderer>();
            if (mapRenderer != null)
            {
                mapCenter = mapRenderer.bounds.center;
                mapSize = mapRenderer.bounds.size;
            }
        }

        float randomX = UnityEngine.Random.Range(-mapSize.x / 2.1f, mapSize.x / 2.1f);
        float randomZ = UnityEngine.Random.Range(-mapSize.z / 2.1f, mapSize.z / 2.1f);
        float randomY = UnityEngine.Random.Range(-mapSize.y / 2f, mapSize.y / 2f);

        return new Vector3(mapCenter.x + randomX, mapCenter.y + randomY, mapCenter.z + randomZ);

    }

    private void ChasePlayer()
    {
        enemy.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        enemy.SetDestination(transform.position);

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        if (!alreadyAttacked)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20f + transform.up, ForceMode.Impulse);

            //TODO: TP2 - SOLID
            Destroy(projectile, 0.5f);

            //TODO: Fix - Could be a coroutine
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            transform.position = respawnPoint.position;
        }
        UpdateHealthBar();
    }

    //TODO: TP2 - SOLID
    public void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / maxHealth;
        healthBar.value = healthPercentage;
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(walkPoint, 1);
    }

}
