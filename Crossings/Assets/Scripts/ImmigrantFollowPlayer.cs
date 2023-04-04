using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmigrantFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    void Start () {
        if (GameObject.FindGameObjectWithTag ("Player") != null) {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform> ();
        }
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}

