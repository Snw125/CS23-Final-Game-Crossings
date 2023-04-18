using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PatrolCircle : MonoBehaviour {
       public Animator anim;
       public float speed = 2f;
       private float waitTime;
       public float startWaitTime = 2f;

       public Transform[] moveSpots;
       public int startSpot = 0;
       public bool endPath = false;

       // Turning
       private int nextSpot;
       private int previousSpot;

       public bool faceUp = false;
       public bool faceDown = false;
       public bool faceRight = false;
       public bool faceLeft = false;

       void Start(){
              waitTime = startWaitTime;
              nextSpot = startSpot;
              anim = gameObject.GetComponentInChildren<Animator>();
       }

       void Update(){
              transform.position = Vector2.MoveTowards(transform.position, moveSpots[nextSpot].position, speed * Time.deltaTime);

              if (Vector2.Distance(transform.position, moveSpots[nextSpot].position) < 0.2f){
                     if (waitTime <= 0){
                            if (endPath == false){ previousSpot = nextSpot; nextSpot += 1; }
                            else if (endPath == true){ previousSpot = nextSpot; nextSpot = 0; }
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
                            //Debug.Log("waiting!");
                            waitTime -= Time.deltaTime;
                     }
              }

              // cause the cycle
              if (nextSpot == 0) {endPath = false; }
              else if (nextSpot == (moveSpots.Length -1)) { endPath = true; }

              // cycle thru spots
              if (previousSpot < 0){ previousSpot = moveSpots.Length - 1; }
              else if (previousSpot > moveSpots.Length -1){ previousSpot = 0; }

       }



    private void TurnUp(){
            faceUp = true;
            faceDown = false;
            faceRight = false;
            faceLeft = false;

            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
    }

    private void TurnDown(){
            faceUp = false;
            faceDown = true;
            faceRight = false;
            faceLeft = false;

            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
    }

    private void TurnRight(){
            faceUp = false;
            faceDown = false;
            faceRight = true;
            faceLeft = false;

            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Right", true);
            anim.SetBool("Left", false);
    }

    private void TurnLeft(){
            faceUp = false;
            faceDown = false;
            faceRight = false;
            faceLeft = true;

            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", true);
    }

    private void Idle(){
            faceUp = false;
            faceDown = false;
            faceRight = false;
            faceLeft = false;

            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
    }

}
