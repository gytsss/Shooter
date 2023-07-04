using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages the camera movement based on player input, allowing for horizontal and vertical rotation.
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float sens = 100.0f;

    [SerializeField] private float minRotationX = -90.0f;
    [SerializeField] private float maxRotationX = 90.0f;
  
    [SerializeField] private GameObject weapon;

    private float rotationX;

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
    /// Handles the player's look input to rotate the camera horizontally and vertically.
    /// </summary>
    ///// <param name="value">The input value representing the mouse movement.</param>
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