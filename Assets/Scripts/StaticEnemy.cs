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

        CalculateHealthPercentage();
    }

    public void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    private void CalculateHealthPercentage()
    {
        float healthPercentage = currentHealth / maxHealth;
        UpdateHealthBar(healthPercentage);
    }

    private void UpdateHealthBar(float healthPercentage)
    {
        healthBar.value = healthPercentage;
    }
}
