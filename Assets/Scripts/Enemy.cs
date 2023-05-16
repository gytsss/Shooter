using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 50;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private NavMeshAgent enemy;

    private Transform player;
    private GameObject mapObject;

    //public LayerMask whatIsGround, whatIsPlayer;

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
        player = GameObject.Find("Player").transform;
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

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            isWalkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        Vector3 mapCenter = Vector3.zero;
        Vector3 mapSize = Vector3.one;

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

        RaycastHit hitInfo;

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
        direction.y = 0f; // Ignore the y component of the direction
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up); // Rotate towards the player's position

        if (!alreadyAttacked)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20f + transform.up, ForceMode.Impulse);
            Destroy(projectile, 1f);

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
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);


        Gizmos.color = Color.green;
        Gizmos.DrawSphere(walkPoint, 0.5f);
    }
}
