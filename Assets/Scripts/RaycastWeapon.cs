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
    public void OnFire()
    {
        if (enabled)
        {

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                FindObjectOfType<SoundManager>().Play("Shoot");
                RunEffect();

                if (hit.collider.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(damage);
                }
                else if (hit.collider.TryGetComponent(out StaticEnemy staticEnemy))
                {
                    staticEnemy.TakeDamage();
                }
            }
        }
    }

    /// <summary>
    /// Run bullet effects
    /// </summary>
    private void RunEffect()
    {
        GameObject impactEffectGo = Instantiate(impactEffect, hit.point, Quaternion.identity) as GameObject;

        Destroy(impactEffectGo, impactDuration);
    }

}

