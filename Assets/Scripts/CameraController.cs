using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

        //TODO: Fix - Remove redundant comments
        // Set camera position to player pos
        transform.parent = player;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    //TODO: TP2 - Remove unused methods/variables/classes
    void Update()
    {
    }

    //TODO: Fix - Using Input related logic outside of an input responsible class
    public void OnLook(InputValue value)
    {
        Vector2 mouseDelta = value.Get<Vector2>();

        float mouseX = mouseDelta.x * sens * Time.deltaTime;
        float mouseY = mouseDelta.y * sens * Time.deltaTime;

        rotationX += mouseY;
        rotationX = Mathf.Clamp(rotationX, minRotationX, maxRotationX);

        player.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);

        weapon.transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);
    }
}