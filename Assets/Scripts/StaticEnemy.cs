using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticEnemy : MonoBehaviour
{
    public static event Action Destroyed;
    [SerializeField] private Slider healthBar;
    [SerializeField] private float maxHealth = 3f;
    private float currentHealth = 0;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        UpdateHealthBar();
    }

    public void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    public void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / maxHealth;
        healthBar.value = healthPercentage;
    }
}
