using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class StaticEnemy : MonoBehaviour
{
    public static event Action Destroyed;
    [SerializeField] private Slider healthBar;
    [SerializeField] private int receivableBullets = 2;
    private int bulletCount = 0;
    public void TakeBullet()
    {
        bulletCount++;

        if (bulletCount >= receivableBullets)
        {
            Destroy(gameObject);
        }

        UpdateHealthBar();
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    public void UpdateHealthBar()
    {
        float healthPercentage = bulletCount / receivableBullets;
        healthBar.value = healthPercentage;
    }
}
