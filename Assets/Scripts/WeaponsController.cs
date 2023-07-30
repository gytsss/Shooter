using UnityEngine;
using System;

/// <summary>
/// Handles the control and management of weapons in the game. It incorporates functionality for picking up and equipping guns, as well as dropping them. 
/// </summary>
public class WeaponsController : MonoBehaviour
{
    public static event Action turnOffSprite;
    public static event Action turnOnSprite;

    private static int equippedWeapons = 0;
    [SerializeField] private bool slotFull;
    [SerializeField] private Weapon gunScript;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider coll;
    [SerializeField] private GameObject playerWeapon;
    [SerializeField] private Transform player, gunContainer, fpsCam;

    [SerializeField] private float pickUpRange;
    [SerializeField] private float dropForwardForce, dropUpwardForce;

    [SerializeField] private bool equipped;
    private float torqueRandomRange = 5f;


    /// <summary>
    ///Initializes the slotFull variable as false when the WeaponsController object is awakened.
    /// </summary>
    private void Awake()
    {
        slotFull = false;
    }

    /// <summary>
    /// Handles the initial state of the gun when the scene starts. Enables or disables components based on whether the gun is equipped or not.
    /// </summary>
    private void Start()
    {
        equippedWeapons = 0;

        if (!equipped)
        {
            DisableGun();
        }
        else if (equipped)
        {
            EnableGun();
        }
    }

    /// <summary>
    /// Moves the gun to the gun container position if it is equipped.
    /// </summary>
    private void Update()
    {
        WeaponsController playerWeaponController = playerWeapon.GetComponent<WeaponsController>();

        if (playerWeaponController.slotFull)
        {
            slotFull = true;
        }
        else
        {
            slotFull = false;
        }

        if (equipped)
        {
            transform.position = gunContainer.position;
        }

    }


    /// <summary>
    /// Called when the gun is picked up by the player. Equips the gun, sets it as a child of the gun container, and ensures its position and rotation are correct.
    /// </summary>
    public void OnPick()
    {
        Vector3 distanceToPlayer = player.position - transform.position;

        if (!equipped && distanceToPlayer.magnitude < pickUpRange && !slotFull && equippedWeapons < 1)
        {
            turnOnSprite?.Invoke();
            equipped = true;
            slotFull = true;

            equippedWeapons++;

            transform.SetParent(gunContainer);
            ResetTransform();

            DisablePhysics();

            gunScript.enabled = true;
        }
    }

    /// <summary>
    /// Handles the dropping of the gun. Resets the equipped status and unparents the gun object.
    /// </summary>
    public virtual void OnDrop()
    {
        if (equipped)
        {
            turnOffSprite?.Invoke();
            equipped = false;
            slotFull = false;

            equippedWeapons--;

            transform.SetParent(null);

            EnablePhysics();

            rb.velocity = player.GetComponent<Rigidbody>().velocity;

            rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
            rb.AddForce(fpsCam.forward * dropUpwardForce, ForceMode.Impulse);

            ApplyRandomTorque();

            gunScript.enabled = false;
        }
    }
    /// <summary>
    /// Disables gun script
    /// </summary>
    private void DisableGun()
    {
        
        gunScript.enabled = false;
        rb.isKinematic = false;
        coll.isTrigger = false;
    }

    /// <summary>
    /// Enables gun script
    /// </summary>
    protected void EnableGun()
    {
        
        gunScript.enabled = true;
        rb.isKinematic = true;
        coll.isTrigger = true;
        slotFull = true;
    }

    /// <summary>
    /// Resets gun transform
    /// </summary>
    private void ResetTransform()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    /// <summary>
    /// Disables gun physics
    /// </summary>
    private void DisablePhysics()
    {
        rb.isKinematic = true;
        coll.isTrigger = true;
    }

    /// <summary>
    /// Enables gun physics
    /// </summary>
    private void EnablePhysics()
    {
        rb.isKinematic = false;
        coll.isTrigger = false;
    }

    /// <summary>
    /// Apllies random torque when gun is droped
    /// </summary>
    private void ApplyRandomTorque()
    {
        float random = UnityEngine.Random.Range(-torqueRandomRange, torqueRandomRange);
        rb.AddTorque(new Vector3(random, random, random));
    }
}
