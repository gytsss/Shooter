using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] float impactDuration = 1.0f;

    public GameObject impactEffect;

    private void OnCollisionEnter(Collision collision)
    {
        //TODO: TP2 - SOLID
        GameObject impactEffectGo = Instantiate(impactEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(impactEffectGo, impactDuration);

        if (collision.gameObject.GetComponent<Enemy>())
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
    }
}
