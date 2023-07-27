using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Weapon with instantiate bullets
/// </summary>
public class BulletWeapon : Weapon
{
    [SerializeField] private GameObject[] bulletPrefabs;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private float bulletSpeed = 3000.0f;


    [SerializeField] private Image bulletSprite;

    [SerializeField] private Color normalBulletColor = Color.white;
    [SerializeField] private Color explosiveBulletColor = Color.yellow;
    [SerializeField] private Color fireBulletColor = Color.red;
    [SerializeField] private Color poisonBulletColor = Color.green;

    private bool isNormalBulletActive = false;
    private bool isExplosiveBulletActive = false;
    private bool isFireBulletActive = false;
    private bool isPoisonBulletActive = false;
    private int currentBullet = 0;

    /// <summary>
    /// Fires the bullet from the weapon's bullet point.
    /// </summary>
    public void OnFire()
    {
        if (enabled)
        {
            GameObject bullet = Instantiate(bulletPrefabs[currentBullet], bulletPoint.transform.position, bulletPoint.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);

        }
    }

    /// <summary>
    /// Change bullet type
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

            if (currentBullet == 0)
                isNormalBulletActive = true;
            if (currentBullet == 1)
                isExplosiveBulletActive = true;
            if (currentBullet == 2)
                isFireBulletActive = true;
            if (currentBullet == 3)
                isPoisonBulletActive = true;

            ChangeColor();
        }
    }

    /// <summary>
    /// Change bullet sprite color
    /// </summary>
    private void ChangeColor()
    {
        if(isNormalBulletActive)
            bulletSprite.color = normalBulletColor;
        if(isExplosiveBulletActive)
            bulletSprite.color = explosiveBulletColor;
        if(isFireBulletActive)
            bulletSprite.color = fireBulletColor;
        if(isPoisonBulletActive)
            bulletSprite.color = poisonBulletColor;

    }

    /// <summary>
    /// Play reload sound
    /// </summary>
    private void PlayReloadSound()
    {
        FindObjectOfType<SoundManager>().Play("Reload");
    }
}
