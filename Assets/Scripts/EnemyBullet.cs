using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnCollisionEnter()
    {
        Destroy(gameObject, .5f);
    }
}
