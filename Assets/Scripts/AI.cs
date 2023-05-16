using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public NavMeshAgent enemy;

    public Transform player;

    private void Start()
    {
     
    }

    //private void Update()
    //{
    //    // Calculate the direction to the player
    //    Vector3 direction = player.position - transform.position;

    //    // Ignore the y component of the direction (so the enemy only rotates on the y-axis)
    //    direction.y = 0f;

    //    // Create a rotation that faces the direction
    //    Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

    //    // Apply the rotation to the enemy's transform
    //    transform.rotation = targetRotation;

    //    // Set the enemy's destination to the player's position
    //    enemy.SetDestination(player.position);
    //}
}
