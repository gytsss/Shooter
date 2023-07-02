using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class RaycastWeapon : Weapon
{
    //TODO: Fix - Declare this at method level

    [SerializeField] private GameObject impactEffect;
    [SerializeField] private float impactDuration = 1.0f;
    [SerializeField] private float damage = 10f;

    private Camera mainCamera;
    private RaycastHit hit;
    private Ray ray;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        ray = mainCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
    }

    public void OnFire()
    {
        if (enabled)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject impactEffectGo = Instantiate(impactEffect, hit.point, Quaternion.identity) as GameObject;
                //TODO: TP2 - SOLID
                Destroy(impactEffectGo, impactDuration);

                if (hit.collider.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(damage);
                }
                else if (hit.collider.TryGetComponent(out StaticEnemy staticEnemy))
                {
                    staticEnemy.TakeBullet();
                }

            }
        }
    }

}
