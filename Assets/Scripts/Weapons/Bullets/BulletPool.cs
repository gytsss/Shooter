using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    [SerializeField] private int initialSize = 150;
    [SerializeField] private BulletWeapon bulletWeapon;

    private Queue<GameObject>[] bullets;

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
}