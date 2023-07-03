using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 currentMovement;

    [Header("Setup")]
    [SerializeField] private Rigidbody rigidBody;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private bool isJumpInput;

    private void FixedUpdate()
    {
        if (isJumpInput)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumpInput = false;
        }
        rigidBody.velocity = (transform.forward * currentMovement.y + transform.right * currentMovement.x) * speed + Vector3.up * rigidBody.velocity.y;
    }

    public void OnMove(InputValue input)
    {
        //TODO: Fix - Using Input related logic outside of an input responsible class
        var movement = input.Get<Vector2>();
        currentMovement = movement;

    }

    public void OnJump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.5f))
        {
            isJumpInput = true;
        }
    }

    public bool isMoving()
    {
        return currentMovement.magnitude > 0.001f;
    }

    public bool isJumping()
    {
        float distance = GetComponent<Collider>().bounds.extents.y + 0.1f;
        return Physics.Raycast(transform.position, Vector3.down, distance);
    }

}
