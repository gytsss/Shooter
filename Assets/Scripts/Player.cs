using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void LoseHealth(float health)
    {
        currentHealth -= health;

        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            //TODO: Fix - Hardcoded value
            transform.position = GameObject.Find("RespawnPoint").transform.position;
        }

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        //TODO: TP2 - SOLID
        float healthPercentage = currentHealth / maxHealth;
        healthBar.value = healthPercentage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO: Fix - Hardcoded value
        if (collision.gameObject.CompareTag("EnemyBullet"))
            LoseHealth(10);
    }
}
