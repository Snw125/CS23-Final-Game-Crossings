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
        if (other.gameObject.tag == "Player") 
        {
            playerStates.ZButtonSig.SetActive(true);
            playerStates.Timer.SetActive(true);
            playerStates.thingNear = this.gameObject;
            playerStates.nearBush = true;

            playerStates.thingNearArt = playerStates.thingNear.transform.GetChild(0).GetComponent<SpriteRenderer>();
            playerStates.thingNearArt.color = Color.yellow;
        }
    }

    public void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            if (!playerStates.hidden) {
                playerStates.thingNearArt = playerStates.thingNear.transform.GetChild(0).GetComponent<SpriteRenderer>();
                playerStates.thingNearArt.color = Color.white;

                playerStates.ZButtonSig.SetActive(false);
                playerStates.Timer.SetActive(false);
                playerStates.thingNear = null;
                playerStates.thingCol = null;
                playerStates.nearBush = false;
            }
        }
    }
}
