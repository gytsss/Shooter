using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class RaycastWeapon : Weapon
{
    private RaycastHit hit;
    private Ray ray;
    [SerializeField] float impactDuration = 1.0f;
    public GameObject impactEffect;
    [SerializeField] private float impactForce = 30f;
    [SerializeField] float damage = 10f;

    

    private void Update()
    {
        transform.localPosition = Vector3.zero;

        ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
    }

    public void OnFire()
    {
        Fire();
        
    }

    private void Fire()
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            GameObject impactEffectGo = Instantiate(impactEffect, hit.point, Quaternion.identity) as GameObject;
            Destroy(impactEffectGo, impactDuration);

            if (hit.collider.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
            }
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
