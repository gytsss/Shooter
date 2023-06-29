using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BulletWeapon : Weapon
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private float bulletSpeed = 3000.0f;

    //TODO: Fix - Unclear logic

    private void Update()
    {
        transform.localPosition = Vector3.zero;
    }

    public void OnFire()
    {

        //TODO: Fix - Why separate these methods?
        Fire();

        if (enabled)
        {
            Fire();
        }
    }
    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        //TODO: TP2 - SOLID
        Destroy(bullet, 0.5f);
    }

}
