using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class RaycastWeapon : Weapon
{
    private PlayerMovement input;

    private RaycastHit hit;
    private Ray ray;
    [SerializeField] float impactDuration = 1.0f;
    [SerializeField] float force = 30.0f;
    public GameObject impactEffect;

    [SerializeField] float damage = 10f;


    private void Start()
    {
        input = transform.root.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        transform.localPosition = Vector3.zero;

        ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));

        if(input.fire)
        {
            Fire();
            input.fire = false;
            
        }
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
                hit.rigidbody.AddForce(-hit.normal * force);
            }
        }
    }

}
