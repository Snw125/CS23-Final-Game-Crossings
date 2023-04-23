using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    //gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
    
    float timer;
    float holdDur;

    //private GameHandler gameHandler;
    public GameObject XButtonSig;
    public GameObject ZButtonSig;
    public GameObject hideArt;
    
    public GameObject Timer;
    public Image TimeBar;

    public GameObject ShopUI;

    public bool nearShop;
    public bool nearBush;
    public bool nearImm;

    public GameObject thingNear;
    public SpriteRenderer thingNearArt;
    public Collider2D thingCol;

    public ImmigrantFollowSpots currImm;

    public bool hidden;
    
    void Start()
    {
        //gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();

        Timer = transform.GetChild(4).transform.GetChild(0).gameObject;
        TimeBar = Timer.transform.GetChild(0).GetComponent<Image>();

        holdDur = 2f;

        XButtonSig = transform.GetChild(2).gameObject;
        ZButtonSig = transform.GetChild(3).gameObject;
        ShopUI = GameObject.FindWithTag("shop");
        hideArt = transform.GetChild(5).gameObject;
        
        XButtonSig.SetActive(false);
        ZButtonSig.SetActive(false);
        ShopUI.SetActive(false);
        Timer.SetActive(false);
        hideArt.SetActive(false);

        nearShop = false;
        nearBush = false;
        nearImm = false;

        hidden = false;
    }


    public void OnTriggerEnter2D(Collider2D other){
            if (other.gameObject.tag == "Bush") {
                    // display button signifier
                    ZButtonSig.SetActive(true);
                    Timer.SetActive(true);
                    nearBush = true;
                    thingNear = other.gameObject;
            }
            if (other.gameObject.tag == "Fence") {
                    // display button signifier
                    XButtonSig.SetActive(true);
                    Timer.SetActive(true);
                    thingNear = other.gameObject;
            }
            if (other.gameObject.tag == "ShopOverworld") {
                    ZButtonSig.SetActive(true);
                    nearShop = true;
            }
    }

    void Update() 
    {
            if (nearShop) {
                    if (Input.GetKeyDown(KeyCode.Z)) {
                            Time.timeScale = 0f;
                            ShopUI.SetActive(true);
                    }
            }

            if (nearBush) {
                if (Input.GetKeyDown(KeyCode.Z)) {
                        timer = Time.time;
                }
                else if(Input.GetKey(KeyCode.Z))
                {
                    if(Time.time - timer > holdDur)
                    {   
                        //by making it positive inf, we won't subsequently run this code by accident,
                        //since X - +inf = -inf, which is always less than holdDur
                        timer = float.PositiveInfinity;
                    
                        //perform your action
                        hidden = true;
                        Debug.Log("hidden!");
                        Timer.SetActive(false);

                        // make player darker 
                        hideArt.SetActive(true);

                        // lock player to bush
                        thingCol = thingNear.GetComponents<Collider2D>()[1];
                        thingCol.enabled = !thingCol.enabled;
                        Vector2 newpos = new Vector2 (thingNear.transform.position.x, thingNear.transform.position.y + .2f); 
                        transform.position = newpos;
                        // go to hidden code
                    }
                }
                else
                {
                    timer = float.PositiveInfinity;
                }
                // update timer visually 
                TimeBar.fillAmount = 1 - (Time.time - timer)/holdDur;
            }

            if (nearImm) {
                if (Input.GetKeyDown(KeyCode.X)) {
                    if (currImm.getIsFollow())
                    {
                        currImm.StopFollow();
                    }
                    else {
                        currImm.FollowPlayer();
                    }
                }
            }

            if (hidden) {
                    // signify you can leave bush with Z (another button signifier?)
                    // check if they move if they do get them out of bush
                    if (Input.GetKeyDown(KeyCode.Z)) {
                            // get em out
                            hidden = false;
                            hideArt.SetActive(false);
                            // after timer to make more seemless??
                            // thingCol.enabled = !thingCol.enabled;
                    }
                    // transform.position = bush position
            }
    }

    public void OnTriggerExit2D(Collider2D other){
            XButtonSig.SetActive(false);
            ZButtonSig.SetActive(false);
            Timer.SetActive(false);
            
            nearShop = false;
            nearBush = false;
            nearImm = false;
    }

}
