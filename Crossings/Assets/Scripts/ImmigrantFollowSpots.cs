using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmigrantFollowSpots : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    public float moveSpeed = 5.0f; // The speed at which the object moves
    public float followDistance = 1.0f; // The distance the object should try to stay behind the player
    public float stopDistance = 0.5f; // The distance at which the object should stop following the player
    public float interpolationSpeed = 5.0f; // The speed at which to interpolate between spots

    private List<Vector3> spots; // The list of spots left by the player
    private int currentSpotIndex; // The index of the current spot being followed
    private Vector3 targetPosition; // The position the object is moving towards

    public Animator anim;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        // Set the current spot index to the oldest spot
        currentSpotIndex = 0;

        // Get the list of spots from the player object
        PlayerMovementSpots playerController = player.GetComponent<PlayerMovementSpots>();
        spots = playerController.spots;

        // Add the current player position as the first spot in the list
        spots.Insert(0, player.transform.position);

        // Set the initial target position to the current spot
        targetPosition = spots[currentSpotIndex];

        anim = gameObject.GetComponentInChildren<Animator>();
    }


    void Update()
    {
        // Check if the object is within stopping distance of the player
        if (Vector3.Distance(transform.position, player.transform.position) < stopDistance)
        {
            // Stop following the player
            Idle();
            return;
        }

        // Check if the current spot has been reached, and if so, move to the next spot
        if (Vector3.Distance(transform.position, targetPosition) < followDistance)
        {
            currentSpotIndex++;
            if (currentSpotIndex >= spots.Count)
            {
                currentSpotIndex = 0;
            }

            // Set the new target position to the next spot in the list
            targetPosition = spots[currentSpotIndex];

            // check if up 
            if (spots[currentSpotIndex - 1].y < targetPosition.y) { TurnUp(); }
            // check if down 
            else if (spots[currentSpotIndex - 1].y > targetPosition.y) { TurnDown(); }
            // check if turn right 
            else if (spots[currentSpotIndex - 1].x < targetPosition.x) { TurnRight(); }
            // check if left 
            else if (spots[currentSpotIndex - 1].x > targetPosition.x) { TurnLeft(); }
        }

        // Interpolate towards the target position
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * interpolationSpeed * Time.deltaTime;

    }



    private void TurnUp(){
            Debug.Log("UP!");
            
            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
    }

    private void TurnDown(){
            Debug.Log("DOWN!");

            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
    }

    private void TurnRight(){
            Debug.Log("RIGHT!");

            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Right", true);
            anim.SetBool("Left", false);
    }

    private void TurnLeft(){
            Debug.Log("LEFT!");

            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", true);
    }

    private void Idle(){
            Debug.Log("STOP!");

            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
    }
}
