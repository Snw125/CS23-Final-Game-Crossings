using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NPC_Gaze_ForPatrol : MonoBehaviour {

       public bool isVertical = false;

       public float distance = 50;
       public LineRenderer lineOfSight;
       public Gradient redColor;
       public Gradient greenColor;
       public Vector3 rayDirection;

       private Renderer rend;
       //private GameHandler gameHandler;

       private bool canHit = true;
       private float coolDown = 0.5f;

       //public Transform tempCircle; //test where ray hits, 1/2

       void Start() {
              Physics2D.queriesStartInColliders = false;

              rend = GetComponentInChildren<Renderer>();
             
            //   if (GameObject.FindWithTag ("GameHandler") != null) {
            //      gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler>();
            //   }
       }

       void FixedUpdate () {
              // allow designers to choose vertical or horizontal patrol paths:
              if (isVertical == false){
                     bool isRight = GetComponent<NPC_PatrolSequencePoints>().faceRight;
                     if (isRight){rayDirection = transform.right;}
                     else {rayDirection = -transform.right;}
              } else {
                     bool isRight = GetComponent<NPC_PatrolSequencePoints>().faceRight;
                     if (isRight){rayDirection = transform.up;}
                     else {rayDirection = -transform.up;}
              }

              // Raycast needs location, Direction, and length
              RaycastHit2D hitInfo = Physics2D.Raycast (transform.position, transform.right, distance);

              // tempCircle.position = hitInfo.point; //test where ray hits 2/2

              if (hitInfo.collider != null) {
                     // if ray hits player, set it to red and add commands for alerted guard
                     if (hitInfo.collider.CompareTag ("Player")) {
                            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
                            lineOfSight.SetPosition(1, hitInfo.point); // index 1 is line end-point
                            lineOfSight.colorGradient = redColor;
                            if (canHit){
                                   StartCoroutine(Enemy_Alert(1));
                                   StartCoroutine(EnemyCoolDown());
                                   Debug.Log("Oops -- Guard saw me.");
                           }
                     }

                     // if ray hits not player, set it to green
                     else if (!hitInfo.collider.CompareTag ("Player")) {
                            Debug.DrawLine(transform.position, hitInfo.point, Color.green);
                            lineOfSight.SetPosition(1, hitInfo.point); // index 1 is line end-point
                            lineOfSight.colorGradient = greenColor;
                     }

              // if ray hits nothing, set it to green
              } else {
                     Debug.DrawLine(transform.position, transform.position + rayDirection * distance, Color.green);
                     lineOfSight.SetPosition(1, transform.position + rayDirection * distance); // index 1 is line end
                     lineOfSight.colorGradient = greenColor;
              }

              lineOfSight.SetPosition (0, transform.position); // index 0 is the line start-point
       }

       // if player touches the guard:
       void OnTriggerEnter2D(Collider2D other){
              if ((other.gameObject.tag == "Player") && (canHit)) {
                     StartCoroutine(Enemy_Alert(2));
                     StartCoroutine(EnemyCoolDown());
                     Debug.Log("Ack -- Hit a Guard.");
              }
       }

       // raise enemy alertness, flash enemy color:
       IEnumerator Enemy_Alert(int amt){
              float pauseTime = 1f * amt;
              // color values are R, G, B, and alpha, each divided by 100
              rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);

              // alert GameHandler to increase guard alertness: a function that
              // increases a static int. If it gets too high, guards chase, go faster, etc.
              // gameHandler.EnemyAlertness(amt)
              Debug.Log("Enemy alertness increased by "+ amt);
              yield return new WaitForSeconds(pauseTime);
              rend.material.color = Color.white;
       }

       // cooldown prevents enemy from destroying player with one gaze:
       IEnumerator EnemyCoolDown(){
              lineOfSight.colorGradient = redColor;
              canHit = false;
              Debug.Log("canHit "+ canHit);
              yield return new WaitForSeconds(coolDown);
              canHit = true;
              Debug.Log("canHit " + canHit);
              lineOfSight.colorGradient = greenColor;
       }
}
