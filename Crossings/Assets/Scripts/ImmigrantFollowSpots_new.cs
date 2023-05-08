using System.Collections;
using UnityEngine;

public class ImmigrantFollowSpots_new : MonoBehaviour
{
    public PlayerGridMove playerGridMove;
    public float followDistance = 1.0f;
    public float followSpeed = 3.0f;
    public int followIndex;

    private static int nextFollowIndex;
    public float stopDistance = 1f;
    public GameObject gameHandlerObject; 

    private bool isFollowing = false;
    private Vector3 currentTarget;
     
    public GameHandler gameHandler;
    public GameObject temp_boarder;

    public ImmigrantManager immManager;

    public Vector3 spawnpos;

    private void Start()
    {
        gameHandlerObject = GameObject.FindWithTag("GameController"); 
        gameHandler = GameObject.FindWithTag("GameController").GetComponent<GameHandler>();
        playerGridMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGridMove>();
        immManager = GameObject.FindGameObjectWithTag("ImmManager").GetComponent<ImmigrantManager>();
        spawnpos = transform.position;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerGridMove.movePoint.position);

        // if (Input.GetKeyDown(KeyCode.X) && !isFollowing && distanceToPlayer <= 1.0f)
        // {
        //     Debug.Log("Following!");
        //     immManager.immsFollowing.Add(this.gameObject);
        //     isFollowing = true;
        //     followIndex = nextFollowIndex++; // Assign and increment the nextFollowIndex
        // }

        // if (Input.GetKeyDown(KeyCode.P) && isFollowing && distanceToPlayer <= 1.0f)
        // {
        //     immManager.immsFollowing.Remove(this.gameObject);
        //     isFollowing = false;
        //     nextFollowIndex--; // Decrement the nextFollowIndex
        // }

        if (isFollowing)
        {
            FollowPlayerSpots();
        }
    }

    private void FollowPlayerSpots()
    {
        if (playerGridMove.Spots.Count > followIndex)
        {
            currentTarget = playerGridMove.Spots.ToArray()[playerGridMove.Spots.Count - 1 - followIndex];
        }
        else
        {
            currentTarget = playerGridMove.movePoint.position; 
            //currentTarget = playerGridMove.transform.position;
        }

        float distanceToTarget = Vector3.Distance(transform.position, currentTarget);

        if (distanceToTarget > followDistance * 0.5f && distanceToTarget > stopDistance)
        {
            transform.position = Vector3.Lerp(transform.position, currentTarget, followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TempBoarder"))
        {
            gameHandler.IncreaseBankBalance(50);
            Destroy(gameObject);
        }
    }

    public bool IsFollowing
    {
        get { return isFollowing; }
        set { isFollowing = value; }
    }

    
}
