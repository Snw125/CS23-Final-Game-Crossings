using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGridMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public Animator anim;
    private bool moving;
    public bool canMove;

    Collider2D DownCollide;
    Collider2D LeftCollide;
    Collider2D UpCollide;
    Collider2D RightCollide;

    public Queue<Vector3> spots = new Queue<Vector3>(); // list of positions to follow
    public List<Transform> immigrants = new List<Transform>(); // list of immigrant objects to follow

    public PlayerInteractions interact;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        interact = GetComponent<PlayerInteractions>();
        
        movePoint.parent = null;

        // set colliders to inactve except down 
        Collider2D[] colliderArr = transform.GetComponents<Collider2D>();
        DownCollide = colliderArr[1];
        LeftCollide = colliderArr[2];
        UpCollide = colliderArr[3];
        RightCollide = colliderArr[4];

        DownCollide.enabled = true;
        LeftCollide.enabled = false;
        UpCollide.enabled = false;
        RightCollide.enabled = false;

        anim.SetBool("Moving", false);
        anim.SetBool("Up", false);
        anim.SetBool("Down", true);
        anim.SetBool("Right", false);
        anim.SetBool("Left", false);
        moving = false;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
                             movePoint.position, moveSpeed * Time.deltaTime);

        if (interact.hidden) 
        {
            canMove = false;
        }
        else {
            canMove = true;
        }

        if (canMove && Vector3.Distance(transform.position, movePoint.position) <= .05f) {

            if (moving) {
                if(interact.nearImm) {
                    spots.Clear();
                }
                spots.Enqueue(transform.position);

                moving = false;
            } else {
                anim.SetBool("Moving", false);
            }
        
            // move the movePoint to one space away in the intended direction
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) {
                Turn();
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0f, whatStopsMovement)) {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {
                Turn();
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0f, whatStopsMovement)) {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    // Debug.Log("Vertical");
                } 
                
            }
        } else if (canMove) { // Animate Movement
            moving = true;
            // Debug.Log(Vector3.Distance(transform.position, movePoint.position));
            anim.SetBool("Moving", true);

            // Debug.Log("Walk");
            Turn();
        } else { // hidden/cant move
            UpCollide.enabled = false;
            DownCollide.enabled = true;
            RightCollide.enabled = false;
            LeftCollide.enabled = false;
            
            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
        }
    }

    public void Turn() {
        if (Input.GetAxisRaw("Horizontal") > 0f) {
                UpCollide.enabled = false;
                DownCollide.enabled = false;
                RightCollide.enabled = true;
                LeftCollide.enabled = false;
                
                anim.SetBool("Up", false);
                anim.SetBool("Down", false);
                anim.SetBool("Right", true);
                anim.SetBool("Left", false);
        } else if(Input.GetAxisRaw("Horizontal") < 0f) {
            UpCollide.enabled = false;
            DownCollide.enabled = false;
            RightCollide.enabled = false;
            LeftCollide.enabled = true;
            
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", true);
        } else if (Input.GetAxisRaw("Vertical") > 0f) {
            UpCollide.enabled = true;
            DownCollide.enabled = false;
            RightCollide.enabled = false;
            LeftCollide.enabled = false;
            
            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
        } else if (Input.GetAxisRaw("Vertical") < 0f) {
            UpCollide.enabled = false;
            DownCollide.enabled = true;
            RightCollide.enabled = false;
            LeftCollide.enabled = false;
            
            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
        }
    }
}

