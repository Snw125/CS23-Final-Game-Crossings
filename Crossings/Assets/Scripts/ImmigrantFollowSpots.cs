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

    private List<Vector3> spots;
    private int currentSpotIndex;
    private Vector3 targetPosition;

    public Animator anim;

    private bool isFollowing = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerStates = player.GetComponent<PlayerInteractions>();

        currentSpotIndex = 0;

        PlayerMovementSpots playerController = player.GetComponent<PlayerMovementSpots>();
        spots = playerController.spots;

        spots.Insert(0, player.transform.position);
        targetPosition = spots[currentSpotIndex];

        anim = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!isFollowing) return;

        targetPosition = player.transform.position;

        if (Vector3.Distance(transform.position, player.transform.position) < stopDistance)
        {
            Idle();
            return;
        }

        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (direction.y > 0) { TurnUp(); }
        else if (direction.y < 0) { TurnDown(); }
        else if (direction.x > 0) { TurnRight(); }
        else if (direction.x < 0) { TurnLeft(); }
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
