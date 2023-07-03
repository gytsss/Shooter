using UnityEngine;

/// <summary>
/// Handles the control and management of weapons in the game. It incorporates functionality for picking up and equipping guns, as well as dropping them. 
/// </summary>
public class WeaponsController : MonoBehaviour
{
    [SerializeField] private static bool slotFull;
    [SerializeField] private Weapon gunScript;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider coll;
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
    /// Moves the gun to the gun container position if it is equipped.
    /// </summary>
    private void Update()
    {
        if (equipped)
        {
            transform.position = gunContainer.position;
        }
    }

    /// <summary>
    /// Handles the initial state of the gun when the scene starts. Enables or disables components based on whether the gun is equipped or not.
    /// </summary>
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

    /// <summary>
    /// Called when the gun is picked up by the player. Equips the gun, sets it as a child of the gun container, and ensures its position and rotation are correct.
    /// </summary>
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

    /// <summary>
    /// Handles the dropping of the gun. Resets the equipped status and unparents the gun object.
    /// </summary>
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

            float random = Random.Range(-torqueRandomRange, torqueRandomRange);
            rb.AddTorque(new Vector3(random, random, random));

            gunScript.enabled = false;
        }
    }
}
