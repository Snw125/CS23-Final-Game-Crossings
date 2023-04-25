using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSpots : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 movement;
    public float distance = 1f; // distance between spots
    public List<Vector3> spots = new List<Vector3>(); // list of positions to follow
    public List<Transform> immigrants = new List<Transform>(); // list of immigrant objects to follow

    private Rigidbody2D rb;
    private bool isMoving = false; // Flag indicating if the player is currently moving
    private bool isLeavingSpots = false; // Flag indicating if the player is currently leaving spots
    public Animator anim;
    public PlayerInteractions interact;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        interact = GetComponent<PlayerInteractions>();
    }

    void FixedUpdate()
    {
        // Move the player object if the player is leaving spots
        if (isLeavingSpots)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

            // Add the current position to the list of spots
            spots.Add(transform.position);
        }
        // Otherwise, move the player object normally
        else
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        // Check if the player is moving
        if (movement.magnitude > 0)
        {
            // Update the flag indicating that the player is moving
            isMoving = true;

            // Start leaving spots if the X key has been pressed
            if (isLeavingSpots)
            {
                // Add the current position to the list of spots
                spots.Add(transform.position);
            }
        }
        else
        {
            // Update the flag indicating that the player is not moving
            isMoving = false;
        }
        
        // Check if the player is close enough to an immigrant object to start following it
        foreach (Transform immigrant in immigrants)
        {
            if (Vector3.Distance(transform.position, immigrant.position) <= distance && Input.GetKeyDown(KeyCode.X))
            {
                // Add the current position to the list of spots for the immigrant object
                immigrant.GetComponent<PlayerMovementSpots>().spots = new List<Vector3>(spots);
                immigrant.GetComponent<PlayerMovementSpots>().isLeavingSpots = true;
            }
        }
    }

    void Update()
    {
        if (interact.hidden == true)
        {
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}