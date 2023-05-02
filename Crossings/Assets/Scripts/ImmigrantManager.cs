using System.Collections.Generic;
using UnityEngine;

public class ImmigrantManager : MonoBehaviour
{
    public List<ImmigrantFollowSpots_new> immigrants;
    public List<GameObject> immsFollowing;

    private PlayerGridMove playerGridMove;

    private void Start() 
    {
        playerGridMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGridMove>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (var immigrant in immigrants)
            {
                //Debug.Log(immigrant.playerGridMove);
                float distanceToPlayer = Vector3.Distance(immigrant.transform.position, playerGridMove.movePoint.position);

                if (!immigrant.IsFollowing && distanceToPlayer <= 2.0f)
                {
                    immsFollowing.Add(immigrant.gameObject);
                    immigrant.followIndex = GetNextFollowIndex();
                    immigrant.IsFollowing = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
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
