using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius = 5f; 
    [Range(1, 360)] public float angle = 45f;
    private float findAngle;

    public LayerMask decoyLayer;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    public GameObject playerRef;
    public PlayerInteractions playerStates;
    public PatrolCircle pathvercircle;
    public NPC_PatrolSequencePoints pathverswitch;

    public bool CanSeePlayer;
    public bool CanSeeDecoy;

    public Transform target;

    // !!! make this easy to change on upper level
    public int turnadj = 360;

    public GameObject Timer;

    private float noticeTime;
    private float loseNoticeTime;
    private float currtime;
    private float savedTime = 0f;
    public float endNoticeTime = 2f;

    public bool PlayerChase = false;
    public bool DecoyChase = false;
    public float chaseSpeed = 2f;

    public bool facingUp;
    public bool facingDown;
    public bool facingRight;
    public bool facingLeft;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        playerStates = playerRef.GetComponent<PlayerInteractions>();

        if (gameObject.GetComponent<PatrolCircle>() != null) 
        {
            pathvercircle = gameObject.GetComponent<PatrolCircle>();
        }
        if (gameObject.GetComponent<NPC_PatrolSequencePoints>() != null) 
        {
            pathverswitch = gameObject.GetComponent<NPC_PatrolSequencePoints>();
        }

        Timer = transform.GetChild(2).transform.GetChild(0).gameObject;

        CanSeeDecoy = false;
        CanSeePlayer = false;

        StartCoroutine(FOVCheck());
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeeDecoy) {
            if (savedTime + currtime < endNoticeTime) {
                currtime = Time.time - noticeTime;
            }
            if (savedTime + currtime >= endNoticeTime) {

                // chase!
                Debug.Log("Chase!");
                if (target != null) {
                    DecoyChase = true;
                }
            }

            Timer.transform.localScale = new Vector3(1, (savedTime + currtime)/endNoticeTime, 1);
        }
        else {
            if ((savedTime - currtime)/endNoticeTime > 0) {
                currtime = Time.time - loseNoticeTime;
            }
            else {
                DecoyChase = false;
            }
            
            Timer.transform.localScale = new Vector3(1, (savedTime - currtime)/endNoticeTime, 1);
            
        }

        //if player is hidden SKIP THESE STEPS 
        if (!playerStates.hidden && !CanSeeDecoy) {
            if (CanSeePlayer) {
                if (savedTime + currtime < endNoticeTime) {
                    currtime = Time.time - noticeTime;
                }
                if (savedTime + currtime >= endNoticeTime) {

                    // chase!
                    Debug.Log("Chase!");
                    PlayerChase = true;
                }

                Timer.transform.localScale = new Vector3(1, (savedTime + currtime)/endNoticeTime, 1);
            }
            else {
                if ((savedTime - currtime)/endNoticeTime > 0) {
                    currtime = Time.time - loseNoticeTime;
                }
                else {
                    PlayerChase = false;
                }
                
                Timer.transform.localScale = new Vector3(1, (savedTime - currtime)/endNoticeTime, 1);
                
            }
        }

        if (pathvercircle != null) {
            facingUp = pathvercircle.faceUp;
            facingDown = pathvercircle.faceDown;
            facingLeft = pathvercircle.faceLeft;
            facingRight = pathvercircle.faceRight;
        }
        else if (pathverswitch != null) {
            facingUp = pathverswitch.faceUp;
            facingDown = pathverswitch.faceDown;
            facingLeft = pathverswitch.faceLeft;
            facingRight = pathverswitch.faceRight;
        }
        

        if (PlayerChase) 
        {
            // TO DO: make this more complicated
            transform.position = Vector2.MoveTowards(transform.position, playerRef.transform.position, chaseSpeed * Time.deltaTime);
        }

        if (DecoyChase) {
            // TO DO: make this more complicated
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, chaseSpeed * Time.deltaTime);
            if (transform.position == target.position) {
                Destroy(target.gameObject);
                DecoyChase = false;
            }
        }

        
        
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FOV();
        }
    }

    private void FOV()
    {
        Collider2D[] rangeCheckDecoy = Physics2D.OverlapCircleAll(transform.position, radius, decoyLayer);
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if (rangeCheckDecoy.Length > 0) 
        {
            target = rangeCheckDecoy[0].transform;
            Debug.Log(target);
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            
            if (facingUp) {
                findAngle = Vector2.Angle(transform.up, directionToTarget);
            }
            if (facingDown) {
                findAngle = Vector2.Angle(-transform.up, directionToTarget);
            }
            if (facingRight) {
                findAngle = Vector2.Angle(transform.right, directionToTarget);
            }
            if (facingLeft) {
                findAngle = Vector2.Angle(-transform.right, directionToTarget);
            }


            if (findAngle < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                {
                    if (!CanSeeDecoy) {
                        noticeTime = Time.time;
                        savedTime = savedTime - currtime;
                        currtime = 0;
                    }
                    CanSeeDecoy = true;
                    Debug.Log("Decoy Seen!");
                }       
                else 
                {
                    if (CanSeeDecoy) {
                        loseNoticeTime = Time.time;
                        savedTime = savedTime + currtime;
                        currtime = 0;
                    }
                    CanSeeDecoy = false;
                }  
            }
            else 
            {
                if (CanSeeDecoy) {
                    loseNoticeTime = Time.time;
                    savedTime = savedTime + currtime;
                    currtime = 0;
                }
                CanSeeDecoy = false;
            }
        }
        else if (CanSeeDecoy)
        {
            loseNoticeTime = Time.time;
            savedTime = savedTime + currtime;
            currtime = 0;
            CanSeeDecoy = false;
        }

        if (rangeCheck.Length > 0) 
        {
            target = rangeCheck[0].transform;
            Debug.Log(target);
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            
            if (facingUp) {
                findAngle = Vector2.Angle(transform.up, directionToTarget);
            }
            if (facingDown) {
                findAngle = Vector2.Angle(-transform.up, directionToTarget);
            }
            if (facingRight) {
                findAngle = Vector2.Angle(transform.right, directionToTarget);
            }
            if (facingLeft) {
                findAngle = Vector2.Angle(-transform.right, directionToTarget);
            }


            if (findAngle < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                {
                    if (!CanSeePlayer) {
                        noticeTime = Time.time;
                        savedTime = savedTime - currtime;
                        currtime = 0;
                    }
                    CanSeePlayer = true;
                }       
                else 
                {
                    if (CanSeePlayer) {
                        loseNoticeTime = Time.time;
                        savedTime = savedTime + currtime;
                        currtime = 0;
                    }
                    CanSeePlayer = false;
                }  
            }
            else 
            {
                if (CanSeePlayer) {
                    loseNoticeTime = Time.time;
                    savedTime = savedTime + currtime;
                    currtime = 0;
                }
                CanSeePlayer = false;
            }
        }
        else if (CanSeePlayer)
        {
            loseNoticeTime = Time.time;
            savedTime = savedTime + currtime;
            currtime = 0;
            CanSeePlayer = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player") 
        {
            if (PlayerChase == true) 
            {
                // GAME OVER
            }
        }
    }




    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.white;
    //     UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

    //     // !!!
    //     if (facingUp) {
    //         turnadj = 0;
    //     }
    //     if (facingDown) {
    //         turnadj = 360;
    //     }
    //     if (facingLeft) {
    //         turnadj = 540;
    //     }
    //     if (facingRight) {
    //         turnadj = 180;
    //     }

    //     Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, (-angle + turnadj) / 2);
    //     Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, (angle + turnadj) / 2);

    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
    //     Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

    //     if (CanSeePlayer)
    //     {
    //         Gizmos.color = Color.green;
    //         Gizmos.DrawLine(transform.position, playerRef.transform.position);
    //     }
    // }

    // private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    // {
    //     angleInDegrees += eulerY;

    //     return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    // }

}
