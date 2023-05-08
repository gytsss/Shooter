using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    public GameObject gun; // The game object that represents the gun

    private Transform hand; // The transform component of the hand (or wherever you want to hold the gun)

    private void Start()
    {
        hand = GetComponent<Transform>(); // Get the transform component of the hand
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gun")) // Check if the player collided with an object tagged as a gun
        {
            gun = other.gameObject; // Save the reference to the gun game object
            gun.transform.parent = hand; // Set the gun's parent to the hand
            gun.transform.localPosition = Vector3.zero; // Move the gun to the position of the hand
            gun.transform.localRotation = Quaternion.identity; // Set the gun's rotation to the identity quaternion
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && gun != null) // Check if the player pressed the space bar and if they're holding a gun
        {
            gun.transform.parent = null; // Unparent the gun from the hand
            gun = null; // Reset the reference to the gun
        }
    }
}
