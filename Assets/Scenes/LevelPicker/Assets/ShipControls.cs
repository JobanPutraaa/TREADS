using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ShipControls : MonoBehaviour
{
    public float speed;  // Movement speed
    private Vector2 move;  // Input for movement
    private Vector3 lastDirection;  // Tracks last movement direction

    private Rigidbody rb;

    // Input system method to capture movement input
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Prevents Rigidbody from rotating unexpectedly

        lastDirection = Vector3.forward;  // Initial direction
    }

    void Update()  // Use FixedUpdate for physics-related actions
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);  // Convert input to a 3D vector

        // Check if there's movement input
        if (movement.magnitude > 0.1f)
        {
            lastDirection = movement;  // Store last movement direction

            // Rotate smoothly towards the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(lastDirection);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, 0.15f));  // Smooth rotation
        }

        // Apply movement to the Rigidbody using MovePosition
        Vector3 newPosition = rb.position + movement.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);  // Moves the player using Rigidbody physics
    }
}