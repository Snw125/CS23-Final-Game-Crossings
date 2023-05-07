using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NPC_PatrolSequencePoints : MonoBehaviour {
       public Animator anim;
       public float speed = 2f;
       private float waitTime;
       public float startWaitTime = 2f;

       public Transform[] moveSpots;
       public int startSpot = 0;
       public bool moveForward = true;

       public FieldOfView FOV;
       private bool seeing;

       // Turning
       private int nextSpot;
       private int previousSpot;

       public bool faceUp = false;
       public bool faceDown = false;
       public bool faceRight = false;
       public bool faceLeft = false;

       public bool moving = false;

       void Start(){
              waitTime = startWaitTime;
              nextSpot = startSpot;
              anim = gameObject.GetComponentInChildren<Animator>();
              FOV = gameObject.GetComponentInChildren<FieldOfView>();
       }

       void Update(){
            if (!FOV.CanSeePlayer && !FOV.CanSeeDecoy && !FOV.PlayerChase && !FOV.DecoyChase) {
                transform.position = Vector2.MoveTowards(transform.position, moveSpots[nextSpot].position, speed * Time.deltaTime);

                if(seeing) {
                    seeing = false;
                    // resume ANIM
                    // check if up 
                    if ((moveSpots[previousSpot]).position.y < (moveSpots[nextSpot]).position.y) { TurnUp(); }
                    // check if down 
                    else if ((moveSpots[previousSpot]).position.y > (moveSpots[nextSpot]).position.y) { TurnDown(); }
                    // check if turn right 
                    else if ((moveSpots[previousSpot]).position.x < (moveSpots[nextSpot]).position.x) { TurnRight(); }
                    // check if left 
                    else if ((moveSpots[previousSpot]).position.x > (moveSpots[nextSpot]).position.x) { TurnLeft(); }
                }

                if (Vector2.Distance(transform.position, moveSpots[nextSpot].position) < 0.2f){
                        if (waitTime <= 0){
                                if (moveForward == true){ previousSpot = nextSpot; nextSpot += 1; }
                                else if (moveForward == false){ previousSpot = nextSpot; nextSpot -= 1; }
                                waitTime = startWaitTime;

                                // check if up 
                                if ((moveSpots[previousSpot]).position.y < (moveSpots[nextSpot]).position.y) { TurnUp(); }
                                // check if down 
                                else if ((moveSpots[previousSpot]).position.y > (moveSpots[nextSpot]).position.y) { TurnDown(); }
                                // check if turn right 
                                else if ((moveSpots[previousSpot]).position.x < (moveSpots[nextSpot]).position.x) { TurnRight(); }
                                // check if left 
                                else if ((moveSpots[previousSpot]).position.x > (moveSpots[nextSpot]).position.x) { TurnLeft(); }
                        } else {
                                Idle();
                                waitTime -= Time.deltaTime;
                        }
                }

                // goes back and forth 
                if (nextSpot == 0) {moveForward = true; }
                else if (nextSpot == (moveSpots.Length -1)) { moveForward = false; }

                // cycle thru spots 
                if (previousSpot < 0){ previousSpot = moveSpots.Length - 1; }
                else if (previousSpot > moveSpots.Length -1){ previousSpot = 0; }
            }
            else {
                Idle();
                seeing = true;
            }
              

       }

       private void TurnUp(){
            faceUp = true;
            faceDown = false;
            faceRight = false;
            faceLeft = false;

            moving = true;

            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
            anim.SetBool("Moving", true);
    }

    private void TurnDown(){
            faceUp = false;
            faceDown = true;
            faceRight = false;
            faceLeft = false;

            moving = true;

            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
            anim.SetBool("Moving", true);
    }

    private void TurnRight(){
            faceUp = false;
            faceDown = false;
            faceRight = true;
            faceLeft = false;

            moving = true;

            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Right", true);
            anim.SetBool("Left", false);
            anim.SetBool("Moving", true);
    }

    private void TurnLeft(){
            faceUp = false;
            faceDown = false;
            faceRight = false;
            faceLeft = true;

            moving = true;

            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", true);
            anim.SetBool("Moving", true);
    }



    private void Idle(){
            moving = false;
            anim.SetBool("Moving", false);
    }

}
