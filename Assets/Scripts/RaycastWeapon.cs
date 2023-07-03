using UnityEngine;

/// <summary>
/// Responsible for performing raycast-based weapon functionality in a game.
/// </summary>
public class RaycastWeapon : Weapon
{
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private float impactDuration = 1.0f;
    [SerializeField] private float damage = 10f;

    private Camera mainCamera;
    private RaycastHit hit;
    private Ray ray;
    private IEnemyDamage enemyDamage;

    /// <summary>
    /// Initializes the mainCamera and the enemyDamage component.
    /// </summary>
    private void Start()
    {
        mainCamera = Camera.main;
        enemyDamage = GetComponent<IEnemyDamage>(); 
    }

    /// <summary>
    /// Sets the ray variable to a ray cast from the mainCamera's viewport center.
    /// </summary>
    private void Update()
    {
        ray = mainCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
    }

    /// <summary>
    /// Executes when the weapon is fired. Instantiates an impact effect at the hit point and applies damage to any enemy hit.
    /// </summary>
    public void OnFire()
    {
        if (enabled)
        {
            
            GameObject impactEffect = this.impactEffect;
            float impactDuration = this.impactDuration;
            float damage = this.damage;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject impactEffectGo = Instantiate(impactEffect, hit.point, Quaternion.identity) as GameObject;
                Destroy(impactEffectGo, impactDuration);

                var enemy = hit.collider.GetComponent<IEnemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage, enemyDamage);
                }
            }
        }
    }

    /// <summary>
    ///Interface for dealing damage to enemies.
    /// </summary>
    public interface IEnemyDamage
    {
        void DealDamage(float damage);
    }

    /// <summary>
    /// Interface for enemies that can take damage from the weapon.
    /// </summary>
    public interface IEnemy
    {
        void TakeDamage(float damage, IEnemyDamage enemyDamage);
    }
}

