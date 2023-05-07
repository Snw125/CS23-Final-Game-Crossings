using System.Collections.Generic;
using UnityEngine;

public class ImmigrantManager : MonoBehaviour
{
    public List<ImmigrantFollowSpots_new> immigrants;
    public List<GameObject> immsFollowing;

    private PlayerGridMove playerGridMove;
    private PlayerInteractions playerState;

    private void Start() 
    {
        playerGridMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGridMove>();
        playerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteractions>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !playerState.hidden)
        {
            foreach (var immigrant in immigrants)
            {
                Debug.Log(immigrant);
                float distanceToPlayer = Vector3.Distance(immigrant.transform.position, playerGridMove.movePoint.position);

                if (!immigrant.IsFollowing && distanceToPlayer <= 2.0f)
                {
                    immsFollowing.Add(immigrant.gameObject);
                    immigrant.followIndex = GetNextFollowIndex();
                    immigrant.IsFollowing = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (var immigrant in immigrants)
            {
                float distanceToPlayer = Vector3.Distance(immigrant.transform.position, playerGridMove.movePoint.position);

                if (immigrant.IsFollowing && distanceToPlayer <= 2.0f)
                {
                    immsFollowing.Remove(immigrant.gameObject);
                    immigrant.IsFollowing = false;
                }
            }
        }
    }

    private int GetNextFollowIndex()
    {
        int highestIndex = -1;

        foreach (var immigrant in immigrants)
        {
            if (immigrant.IsFollowing && immigrant.followIndex > highestIndex)
            {
                highestIndex = immigrant.followIndex;
            }
        }

        return highestIndex + 1;
    }
}
