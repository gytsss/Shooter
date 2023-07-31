using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory
{
    public static GameObject CreateBullet(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
    {
        return Object.Instantiate(bulletPrefab, position, rotation);
    }
}
