using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmigrantFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}

