using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSpots : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 movement;
    public List<Vector3> spots = new List<Vector3>(); // list of positions to follow

    private Rigidbody2D rb;
    private bool isMoving = false; // Flag indicating if the player is currently moving
    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spots.Add(transform.position);
        anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        // Move the player object
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Check if the player is moving
        if (movement.magnitude > 0)
        {
            // Update the flag indicating that the player is moving
            isMoving = true;

            // Add the current position to the list of spots
            spots.Add(transform.position);
        }
        else
        {
            // Update the flag indicating that the player is not moving
            isMoving = false;
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
