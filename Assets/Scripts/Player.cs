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

    private void Update()
    {

    }

    public void LoseHealth(float health)
    {
        currentHealth -= health;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / maxHealth;
        healthBar.value = healthPercentage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
            LoseHealth(10);
    }
}
