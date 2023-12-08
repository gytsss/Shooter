using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Weapon with instantiate bullets
/// </summary>
public class BulletWeapon : Weapon
{
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private Image bulletSprite;
    [SerializeField] private Color normalBulletColor = Color.white;
    [SerializeField] private Color explosiveBulletColor = Color.yellow;
    [SerializeField] private Color fireBulletColor = Color.red;
    [SerializeField] private Color poisonBulletColor = Color.green;
    [SerializeField] private float bulletSpeed = 3000.0f;

    private bool isNormalBulletActive = false;
    private bool isExplosiveBulletActive = false;
    private bool isFireBulletActive = false;
    private bool isPoisonBulletActive = false;
    
    public GameObject[] bulletPrefabs;
    public int currentBullet = 0;


    /// <summary>
    /// Subscribes to the Destroyed events of StaticEnemy and Enemy classes.
    /// </summary>
    private void Awake()
    {
        WeaponsController.turnOffSprite += TurnOffSprite;
        WeaponsController.turnOnSprite += TurnOnSprite;
    }

    /// <summary>
    /// Fires the bullet from the weapon's bullet point.
    /// </summary>
    public override void OnFire()
    {
        if (enabled && !pauseController.IsGamePaused)
        {
            PlayShootSound();
            GameObject bullet = BulletPool.Instance.GetBullet();
            bullet.transform.position = bulletPoint.transform.position;
            bullet.transform.rotation = bulletPoint.transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);

        }
    }

    /// <summary>
    /// Changes bullet type
    /// </summary>
    public void OnChangeBullet()
    {
        if (enabled)
        {
            PlayReloadSound();

            isNormalBulletActive = false;
            isExplosiveBulletActive = false;
            isFireBulletActive = false;
            isPoisonBulletActive = false;

            currentBullet = (currentBullet + 1) % bulletPrefabs.Length;

            switch (currentBullet)
            {
                case 0:
                    isNormalBulletActive = true;
                    break;
                case 1:
                    isExplosiveBulletActive = true;
                    break;
                case 2:
                    isFireBulletActive = true;
                    break;
                case 3:
                    isPoisonBulletActive = true;
                    break;
            }
            ChangeColor();
        }
    }

    /// <summary>
    /// Changes bullet sprite color
    /// </summary>
    private void ChangeColor()
    {
        if (isNormalBulletActive)
            bulletSprite.color = normalBulletColor;
        if (isExplosiveBulletActive)
            bulletSprite.color = explosiveBulletColor;
        if (isFireBulletActive)
            bulletSprite.color = fireBulletColor;
        if (isPoisonBulletActive)
            bulletSprite.color = poisonBulletColor;

    }

    /// <summary>
    /// Reduces the count of remaining enemies.
    /// </summary>
    private void TurnOffSprite()
    {
        bulletSprite.enabled = false;
    }

    /// <summary>
    /// Reduces the count of remaining enemies.
    /// </summary>
    private void TurnOnSprite()
    {
        bulletSprite.enabled = true;
    }

    /// <summary>
    /// Unsubscribes from the Destroyed events of StaticEnemy and Enemy classes.
    /// </summary>
    private void OnDestroy()
    {
        WeaponsController.turnOffSprite -= TurnOffSprite;
        WeaponsController.turnOnSprite -= TurnOnSprite;

    }
}
