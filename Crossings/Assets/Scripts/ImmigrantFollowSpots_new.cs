using System.Collections;
using UnityEngine;

public class ImmigrantFollowSpots_new : MonoBehaviour
{
    public PlayerGridMove playerGridMove;
    public float followDistance = 1.0f;
    public float followSpeed = 3.0f;
    public int followIndex;

    private static int nextFollowIndex = 0;
    public float stopDistance = 1f;
    public GameObject gameHandlerObject; 

    private bool isFollowing = false;
    private Vector3 currentTarget;
     
    public GameHandler gameHandler;
    public GameObject temp_boarder;

    private void Start()
    {
        gameHandler = gameHandlerObject.GetComponent<GameHandler>(); 
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerGridMove.transform.position);

        if (Input.GetKeyDown(KeyCode.X) && !isFollowing && distanceToPlayer <= 1.0f)
        {
            isFollowing = true;
            followIndex = nextFollowIndex++; // Assign and increment the nextFollowIndex
        }

        if (Input.GetKeyDown(KeyCode.P) && isFollowing && distanceToPlayer <= 1.0f)
        {
            isFollowing = false;
            nextFollowIndex--; // Decrement the nextFollowIndex
        }

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
            currentTarget = playerGridMove.transform.position;
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
            gameHandler.IncreaseBankBalance(100);
            Destroy(gameObject);
        }
    }

    public bool IsFollowing
    {
        get { return isFollowing; }
        set { isFollowing = value; }
    }

    
}
