using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    public static event Action Destroyed;
    [SerializeField] private int receivableBullets = 2;
    private int bulletCount = 0;
    public void TakeBullet()
    {
        bulletCount++;

        if (bulletCount >= receivableBullets)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }
}
