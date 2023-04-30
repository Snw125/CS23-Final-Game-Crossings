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

    private Transform currentFollower;
    private List<Transform> followers = new List<Transform>();

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

        if (spots.Count == 1)
        {
            Idle();
            return;
        }
        
        if (spots.Count > 0) 
        {
            if (spots.Peek() == player.transform.position) {
                // Move the follower to the player's previous position
                spots.Dequeue();
                if (followers.Count > 0) {
                    Transform follower = followers[0];
                    followers.RemoveAt(0);
                    spots.Enqueue(follower.position);
                    currentFollower = follower;
                }
            }

            targetPosition = spots.Dequeue();

            if (transform.position.y < targetPosition.y) { TurnUp(); }
            else if (transform.position.y > targetPosition.y) { TurnDown(); }
            else if (transform.position.x < targetPosition.x) { TurnRight(); }
            else if (transform.position.x > targetPosition.x) { TurnLeft(); }
        }

        if (followers.Count > 0) {
            Transform lastFollower = followers[followers.Count - 1];
            Vector3 lastFollowerPos = spots.Peek();
            Vector3 direction = (lastFollowerPos - lastFollower.position).normalized;
            float distance = Vector3.Distance(lastFollowerPos, lastFollower.position);
            if (distance > followDistance) {
                Transform newFollower = followers[0];
                followers.RemoveAt(0);
                followers.Add(newFollower);
                spots.Enqueue(lastFollower.position - (direction * followDistance));
            }
        }
    }

    public bool getIsFollow() 
    {
        return isFollowing;
    }

    public void FollowPlayer() 
    {
        isFollowing = true;
        followers.Add(transform);
    }

    public void StopFollow() 
    {
        isFollowing = false;
        Idle();
        followers.Remove(transform);
        if (currentFollower != null) {
            spots.Enqueue(currentFollower.position);
            currentFollower = null;
        }
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
        anim.SetBool("Up", false);
        anim.SetBool("Down", false);
        anim.SetBool("Right", false);
        anim.SetBool("Left", true);
        anim.SetBool("Moving", true);
    }

    private void Idle(){
        anim.SetBool("Moving", false);
    }

    public void AddFollower(Transform follower) {
        followers.Add(follower);
        if (currentFollower == null) {
            currentFollower = follower;
            spots.Enqueue(currentFollower.position);
        } else {
            Vector3 lastFollowerPos = spots.Peek();
            Vector3 direction = (lastFollowerPos - currentFollower.position).normalized;
            spots.Enqueue(currentFollower.position - (direction * followDistance));
        }
    }

    public void RemoveFollower(Transform follower) {
        followers.Remove(follower);
        if (currentFollower == follower) {
            currentFollower = null;
            if (followers.Count > 0) {
                Transform newFollower = followers[0];
                followers.RemoveAt(0);
                currentFollower = newFollower;
                spots.Enqueue(currentFollower.position);
            }
        }
    }
}


