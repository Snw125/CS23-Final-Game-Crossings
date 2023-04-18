using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius = 5f; 
    [Range(1, 360)] public float angle = 45f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    public GameObject playerRef;
    public PlayerInteractions playerStates;
    public PatrolCircle pathver1;
    public NPC_PatrolSequencePoints pathver2;

    public bool CanSeePlayer;

    // !!! make this easy to change on upper level
    public int turnadj = 360;

    public GameObject Timer;

    private float noticeTime;
    private float loseNoticeTime;
    private float currtime;
    private float savedTime = 0f;
    public float endNoticeTime = 2f;

    public bool PlayerChase = false;
    public float chaseSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        playerStates = playerRef.GetComponent<PlayerInteractions>();

        if (gameObject.GetComponent<PatrolCircle>() != null) 
        {
            pathver1 = gameObject.GetComponent<PatrolCircle>();
        }
        if (gameObject.GetComponent<NPC_PatrolSequencePoints>() != null) 
        {
            pathver2 = gameObject.GetComponent<NPC_PatrolSequencePoints>();
        }

        Timer = transform.GetChild(2).transform.GetChild(0).gameObject;

        StartCoroutine(FOVCheck());
    }

    // Update is called once per frame
    void Update()
    {
        // if player is hidden SKIP THESE STEPS 
        if (!playerStates.hidden) {
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
        

        if (PlayerChase) 
        {
            transform.position = Vector2.MoveTowards(transform.position, playerRef.transform.position, chaseSpeed * Time.deltaTime);
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
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if (rangeCheck.Length > 0) 
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            // !!! change for direction as well (transform.up)
            if (Vector2.Angle(-transform.up, directionToTarget) < angle / 2)
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
