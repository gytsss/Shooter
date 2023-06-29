using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private Weapon gunScript;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider coll;
    [SerializeField] private Transform player, gunContainer, fpsCam;

    [SerializeField] private float pickUpRange;
    [SerializeField] private float dropForwardForce, dropUpwardForce;

    [SerializeField] private bool equipped;
    //TODO: TP2 - Syntax - Fix declaration order
    [SerializeField] private static bool slotFull;

    private void Awake()
    {
        slotFull = false;
    }

    private void Start()
    {
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        else if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }


    public void OnPick()
    {
        Vector3 distanceToPlayer = player.position - transform.position;

        if (!equipped && distanceToPlayer.magnitude < pickUpRange && !slotFull)
        {

            equipped = true;
            slotFull = true;

            transform.SetParent(gunContainer);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            transform.localScale = Vector3.one;

            rb.isKinematic = true;
            coll.isTrigger = true;

            gunScript.enabled = true;
        }
    }
    public void OnDrop()
    {
        if (equipped)
        {
            equipped = false;
            slotFull = false;

            transform.SetParent(null);

            rb.isKinematic = false;
            coll.isTrigger = false;

            rb.velocity = player.GetComponent<Rigidbody>().velocity;

            rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
            rb.AddForce(fpsCam.forward * dropUpwardForce, ForceMode.Impulse);

            //TODO: Fix - Hardcoded value
            float random = Random.Range(-5f, 5f);
            rb.AddTorque(new Vector3(random, random, random));

            gunScript.enabled = false;
        }
    }
}
