using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmigrantFollowSpots : MonoBehaviour
{
    public GameObject player;
    public PlayerInteractions playerStates;

    public float moveSpeed = 5.0f;
    public float followDistance = 3f;
    public float stopDistance = 0.5f;
    public float interpolationSpeed = 5.0f;

    //private bool moving;

    private Queue<Vector3> spots;
    private int currentSpotIndex;
    private Vector3 targetPosition;

    public Animator anim;

    private bool isFollowing = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerStates = player.GetComponent<PlayerInteractions>();

        PlayerGridMove playerController = player.GetComponent<PlayerGridMove>();
        spots = playerController.spots;

        //moving = false;

        spots.Enqueue(player.transform.position);
        targetPosition = transform.position;

        anim = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!isFollowing) return;
        
        transform.position = Vector3.MoveTowards(transform.position, 
                            targetPosition, moveSpeed * Time.deltaTime);
        // Vector3.Distance(transform.position, player.transform.position) < stopDistance
        if (spots.Count == 1)
        {
            Idle();
            return;
        }
        
        if (spots.Count > 0) 
        {
            targetPosition = spots.Dequeue();
            //Debug.Log(targetPosition);

            if (transform.position.y < targetPosition.y) { TurnUp(); }
            else if (transform.position.y > targetPosition.y) { TurnDown(); }
            else if (transform.position.x < targetPosition.x) { TurnRight(); }
            else if (transform.position.x > targetPosition.x) { TurnLeft(); }
        }

        

        // else if (Vector3.Distance(transform.position, targetPosition) <= .05f) 
        // {
        //     if (moving) {
        //         moving = false;
        //     }
        // }

        // else {
        //     moving = true;
        // }

        // if (Vector3.Distance(transform.position, player.transform.position) < followDistance)
        // {
        //     targetPosition = player.transform.position;
        // }
        // else if (Vector3.Distance(transform.position, targetPosition) < followDistance)
        // {
            
        //     // if (currentSpotIndex >= spots.Count)
        //     // {
        //     //     currentSpotIndex = 0;
        //     // }

        //     targetPosition = spots[currentSpotIndex];
        //     currentSpotIndex++;

        //     
        // }

        // Vector3 direction = (targetPosition - transform.position).normalized;
        // transform.position += direction * moveSpeed * Time.deltaTime;

        //transform.position = targetPosition;

        

        // if (direction.y > 0) { TurnUp(); }
        // else if (direction.y < 0) { TurnDown(); }
        // else if (direction.x > 0) { TurnRight(); }
        // else if (direction.x < 0) { TurnLeft(); }
    }


    public bool getIsFollow() 
    {
        return isFollowing;
    }

    public void FollowPlayer() 
    {
        isFollowing = true;
    }

    public void StopFollow() 
    {
        isFollowing = false;
        Idle();
    }


    private void TurnUp(){
        anim.SetBool("Up", true);
        anim.SetBool("Down", false);
        anim.SetBool("Right", false);
        anim.SetBool("Left", false);
        anim.SetBool("Moving", true);
    }

    private void TurnDown(){
        anim.SetBool("Up", false);
        anim.SetBool("Down", true);
        anim.SetBool("Right", false);
        anim.SetBool("Left", false);
        anim.SetBool("Moving", true);
    }

    private void TurnRight(){
        anim.SetBool("Up", false);
        anim.SetBool("Down", false);
        anim.SetBool("Right", true);
        anim.SetBool("Left", false);
        anim.SetBool("Moving", true);
    }

    private void TurnLeft(){
        //Debug.Log("LEFT!");

        anim.SetBool("Up", false);
        anim.SetBool("Down", false);
        anim.SetBool("Right", false);
        anim.SetBool("Left", true);
        anim.SetBool("Moving", true);
    }

    private void Idle(){
        //Debug.Log("STOP!");
        anim.SetBool("Moving", false);
    }
}
