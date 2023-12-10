using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    #region INSTANCE

    public static BulletPool Instance { get; private set; }

    #endregion

    #region EXPOSED_FIELDS

    [SerializeField] private int initialSize = 150;
    [SerializeField] private BulletWeapon bulletWeapon;

    #endregion

    #region PRIVATE_FIELDS

    private Queue<GameObject>[] bullets;

    #endregion

    #region UNITY_CALLS

    /// <summary>
    /// Creates a queue for each bullet prefab
    /// </summary>
    private void Awake()
    {
        Instance = this;
        bullets = new Queue<GameObject>[bulletWeapon.bulletPrefabs.Length];

        for (int j = 0; j < bulletWeapon.bulletPrefabs.Length; j++)
        {
            bullets[j] = new Queue<GameObject>(initialSize / bulletWeapon.bulletPrefabs.Length);

            for (int i = 0; i < initialSize / bulletWeapon.bulletPrefabs.Length; i++)
            {
                GameObject bullet = BulletFactory.CreateBullet(bulletWeapon.bulletPrefabs[j],
                    transform.position, transform.rotation);
                bullet.SetActive(false);
                bullets[j].Enqueue(bullet);
            }
        }
    }

    #endregion

    #region PUBLIC_METHODS

    /// <summary>
    /// Return a bullet from the pool
    /// </summary>
    public GameObject GetBullet()
    {
        if (bullets[bulletWeapon.currentBullet].Count == 0)
        {
            GameObject bullet = BulletFactory.CreateBullet(bulletWeapon.bulletPrefabs[bulletWeapon.currentBullet],
                transform.position, transform.rotation);
            bullet.SetActive(false);
            bullets[bulletWeapon.currentBullet].Enqueue(bullet);
        }

        return bullets[bulletWeapon.currentBullet].Dequeue();
    }

    /// <summary>
    /// Return a bullet to the pool when finish his usage
    /// </summary>
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.rotation = Quaternion.identity;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }

        bullets[bulletWeapon.currentBullet].Enqueue(bullet);
    }

    #endregion
}