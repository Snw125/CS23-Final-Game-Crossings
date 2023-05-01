using System.Collections.Generic;
using UnityEngine;

public class ImmigrantManager : MonoBehaviour
{
    public List<ImmigrantFollowSpots_new> immigrants;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (var immigrant in immigrants)
            {
                float distanceToPlayer = Vector3.Distance(immigrant.transform.position, immigrant.playerGridMove.transform.position);

                if (!immigrant.IsFollowing && distanceToPlayer <= 1.0f)
                {
                    immigrant.followIndex = GetNextFollowIndex();
                    immigrant.IsFollowing = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (var immigrant in immigrants)
            {
                float distanceToPlayer = Vector3.Distance(immigrant.transform.position, immigrant.playerGridMove.transform.position);

                if (immigrant.IsFollowing && distanceToPlayer <= 1.0f)
                {
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
