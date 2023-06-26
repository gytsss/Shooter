using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    //TODO: Fix - Unclear logic
    private void OnTriggerEnter(Collider other)
    {
        //TODO: Fix - Hardcoded value
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
