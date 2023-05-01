using System.Collections;
using UnityEngine;

public class ImmigrantFollowSpots_new : MonoBehaviour
{
    public PlayerGridMove playerGridMove;
    public float followDistance = 1.0f;
    public float followSpeed = 3.0f;
    public int followIndex;
    public float stopDistance = 1f; 

    private bool isFollowing = false;
    private Vector3 currentTarget;

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerGridMove.transform.position);

        if (Input.GetKeyDown(KeyCode.X) && !isFollowing && distanceToPlayer <= 1.0f)
        {
            isFollowing = true;
        }

        if (Input.GetKeyDown(KeyCode.P) && isFollowing && distanceToPlayer <= 1.0f)
        {
            isFollowing = false;
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

    public bool IsFollowing
    {
        get { return isFollowing; }
        set { isFollowing = value; }
    }
}
