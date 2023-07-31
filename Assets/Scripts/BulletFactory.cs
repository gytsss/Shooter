using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages bullet creation
/// </summary>
public class BulletFactory
{
    /// <summary>
    /// Creates a new bullet instance
    /// </summary>
    public static GameObject CreateBullet(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
    {
        return Object.Instantiate(bulletPrefab, position, rotation);
    }
}
