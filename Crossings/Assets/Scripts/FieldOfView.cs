using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Transform playertarget;
    public Vector2 whereplayerseen;
    public Vector2 wheretargetseen;

    public int turnadj = 360;

    public GameObject Timer;

    public float noticeTime;
    public float loseNoticeTime;
    public float currtime;
    public float savedTime = 0f;
    public float endNoticeTime = 2f;

    public bool PlayerChase = false;
    public bool DecoyChase = false;
    public bool goThere = false;
    public float chaseSpeed = 2f;

    public float progress;
    public float lastcheck;

    public bool facingUp;
    public bool facingDown;
    public bool facingRight;
    public bool facingLeft;

    public ImmigrantManager immmanage;

    public SpriteRenderer sprite;

    public bool cooldown;

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
        immmanage = GameObject.FindGameObjectWithTag("ImmManager").GetComponent<ImmigrantManager>();

        CanSeeDecoy = false;
        CanSeePlayer = false;
        cooldown = false;

        sprite = transform.GetComponentInChildren<SpriteRenderer>();

        StartCoroutine(FOVCheck());
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (PlayerChase) 
        // {
        // //     transform.position = Vector2.MoveTowards(transform.position, whereplayerseen, chaseSpeed * Time.deltaTime);
        // //     PlayerChase = false;
        // //     // raycast to player, move in that direction 
        // //     // failed chase
        // //     // Debug.Log(Vector3.Distance(transform.position, whereplayerseen));

        // //     // if (Vector3.Distance(transform.position, whereplayerseen) <= .2f) {
        // //     //     Debug.Log("Lost them");
        // //     //     if (!CanSeePlayer) {
        // //     //         PlayerChase = false;
        // //     //     }
        // //     // }

        // }
        
        if (PlayerChase) {
            transform.position = Vector2.MoveTowards(transform.position, whereplayerseen, chaseSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, whereplayerseen) <= .2f) {
                // Debug.Log("Lost them");
                if (!CanSeePlayer) {
                    MusicPlayer.PlayLevelMusic();
                    PlayerChase = false;
                }
            }
        }

        if (DecoyChase) {
            transform.position = Vector2.MoveTowards(transform.position, wheretargetseen, chaseSpeed * Time.deltaTime);
            // successful chase
            if (target == null) {
                DecoyChase = false;
            }
            else if (Vector3.Distance(transform.position, target.position) <= .2f) {
                if (target.tag == "Immigrant") 
                {
                    ImmigrantFollowSpots_new followcode = target.GetComponent<ImmigrantFollowSpots_new>();
                    Debug.Log(Vector3.Distance(followcode.spawnpos, target.position));
                    if (Vector3.Distance(followcode.spawnpos, target.position) > .2f) {
                        followcode.IsFollowing = false;
                        // remove from following array 
                        immmanage.immsFollowing.Remove(target.gameObject);
                        int index = 0;
                        // reassigns indices 
                        foreach (var immigrant in immmanage.immsFollowing)
                        {
                            ImmigrantFollowSpots_new followcode2 = immigrant.GetComponent<ImmigrantFollowSpots_new>();
                            followcode2.followIndex = index; 
                            index++; 
                        }       
                        target.transform.position = followcode.spawnpos;
                        DecoyChase = false; 
                        StartCoroutine(Cooldown());
                    }
                }
                else {
                    DecoyChase = false; 
                    Debug.Log("destroyed");
                    StartCoroutine(Cooldown());
                    Destroy(target.gameObject);
                }
                
            }
            // failed 
            // if (Vector3.Distance(transform.position, wheretargetseen) <= .5f) 
            // {
            //     DecoyChase = false;
            // }
        }

        if (CanSeeDecoy) {
            // if (savedTime + currtime < endNoticeTime) {
            //     currtime = Time.time - noticeTime;
            // }
            // else if (savedTime + currtime >= endNoticeTime) {

            //     // chase!
            //     Debug.Log("Chase!");
            //     if (target != null) {
            //         if (!DecoyChase) {
            //             DecoyChase = true;
            //             wheretargetseen = target.transform.position;
            //         }
            //     }
            // }

            progress += Time.time - lastcheck;
            if (progress >= 2) {
                progress = 2;
            }
            lastcheck = Time.time;

            Timer.transform.localScale = new Vector3(1, (progress - .01f)/endNoticeTime, 1);

            // Timer.transform.localScale = new Vector3(1, (savedTime + currtime)/endNoticeTime, 1);
        }
        // else if (!CanSeeDecoy && !CanSeePlayer) {
        //     // if ((savedTime - currtime)/endNoticeTime > 0) {
        //     //     currtime = Time.time - loseNoticeTime;
        //     // }
        //     // else {
        //     //     DecoyChase = false;
        //     //     DecoyTimeEnd = true;
        //     // }
            
        //     progress -= Time.time - lastcheck;
        //     if (progress <= 0) {
        //         progress = 0;
        //     }
        //     lastcheck = Time.time;

        //     Timer.transform.localScale = new Vector3(1, (progress - .01f)/endNoticeTime, 1);

        //     // Timer.transform.localScale = new Vector3(1, (savedTime - currtime)/endNoticeTime, 1);
            
        // }

        
        // PLAYER ------------------------------------------------------------ 
        else if (CanSeePlayer) {
            // if (savedTime + currtime < endNoticeTime) {
            //     //Debug.Log("Up:" + (savedTime + currtime));
            //     currtime = Time.time - noticeTime;
            //     PlayerChase = false;
            // }
            // else if (savedTime + currtime >= endNoticeTime) {

            //     // chase!
                
            //     Debug.Log("Chase!");
            //     PlayerChase = true;
            //     whereplayerseen = playerRef.transform.position;
            //     goThere = true;
            // }

            progress += Time.time - lastcheck;
            if (progress >= 2) {
                progress = 2;
            }
            lastcheck = Time.time;

            Timer.transform.localScale = new Vector3(1, (progress - .01f)/endNoticeTime, 1);
            //Timer.transform.localScale = new Vector3(1, ((savedTime + currtime)-.01f)/endNoticeTime, 1);
        }
        else if (!CanSeeDecoy && !CanSeePlayer) {
            //PlayerChase = false;
            progress -= Time.time - lastcheck;
            if (progress <= 0) {
                progress = 0;
            }
            lastcheck = Time.time;

            Timer.transform.localScale = new Vector3(1, (progress - .01f)/endNoticeTime, 1);
            // if ((savedTime - currtime)/endNoticeTime > 0) {
            //     Debug.Log("Down:" + (savedTime - currtime));
            //     currtime = Time.time - loseNoticeTime;
            // }
            // // else {
            // //     PlayerChase = false;
            // // }
            
            // Timer.transform.localScale = new Vector3(1, ((savedTime - currtime)+.01f)/endNoticeTime, 1);
            
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
        
        if (progress >= 2) {
            if (CanSeeDecoy && target != null) {
                Debug.Log("DecoyChase");
                DecoyChase = true;
                wheretargetseen = target.transform.position;
            }
            else if (CanSeePlayer) {
                if (!PlayerChase) {
                    MusicPlayer.PlaySpeedMusic();
                }
                PlayerChase = true;
                whereplayerseen = playerRef.transform.position;
            }  
        }
        else if (progress <= 0) {
            if (PlayerChase) {
                MusicPlayer.PlayLevelMusic();
            }   
            PlayerChase = false;
            DecoyChase = false;
        }
        else if (progress >= 0 && PlayerChase) {
            whereplayerseen = playerRef.transform.position;
        }
        
        
    }

    private IEnumerator Cooldown()
    {
        CanSeePlayer = false;
        CanSeeDecoy = false;
        cooldown = true;

        WaitForSeconds wait = new WaitForSeconds(2f);
        yield return wait;

        cooldown = false;
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
        // Debug.Log("can see:" + CanSeeDecoy);
        // Debug.Log("Chase:" + DecoyChase);
        Collider2D[] rangeCheckDecoy = Physics2D.OverlapCircleAll(transform.position, radius, decoyLayer);
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if (!cooldown) {
            if (rangeCheckDecoy.Length > 0) 
            {
                target = rangeCheckDecoy[0].transform;
                //Debug.Log(target);
                if (playerStates.hidden && target.tag == "Immigrant") {
                    Debug.Log("Imm is hiding with player!");
                }
                else {
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
                            // if (!CanSeeDecoy) {
                            //     noticeTime = Time.time;
                            //     savedTime = savedTime - currtime;
                            //     currtime = 0;
                            // }

                            if (target.tag == "Immigrant") 
                            {
                                ImmigrantFollowSpots_new followcode = target.GetComponent<ImmigrantFollowSpots_new>();
                                if (Vector3.Distance(followcode.spawnpos, target.position) > .2f) {
                                    if (!CanSeeDecoy) {
                                        // first seen 
                                        lastcheck = Time.time;
                                    }
                                    CanSeeDecoy = true;
                                }
                                else {
                                    CanSeeDecoy = false;
                                }
                            }
                            else {
                                if (!CanSeeDecoy) {
                                    // first seen 
                                    lastcheck = Time.time;
                                }
                                CanSeeDecoy = true;
                            }

                        }       
                        else 
                        {
                            // if (CanSeeDecoy) {
                            //     loseNoticeTime = Time.time;
                            //     savedTime = savedTime + currtime;
                            //     currtime = 0;
                            // }
                            CanSeeDecoy = false;
                        }  
                    }
                    else 
                    {
                        // if (CanSeeDecoy) {
                        //     loseNoticeTime = Time.time;
                        //     savedTime = savedTime + currtime;
                        //     currtime = 0;
                        // }
                        CanSeeDecoy = false;
                    }
                }

            }
            else if (CanSeeDecoy)
            {
                // loseNoticeTime = Time.time;
                // savedTime = savedTime + currtime;
                // currtime = 0;
                CanSeeDecoy = false;
            }



            // PLAYER -------------------------------------------------------------
            if (!playerStates.hidden) {
                if (rangeCheck.Length > 0) 
                {
                    playertarget = rangeCheck[0].transform;
                    Debug.Log(playertarget);
                    Vector2 directionToTarget = (playertarget.position - transform.position).normalized;
                    
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
                        float distanceToTarget = Vector2.Distance(transform.position, playertarget.position);

                        if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                        {
                            // if (!CanSeePlayer) {
                            //     Debug.Log("here");
                            //     noticeTime = Time.time;
                            //     //loseNoticeTime = Time.time;
                            //     savedTime = savedTime - currtime;
                            //     currtime = 0;
                            // }
                            if (!CanSeePlayer) {
                                // first seen 
                                lastcheck = Time.time;
                            }
                            CanSeePlayer = true;
                        }       
                        else 
                        {
                            // if (CanSeePlayer) {
                            //     //noticeTime = Time.time;
                            //     loseNoticeTime = Time.time;
                            //     savedTime = savedTime + currtime;
                            //     currtime = 0;
                            // }
                            CanSeePlayer = false;
                        }  
                    }
                    else 
                    {
                        // if (CanSeePlayer) {
                        //     //noticeTime = Time.time;  
                        //     loseNoticeTime = Time.time;
                        //     savedTime = savedTime + currtime;
                        //     currtime = 0;
                        // }
                        CanSeePlayer = false;
                    }
                }
                else if (CanSeePlayer)
                {
                    // //noticeTime = Time.time; 
                    // loseNoticeTime = Time.time;
                    // savedTime = savedTime + currtime;
                    // currtime = 0;
                    CanSeePlayer = false;
                }
            }

            if (playerStates.hidden && CanSeePlayer) {
                // //noticeTime = Time.time;
                // loseNoticeTime = Time.time;
                // savedTime = savedTime + currtime;
                // currtime = 0;
                CanSeePlayer = false;
            }
        }
        

        // if(PlayerChase && CanSeePlayer) {
        //     sprite.color = Color.red;
        //     // noticeTime = Time.time;
        //     // currtime = 0;
        // }
        // if(PlayerChase && !CanSeePlayer) {
        //     sprite.color = Color.blue;
        // }
        // if(!PlayerChase && CanSeePlayer) {
        //     sprite.color = Color.yellow;

        // }
        // if(!PlayerChase && !CanSeePlayer) {
        //     sprite.color = Color.green;
        // }

        // if (PlayerChase) {
        //     loseNoticeTime = Time.time;
        //     savedTime = savedTime + currtime;
        //     currtime = 0;
        // }
        
        // if (DecoyChase) {
        //     loseNoticeTime = Time.time;
        //     savedTime = savedTime + currtime;
        //     currtime = 0;
        // }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player") 
        {
            if (CanSeePlayer == true) 
            {
                SceneLoader.LoadScene("EndScene");    
            }
        }
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        // !!!
        if (facingUp) {
            turnadj = 0;
        }
        if (facingDown) {
            turnadj = 360;
        }
        if (facingLeft) {
            turnadj = 540;
        }
        if (facingRight) {
            turnadj = 180;
        }

        Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, (-angle + turnadj) / 2);
        Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, (angle + turnadj) / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        if (CanSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerRef.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
