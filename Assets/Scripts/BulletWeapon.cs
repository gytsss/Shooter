using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletWeapon : Weapon
{
    private PlayerMovement input;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private float bulletSpeed = 3000.0f;

    private void Start()
    {
        input = transform.root.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        transform.localPosition = Vector3.zero;

        if (input.fire)
        {
            Fire();
            input.fire = false;
        }

    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        Destroy(bullet, 0.5f);
    }

}
