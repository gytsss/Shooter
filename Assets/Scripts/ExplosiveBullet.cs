using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///Fires a explosive bullet from a weapon's bullet point, creates an impact effect upon collision with an object
/// </summary>
public class ExplosiveBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject explosion;

    [Range(0f, 1f)]
    [SerializeField] private float bounciness;
    [SerializeField] private bool usegravity;

    [SerializeField] private int explosionDamage;
    [SerializeField] private float explosionRange;
    [SerializeField] private float explosionDuration = 1.0f;
    [SerializeField] private float explosionForce = 1.0f;

    [SerializeField] private int maxCollisions;
    [SerializeField] private float maxLifeTime;
    [SerializeField] private bool explodeOnTouch = true;

    private int collisions;
    private PhysicMaterial physicsMat;

    /// <summary>
    /// Call the Setup physics method
    /// </summary>
    private void Start()
    {
        Setup();
    }

    /// <summary>
    /// Check if the bullet would explode
    /// </summary>
    private void Update()
    {
        if (collisions > maxCollisions)
            Explode();

        maxLifeTime -= Time.deltaTime;
        if (maxLifeTime <= 0)
            Explode();
    }

    /// <summary>
    /// Creates the impact effect, explode the bullet
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        collisions++;

        if (collision.collider.CompareTag("Bullet"))
            return;
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("StaticEnemy") && explodeOnTouch)
            Explode();
        Explode();
    }

    /// <summary>
    /// Sets up bullets physics
    /// </summary>
    private void Setup()
    {
        physicsMat = new PhysicMaterial();
        physicsMat.bounciness = bounciness;
        physicsMat.frictionCombine = PhysicMaterialCombine.Minimum;
        physicsMat.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<CapsuleCollider>().material = physicsMat;

        rb.useGravity = usegravity;
    }

    /// <summary>
    /// Explodes the bullet and check which enemy take damage
    /// </summary>
    private void Explode()
    {
        if (explosion != null)
            CreateExplosionEffect();


        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange);

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponent<SoldierEnemy>())
            {
                enemies[i].GetComponent<SoldierEnemy>().TakeDamage(explosionDamage);
            }
            else if (enemies[i].CompareTag("Enemy"))
                enemies[i].GetComponent<Enemy>().TakeDamage(explosionDamage);

            if (enemies[i].CompareTag("StaticEnemy"))
                enemies[i].GetComponent<StaticEnemy>().TakeDamage();

            if (enemies[i].GetComponent<Rigidbody>())
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);

        }

        Invoke("Delay", 0.05f);
    }

    /// <summary>
    /// Little delay in order to destroy the bullet
    /// </summary>
    private void Delay()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Creates the explosion effect at the bullet's position and destroys it after a specified duration.
    /// </summary>
    private void CreateExplosionEffect()
    {
        GameObject impactEffectGo = ImpactEffectFactory.CreateImpactEffect(explosion, transform.position, transform.rotation);
        Destroy(impactEffectGo, explosionDuration);
    }

}