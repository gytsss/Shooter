using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fires a fire bullet from a weapon's bullet point, creates an impact effect upon collision with an object
/// </summary>
public class FireBullets : MonoBehaviour
{
   [SerializeField] private GameObject fire;
   [SerializeField] private float fireDuration = 1.0f;
   [SerializeField] private float damagePerTick = 5f;
   [SerializeField] private float tickInterval = 0.5f;
   [SerializeField] private float maxTicks = 3f;
   [SerializeField] private float currentTicks = 0f;
   [SerializeField] private float timeToDestroyBullet = 3f;
   private Rigidbody rb;

    /// <summary>
    /// Gets the Rigidbody component attached to the current object using GetComponent<Rigidbody>() and saves it in the rb variable.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Checks if the currentTicks variable has reached the maxTicks value and destroys the game object if true. It also increments currentTicks.
    /// </summary>
    private void Update()
    {
        if (currentTicks >= maxTicks)
        {
            Destroy(gameObject);
        }

        currentTicks += Time.deltaTime;
    }

    /// <summary>
    /// Checks the tags of the collided objects and triggers the corresponding damage over time functions.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            return;
        }

        CreateFireEffect();

        HealthComponent enemyHealthComponent = collision.collider.GetComponent<HealthComponent>();
        if (enemyHealthComponent != null)
        {
            StartCoroutine(DamageEnemyOverTime(enemyHealthComponent));
        }
        

        DisablePhysics();

        Destroy(GetComponentInChildren<TrailRenderer>());

        StartCoroutine(DestroyBulletAfterDelay());
    }

    /// <summary>
    /// Disables bullet physics
    /// </summary>
    private void DisablePhysics()
    {
        rb.isKinematic = false;
        rb.velocity = transform.forward;
        rb.detectCollisions = false;
        rb.useGravity = true;
    }

    /// <summary>
    /// This coroutine function waits for a specified time, timeToDestroyBullet, and then destroys the game object.
    /// </summary>
    private IEnumerator DestroyBulletAfterDelay()
    {
        yield return new WaitForSeconds(timeToDestroyBullet);
        Destroy(gameObject);
    }

    /// <summary>
    /// This coroutine function applies damage to an enemy over time. It uses a loop to repeat the damage application for a specified duration. 
    /// If the enemy becomes null during the loop, the function will exit.
    /// </summary>
    private IEnumerator DamageEnemyOverTime(HealthComponent enemyHealth)
    {
        float elapsedTime = 0f;
        while (elapsedTime < maxTicks * tickInterval)
        {
            if (enemyHealth == null)
            {
                yield break;
            }

            enemyHealth.DecreaseHealth(damagePerTick);
            elapsedTime += tickInterval;
            yield return new WaitForSeconds(tickInterval);
        }
    }

    /// <summary>
    /// Creates the fire effect at the bullet's position and destroys it after a specified duration.
    /// </summary>
    private void CreateFireEffect()
    {
        GameObject impactEffectGo = ImpactEffectFactory.CreateImpactEffect(fire, transform.position, transform.rotation);
        Destroy(impactEffectGo, fireDuration);
    }
}
