using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    private Transform player;
    private GameObject mapObject;

    //TODO: Fix - Should be header
    //Patroling
    [SerializeField] private Vector3 walkPoint;
    private bool isWalkPointSet;
    [SerializeField] private float walkPointRange;

    //Attack
    [SerializeField] private float attackCooldown;
    private bool alreadyAttacked;

    //States
    [SerializeField] private float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;
    private float distanceToPlayer;

    private void Awake()
    {
        //TODO: TP2 - Factory Method
        player = GameObject.Find("Player").transform;
        currentHealth = maxHealth;
    }

    //TODO: TP2 - Remove unused methods/variables/classes
    private void Start()
    {
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

        //TODO: TP2 - FSM
        if (!playerInSightRange && !playerInAttackRange)
            Patroling();

        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();

        if (playerInSightRange && playerInAttackRange)
            AttackPlayer();
    }

    private void Patroling()
    {
        if (!isWalkPointSet)
        {
            SearchWalkPoint();
        }
        else if (isWalkPointSet)
        {
            enemy.SetDestination(walkPoint);
        }

        //TODO: Fix - enemy.RemainingDistance
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //TODO: Fix - It's not recommendable to edit class level variables all around your code (which is called "side effects")
        if (distanceToWalkPoint.magnitude < 1f)
            isWalkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        Vector3 mapCenter = Vector3.zero;
        Vector3 mapSize = Vector3.one;

        //TODO: Fix - Cache value/s
        mapObject = GameObject.Find("Room");

        if (mapObject != null)
        {
            Renderer mapRenderer = mapObject.GetComponent<Renderer>();
            if (mapRenderer != null)
            {
                mapCenter = mapRenderer.bounds.center;
                mapSize = mapRenderer.bounds.size;
            }
        }

        float randomX = Random.Range(-mapSize.x / 2.1f, mapSize.x / 2.1f);
        float randomZ = Random.Range(-mapSize.z / 2.1f, mapSize.z / 2.1f);
        walkPoint = new Vector3(mapCenter.x + randomX, transform.position.y, mapCenter.z + randomZ);

        //TODO: TP2 - Remove unused methods/variables/classes
        RaycastHit hitInfo;

        //TODO: Fix - Unclear logic - If you're trying to figure if the enemy is able to walk to that point, navmesh has functionality meant for that.
        if (Physics.Raycast(walkPoint, Vector3.down, out hitInfo, 2f) || Physics.Raycast(walkPoint, Vector3.up, out hitInfo, 2f))
        {
            SearchWalkPoint();
            return;
        }

        isWalkPointSet = true;
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
            Destroy(projectile, 1f);

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
            //TODO: Fix - Hardcoded value
            transform.position = GameObject.Find("EnemyRespawnPoint").transform.position;
            transform.rotation = GameObject.Find("EnemyRespawnPoint").transform.rotation;
        }
        UpdateHealthBar();
    }

    //TODO: TP2 - SOLID
    public void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / maxHealth;
        healthBar.value = healthPercentage;
    }

}
