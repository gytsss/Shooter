using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DroneEnemy : MonoBehaviour
{
    private StaticEnemy staticEnemy;
    [SerializeField] private Transform player;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float shootingRange = 10f;
    [SerializeField] private float visionRange = 10f;
    [SerializeField] private float shootingDamage = 10f;
    [SerializeField] private float laserDuration = .05f;
    [SerializeField] private float fireRate = .2f;

    private float fireTimer;
    private LineRenderer laserLine;

    private void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;

        if (IsPlayerInVisionRange() && fireTimer > fireRate)
        {
            fireTimer = 0;
            AttackPlayer();
        }
    }

    public void TakeBullet()
    {
        staticEnemy.TakeDamage();
    }

    public void AttackPlayer()
    {
        
        laserLine.SetPosition(0, rayPoint.position);

        Vector3 direction = player.position - transform.position;
        Ray ray = new Ray(transform.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, shootingRange))
        {
            if (hit.collider.TryGetComponent(out Player player))
            {
                laserLine.SetPosition(1, hit.point);
                player.LoseHealth(shootingDamage);
            }
        }

        StartCoroutine(ShootLaser());

    }

    private bool IsPlayerInVisionRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= visionRange;
    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled= false;
    }

}
