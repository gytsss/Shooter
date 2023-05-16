using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 50;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private NavMeshAgent enemy;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

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

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //if (!playerInSightRange && !playerInAttackRange)
            Patroling();

        //if (playerInSightRange && !playerInAttackRange)
        //    ChasePlayer();

        //if (playerInSightRange && playerInAttackRange) { }
        //AttackPlayer();
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
        //float randomZ = Random.Range(-walkPointRange, walkPointRange);
        //float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        isWalkPointSet = true;
    }

    private void ChasePlayer()
    {
        enemy.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        enemy.SetDestination(transform.position);

        transform.LookAt(player);

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
    }
}
