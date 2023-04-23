using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmigrantFollowSpots : MonoBehaviour
{
    public GameObject player;
    public PlayerInteractions playerStates;

    public float moveSpeed = 5.0f;
    public float followDistance = 1.0f;
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

    if (Vector3.Distance(transform.position, player.transform.position) < stopDistance)
    {
        Idle();
        return;
    }

    if (Vector3.Distance(transform.position, player.transform.position) < followDistance)
    {
        targetPosition = player.transform.position;
    }
    else if (Vector3.Distance(transform.position, targetPosition) < followDistance)
    {
        currentSpotIndex++;
        if (currentSpotIndex >= spots.Count)
        {
            currentSpotIndex = 0;
        }

        targetPosition = spots[currentSpotIndex];

        if (spots[currentSpotIndex - 1].y < targetPosition.y) { TurnUp(); }
        else if (spots[currentSpotIndex - 1].y > targetPosition.y) { TurnDown(); }
        else if (spots[currentSpotIndex - 1].x < targetPosition.x) { TurnRight(); }
        else if (spots[currentSpotIndex - 1].x > targetPosition.x) { TurnLeft(); }
    }

    Vector3 direction = (targetPosition - transform.position).normalized;
    transform.position += direction * interpolationSpeed * Time.deltaTime;
}


public void OnTriggerEnter2D(Collider2D other) 
{
    if (other.gameObject.tag == "Player") 
    {
        playerStates.XButtonSig.SetActive(true);
        playerStates.thingNear = this.gameObject;
        playerStates.currImm = this;
        playerStates.nearImm = true;

        playerStates.thingNearArt = playerStates.thingNear.transform.GetChild(0).GetComponent<SpriteRenderer>();
        playerStates.thingNearArt.color = Color.yellow;
    }
}

public void OnTriggerExit2D(Collider2D other) 
{
    if (other.gameObject.tag == "Player") 
    {
        playerStates.thingNearArt = playerStates.thingNear.transform.GetChild(0).GetComponent<SpriteRenderer>();
        playerStates.thingNearArt.color = Color.white;

        playerStates.XButtonSig.SetActive(false);
        playerStates.thingNear = null;
        playerStates.currImm = null;
        playerStates.nearImm = false;
    }
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
        //Debug.Log("UP!");

        anim.SetBool("Up", true);
        anim.SetBool("Down", false);
        anim.SetBool("Right", false);
        anim.SetBool("Left", false);
        anim.SetBool("Moving", true);
    }

    private void TurnDown(){
        //Debug.Log("DOWN!");

        anim.SetBool("Up", false);
        anim.SetBool("Down", true);
        anim.SetBool("Right", false);
        anim.SetBool("Left", false);
        anim.SetBool("Moving", true);
    }

    private void TurnRight(){
        //Debug.Log("RIGHT!");

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
