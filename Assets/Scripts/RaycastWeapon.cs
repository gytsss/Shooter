using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : Weapon
{
    private PlayerMovement input;

    private RaycastHit hit;
    private Ray ray;
    [SerializeField] float impactDuration = 1.0f;

    public int damage = 10;

    public GameObject impactEffect;

    private void Start()
    {
        input = transform.root.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        transform.localPosition = Vector3.zero;

        ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));

        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject impactEffectGo = Instantiate(impactEffect, hit.point, Quaternion.identity) as GameObject;
                Destroy(impactEffectGo, impactDuration);

                if(hit.collider.gameObject.tag == "Enemy")
                {
                    Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(damage);
                }
            }
        }
    }

}
