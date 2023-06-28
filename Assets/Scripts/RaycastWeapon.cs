using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class RaycastWeapon : Weapon
{
    //TODO: TP2 - Syntax - Fix declaration order
    //TODO: Fix - Declare this at method level
    private RaycastHit hit;
    private Ray ray;

    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)

    [SerializeField] float impactDuration = 1.0f;
    public GameObject impactEffect;
    [SerializeField] private float impactForce = 30f;
    [SerializeField] float damage = 10f;

    private void Update()
    {
        transform.localPosition = Vector3.zero;

        //TODO: Fix - Cache value/s
        ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));

    }

    public void OnFire()
    {
        if (enabled)
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            GameObject impactEffectGo = Instantiate(impactEffect, hit.point, Quaternion.identity) as GameObject;
            //TODO: TP2 - SOLID
            Destroy(impactEffectGo, impactDuration);

            //TODO: Fix - TryGetComponent
            if (hit.collider.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
            }
            //TODO: Fix - Hardcoded value
            else if (hit.collider.gameObject.CompareTag("Cube"))
            {
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
            }
        }
    }
}
