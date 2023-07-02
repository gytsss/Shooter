using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    [SerializeField] private int receivableBullets = 2;
    private int bulletCount = 0;
    public void TakeDamage()
    {
        bulletCount++;

        if (bulletCount >= receivableBullets)
            Destroy(gameObject);
    }
}
