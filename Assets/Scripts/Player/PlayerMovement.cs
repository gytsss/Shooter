using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles player movement, including horizontal and vertical movement, jumping, and checking if the player is currently moving or jumping.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private Vector3 currentMovement;

    [Header("Setup")]
    [SerializeField] private Rigidbody rigidBody;

    [Header("Movement")]
    [SerializeField] public float speed;
    [SerializeField] private float jumpForce;

    private bool isJumpInput;

    /// <summary>
    /// Fixed update method that applies movement and jumping to the player's rigidbody based on input and current movement values.
    /// </summary>
    private void FixedUpdate()
    {
        if (isJumpInput)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumpInput = false;
        }

        rigidBody.velocity = (transform.forward * currentMovement.y + transform.right * currentMovement.x) * speed + Vector3.up * rigidBody.velocity.y;
    }

    /// <summary>
    /// Updates the currentMovement vector based on input received.
    /// </summary>
    public void OnMove(InputValue input)
    {
       
        var movement = input.Get<Vector2>();
        currentMovement = movement;

    }

    /// <summary>
    /// Checks if the player is grounded and sets the isJumpInput flag to true if they are.
    /// </summary>
    public void OnJump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.5f))
        {
            isJumpInput = true;
        }
    }

    /// <summary>
    /// Returns true if the player is currently moving, based on the magnitude of the currentMovement vector.
    /// </summary>
    public bool isMoving()
    {
        return currentMovement.magnitude > 0.001f;
    }

    /// <summary>
    /// Returns true if the player is currently jumping, based on a raycast to check if there is ground below the player.
    /// </summary>
    public bool isJumping()
    {
        float distance = GetComponent<Collider>().bounds.extents.y + 0.1f;
        return Physics.Raycast(transform.position, Vector3.down, distance);
    }

}
