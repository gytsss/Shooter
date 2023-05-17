using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = transform.position;
        }
        else if (other.CompareTag("Enemy"))
        {
            other.transform.position = transform.position;
        }
    }
}
