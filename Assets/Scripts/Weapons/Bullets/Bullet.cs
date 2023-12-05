using UnityEngine;
using UnityEngine.Animations;
/// <summary>
/// Fires a bullet from a weapon's bullet point, creates an impact effect upon collision with an object
/// </summary>
public partial class Bullet : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] float impactDuration = 1.0f;

    [SerializeField] GameObject impactEffect;

    /// <summary>
    /// Creates the impact effect, deals damage to the collided object, and destroys the bullet.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        CreateImpactEffect();
        DealDamage(collision.gameObject);
        DestroyBullet();
    }

    /// <summary>
    /// Creates the impact effect at the bullet's position and destroys it after a specified duration.
    /// </summary>
    private void CreateImpactEffect()
    {
        Quaternion rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        GameObject impactEffectGo = ImpactEffectFactory.CreateImpactEffect(impactEffect, transform.position, rotation);
        Destroy(impactEffectGo, impactDuration);
    }

    /// <summary>
    /// Deals damage to the collided object based on its type.
    /// </summary>
    private void DealDamage(GameObject collidedObject)
    {
        HealthComponent enemyHealth = collidedObject.GetComponent<HealthComponent>();
        //SoldierEnemy sEnemy = collidedObject.GetComponent<SoldierEnemy>();

        if (enemyHealth != null)
        {
            enemyHealth.DecreaseHealth(damage);
        }
        
    }

    /// <summary>
    /// Destroys the bullet game object.
    /// </summary>
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
