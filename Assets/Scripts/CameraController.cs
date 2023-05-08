using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float sens = 100.0f;

    [SerializeField] private float minRotationX = -90.0f;
    [SerializeField] private float maxRotationX = 90.0f;

    [SerializeField] private GameObject weapon;

    private float rotationX;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Set camera position to player pos
        transform.parent = player;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    void Update()
    {
        // Rotate player based on keyboard input
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens* Time.deltaTime;

        rotationX += mouseY;
        rotationX = Mathf.Clamp(rotationX, minRotationX, maxRotationX);

        player.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);
        
        weapon.transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);
    }

}