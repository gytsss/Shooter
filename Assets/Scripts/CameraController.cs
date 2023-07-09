using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages the camera movement based on player input, allowing for horizontal and vertical rotation.
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 sens;
    [SerializeField] private float gamepadSens = 100.0f;

    [SerializeField] private float minRotationX = -90.0f;
    [SerializeField] private float maxRotationX = 90.0f;

    [SerializeField] private GameObject weapon;
    Vector2 gamepadCameraAxis;

    /// <summary>
    /// Locks the cursor, sets the initial position and rotation of the camera.
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        transform.parent = player;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// Check if the magnitude of the gamepadCameraAxis axis is greater than zero. If it is, call the Rotate method.
    /// </summary>
    private void Update()
    {
        if (gamepadCameraAxis.magnitude > 0f)
        {
            Rotate(gamepadCameraAxis * gamepadSens * Time.deltaTime);
        }

    }

    /// <summary>
    /// Handles the player's look input to rotate the camera horizontally and vertically.
    /// </summary>
    /// <param name="value">The input value representing the mouse movement.</param>
    public void OnLook(InputValue value)
    {
        Vector2 mouseDelta = value.Get<Vector2>();

        Rotate(mouseDelta * sens * Time.deltaTime);

    }

    /// <summary>
    /// Handles the player's look input to rotate the camera horizontally and vertically with gamepad.
    /// </summary>
    public void OnGamepadLook(InputValue value)
    {
        gamepadCameraAxis = value.Get<Vector2>();

    }

    /// <summary>
    /// Handles the player's rotation logic.
    /// </summary>
    public void Rotate(Vector2 rotation)
    {
        if (Time.timeScale != 0f)
        {
            player.Rotate(Vector3.up * rotation.x);

            Vector3 eulers = transform.localRotation.eulerAngles;
            eulers.z = 0f;
            eulers.x -= rotation.y;
            eulers.x = ClampAngle(eulers.x, minRotationX, maxRotationX);

            transform.localRotation = Quaternion.Euler(eulers);
            weapon.transform.localRotation = Quaternion.Euler(eulers.x, 0f, 0f);
        }
    }

    /// <summary>
    /// ClampAngle is responsible for constraining an angle value within a specified range defined by the minimum and maximum values. 
    /// It first checks if the angle is greater than 180, and if so, it subtracts 360 from it
    /// </summary>
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle > 180)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}