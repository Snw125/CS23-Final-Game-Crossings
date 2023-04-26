using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushTrigger : MonoBehaviour
{
    public GameObject player;
    public PlayerInteractions playerStates;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerStates = player.GetComponent<PlayerInteractions>();
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        
    }

    public void OnTriggerExit2D(Collider2D other) 
    {
        
    }
}
