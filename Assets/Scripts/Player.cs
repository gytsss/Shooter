using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float enemyDamage = 10;
    [SerializeField] private string enemyBulletTag = "EnemyBullet";
    [SerializeField] private Transform respawnPoint;
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
            transform.position = respawnPoint.position;
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
        if (collision.gameObject.CompareTag(enemyBulletTag))
            LoseHealth(enemyDamage);
    }
}
