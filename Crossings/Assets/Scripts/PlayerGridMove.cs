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

    public PlayerInteractions interact;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        interact = GetComponent<PlayerInteractions>();
        
        movePoint.parent = null;
        anim.SetBool("Moving", false);
        anim.SetBool("Up", false);
        anim.SetBool("Down", false);
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
                             
        if (canMove && Vector3.Distance(transform.position, movePoint.position) <= .05f) {

            if (moving) {
                moving = false;
            } else {
                anim.SetBool("Moving", false);
            }
        
            // move the movePoint to one space away in the intended direction
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0f, whatStopsMovement)) {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {
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
            if(Input.GetAxisRaw("Horizontal") > 0f) {
                anim.SetBool("Up", false);
                anim.SetBool("Down", false);
                anim.SetBool("Right", true);
                anim.SetBool("Left", false);
            } else if(Input.GetAxisRaw("Horizontal") < 0f) {
                anim.SetBool("Up", false);
                anim.SetBool("Down", false);
                anim.SetBool("Right", false);
                anim.SetBool("Left", true);
            } else if (Input.GetAxisRaw("Vertical") > 0f) {
                anim.SetBool("Up", true);
                anim.SetBool("Down", false);
                anim.SetBool("Right", false);
                anim.SetBool("Left", false);
            } else if (Input.GetAxisRaw("Vertical") < 0f) {
                anim.SetBool("Up", false);
                anim.SetBool("Down", true);
                anim.SetBool("Right", false);
                anim.SetBool("Left", false);
            }
        }
    }
}

