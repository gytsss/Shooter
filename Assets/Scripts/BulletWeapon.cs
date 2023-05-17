using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BulletWeapon : Weapon
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private float bulletSpeed = 3000.0f;

    private bool fire;

    private void Update()
    {
        transform.localPosition = Vector3.zero;
    }

    public void OnFire()
    {
        Fire();
        fire = false;
    }
    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        Destroy(bullet, 0.5f);
    }

}
