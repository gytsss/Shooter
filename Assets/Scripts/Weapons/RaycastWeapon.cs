using UnityEngine;

/// <summary>
/// Responsible for performing raycast-based weapon functionality in a game.
/// </summary>
public class RaycastWeapon : Weapon
{
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private float impactDuration = 1.0f;

    private Camera mainCamera;
    private RaycastHit hit;
    private Ray ray;

    /// <summary>
    /// Initializes the mainCamera variable with the reference to the main camera in the scene.
    /// </summary>
    private void Start()
    {
        mainCamera = Camera.main;
    }

    /// <summary>
    /// Updates the ray direction based on the center of the screen every frame.
    /// </summary>
    private void Update()
    {
        ray = mainCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
    }

    /// <summary>
    /// Performs a raycast from the main camera's viewport center, detects if it hits an object, and triggers corresponding actions based on the object's components.
    /// </summary>
    public override void OnFire()
    {
        if (enabled && isPlayerWeapon && !pauseController.IsGamePaused)
        {
            PlayShootSound();

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                RunEffect(hit);
                
                HealthComponent enemyHealthComponent = hit.collider.GetComponent<HealthComponent>();

                if (enemyHealthComponent != null)
                {
                    enemyHealthComponent.DecreaseHealth(damage);
                }
            }
        }
    }

    /// <summary>
    /// Performs a raycast from the enemy's position, detects if it hits an object, and triggers corresponding actions based on the object's components.
    /// </summary>
    public void OnEnemyFire(Transform enemyTransform)
    {
        if (enabled && !isPlayerWeapon)
        {
            ray = new Ray(enemyTransform.position, enemyTransform.forward);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                RunEffect(hit);

                if (hit.collider.TryGetComponent(out Player player))
                {
                    player.healthComponent.DecreaseHealth(damage);
                }
            }
        }
    }


    /// <summary>
    /// Runs bullet effects
    /// </summary>
    private void RunEffect(RaycastHit hit)
    {
        GameObject impactEffectGo = ImpactEffectFactory.CreateImpactEffect(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(impactEffectGo, impactDuration);
    }
    
}

