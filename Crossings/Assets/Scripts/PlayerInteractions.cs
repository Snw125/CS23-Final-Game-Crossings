using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    //gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
    
    public PlayerGridMove movement;

    float timer;
    float holdDur;

    //private GameHandler gameHandler;
    public GameObject ZButtonSig;
    public GameObject XButtonSig;
    public GameObject CButtonSig;

    // object sigs here
    // TODO

    public GameObject hideArt;
    
    public GameObject Timer;
    public Image TimeBar;

    public GameObject ShopUI;

    public bool nearShop;
    public bool nearBush;
    public bool nearImm;
    public bool nearFence;
    public bool nearWall;

    public bool hasDecoy;
    public bool hasClip;
    public bool hasClimb;
    public bool hasLadder;

    public float numDecoy;
    public GameObject Decoy;

    public GameObject thingNear;
    public SpriteRenderer thingNearArt;
    public Collider2D thingCol;

    public ImmigrantFollowSpots currImm;

    public bool hidden;
    
    void Start()
    {
        //gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        movement = gameObject.GetComponent<PlayerGridMove>();
        Timer = transform.GetChild(4).transform.GetChild(0).gameObject;
        TimeBar = Timer.transform.GetChild(0).GetComponent<Image>();

        holdDur = 2f;

        ZButtonSig = transform.GetChild(4).transform.GetChild(1).gameObject;
        XButtonSig = transform.GetChild(4).transform.GetChild(5).gameObject;
        CButtonSig = transform.GetChild(4).transform.GetChild(9).gameObject;

        ShopUI = GameObject.FindWithTag("shop");
        hideArt = transform.GetChild(5).gameObject;
        

        ZButtonSig.SetActive(false);
        XButtonSig.SetActive(false);
        CButtonSig.SetActive(false);

        ShopUI.SetActive(false);
        Timer.SetActive(false);
        hideArt.SetActive(false);

        nearShop = false;
        nearBush = false;
        nearImm = false;
        nearFence = false;
        nearWall = false;

        // purchasable 
        hasDecoy = false;
        hasClip = false;
        hasClimb = false;
        hasLadder = false;

        numDecoy = 0;

        hidden = false;
    }


    public void OnTriggerEnter2D(Collider2D other){
            if (other.gameObject.tag == "Bush") 
            {
                ZButtonSig.SetActive(true);
                Timer.SetActive(true);
                thingNear = other.gameObject;
                nearBush = true;

                thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
                thingNearArt.color = Color.yellow;
            }
            if (other.gameObject.tag == "Immigrant") 
            {
                XButtonSig.SetActive(true);
                thingNear = other.gameObject;
                currImm = other.gameObject.GetComponent<ImmigrantFollowSpots>();
                nearImm = true;

                thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
                thingNearArt.color = Color.yellow;
            }
            if (other.gameObject.tag == "Fence") {
                ZButtonSig.SetActive(true);
                // also X if have clip
                Timer.SetActive(true);
                thingNear = other.gameObject;
                nearFence = true;

                //thingNearArt = other.transform.GetChild.GetComponent<TilemapRenderer>();
                //thingNearArt.color = Color.yellow;
            }
            if (other.gameObject.tag == "Wall") {
                if (hasClimb) {
                    ZButtonSig.SetActive(true);
                    // also X if have ladder
                    Timer.SetActive(true);
                    thingNear = other.gameObject;
                    nearWall = true;

                    //thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    // thingNearArt.color = Color.yellow;
                }
            }
            if (other.gameObject.tag == "ShopOverworld") {
                ZButtonSig.SetActive(true);
                nearShop = true;

                thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
                thingNearArt.color = Color.yellow;
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
                        thingCol = thingNear.GetComponent<Collider2D>();
                        thingCol.enabled = !thingCol.enabled;
                        Vector2 newpos = new Vector2 (thingNear.transform.position.x, thingNear.transform.position.y + .2f); 
                        movement.movePoint.position = newpos;
                        //transform.position = newpos;
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

            if (nearFence) {
                // JUMP 
                if (Input.GetKeyDown(KeyCode.Z)) {
                    // Player will jump 
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
                        // tp the player to other side 

                        Debug.Log(thingNear);

                        // you cant do this. Use player orientation !!!
                        if (transform.position.x < thingNear.transform.position.x) {
                            // tp the player to the right 2 tiles
                            Vector2 newpos = new Vector2 (thingNear.transform.position.x + 1f, thingNear.transform.position.y); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }
                        else if (transform.position.x > thingNear.transform.position.x) {
                            // tp the player to the left 2 tiles
                            Vector2 newpos = new Vector2 (thingNear.transform.position.x - 1f, thingNear.transform.position.y); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }
                        else if (transform.position.y < thingNear.transform.position.y) {
                            // tp the player up 2 tiles
                            Vector2 newpos = new Vector2 (thingNear.transform.position.x, thingNear.transform.position.y + 1f); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }
                        else if (transform.position.y > thingNear.transform.position.y) {
                            // tp the player down 2 tiles
                            Vector2 newpos = new Vector2 (thingNear.transform.position.x, thingNear.transform.position.y - 1f); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }
                    }
                }
                
                
                
                // BREAK 
                else if (Input.GetKeyDown(KeyCode.X)) {
                    // Player will break
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
                        // destroy the tile
                        // bruh idk ???

                        // Destroy(thingNear);
                        // instantiate boom  
                    }
                }
                else
                {
                    timer = float.PositiveInfinity;
                }


                // update timer visually 
                TimeBar.fillAmount = 1 - (Time.time - timer)/holdDur;
                // CLARIFY WHICH IS BEING PRESSED !!!
            }

            if (nearWall) {
                // both tp. Ladder tp's with imms.
                if (Input.GetKeyDown(KeyCode.Z)) {
                    // Player will climb
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
                        // tp the player to the wall 
                        thingCol = thingNear.GetComponent<Collider2D>();
                        thingCol.enabled = !thingCol.enabled;
                        Vector2 newpos = new Vector2 (thingNear.transform.position.x, thingNear.transform.position.y); 
                        movement.movePoint.position = newpos;
                        //transform.position = newpos;

                        // leave immigrants behind!!!
                    }
                }


                else if (Input.GetKeyDown(KeyCode.X)) {
                    // Player will use ladder
                    timer = Time.time;
                }
                else if(Input.GetKey(KeyCode.X))
                {
                    if(Time.time - timer > holdDur)
                    {   
                        //by making it positive inf, we won't subsequently run this code by accident,
                        //since X - +inf = -inf, which is always less than holdDur
                        timer = float.PositiveInfinity;
                    
                        //perform your action
                        // tp the player to the wall 
                        thingCol = thingNear.GetComponent<Collider2D>();
                        thingCol.enabled = !thingCol.enabled;
                        Vector2 newpos = new Vector2 (thingNear.transform.position.x, thingNear.transform.position.y); 
                        movement.movePoint.position = newpos;
                        //transform.position = newpos;

                        // tp immigrants as well ??????
                    }
                }
                else
                {
                    timer = float.PositiveInfinity;
                }


                // update timer visually 
                TimeBar.fillAmount = 1 - (Time.time - timer)/holdDur;
                // CLARIFY WHICH IS BEING PRESSED !!!
            }
            



            if (hidden) {
                    // signify you can leave bush with Z (another button signifier?)
                    // check if they move if they do get them out of bush
                    if (Input.GetKeyDown(KeyCode.Z)) {
                            // get em out
                            hidden = false;
                            hideArt.SetActive(false);
                            Vector2 newpos = new Vector2 (transform.position.x, transform.position.y + 1f); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                            thingCol.enabled = !thingCol.enabled;
                    }
            }

            if (hasDecoy) {
                    if (Input.GetKeyDown(KeyCode.C)) {
                        Vector2 decoypos = new Vector2 (transform.position.x, transform.position.y + 1f);
                        Instantiate(Decoy, decoypos, Quaternion.identity);
                    }
            }
    }

    public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Bush") 
        {
            if (!hidden) {
                thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
                thingNearArt.color = Color.white;

                ZButtonSig.SetActive(false);
                Timer.SetActive(false);
                thingNear = null;
                thingCol = null;
                nearBush = false;
            }
        }
        if (other.gameObject.tag == "Immigrant") 
        {
            thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
            thingNearArt.color = Color.white;

            XButtonSig.SetActive(false);
            thingNear = null;
            currImm = null;
            nearImm = false;
        }
        if (other.gameObject.tag == "Fence") {
            //thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
            //thingNearArt.color = Color.white;

            ZButtonSig.SetActive(false);
            Timer.SetActive(false);
            thingNear = null;
            nearFence = false;
        }
        if (other.gameObject.tag == "Wall") {
            if (hasClimb) {
                //thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
                //thingNearArt.color = Color.white;
                
                ZButtonSig.SetActive(false);
                // also X if have ladder
                Timer.SetActive(false);
                thingNear = null;
                nearWall = false;
            }
        }
        if (other.gameObject.tag == "ShopOverworld") {
            thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
            thingNearArt.color = Color.white;
            
            ZButtonSig.SetActive(false);
            nearShop = false;
        }

    }

}
