using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class PlayerInteractions : MonoBehaviour
{   
    public PlayerGridMove movement;

    float timer;
    public float bushholdDur = 2f;
    public float jumpholdDur = 2f; 
    public float breakholdDur = 2f;
    public float climbholdDur = 2f;
    public float ladholdDur = 2f;

    private GameHandler gameHandler;

    // signifier stuff
    private GameObject ZButtonSig;
    private GameObject XButtonSig;
    private GameObject CButtonSig;

    // Z
    private GameObject BushSig;
    private GameObject ShopSig;
    private GameObject FenceSig;
    private GameObject ClimbSig;

    // X 
    private GameObject ImmSig;
    private GameObject ClipSig;
    private GameObject LadderSig;

    // C
    private GameObject DecoySig;

    // Text 
    private GameObject ZSig;
    private GameObject XSig;
    private GameObject CSig;
    private TMPro.TextMeshProUGUI ZSigtxt;
    private TMPro.TextMeshProUGUI XSigtxt;
    private TMPro.TextMeshProUGUI CSigtxt;


    public GameObject hideArt;
    private Animator anim; 
    
    public GameObject Timer;
    public Image TimeBar;

    public GameObject ShopUI;

    private bool nearShop;
    private bool nearBush;
    public bool nearImm;
    private bool nearFence;
    private bool nearWall;

    public bool hasDecoy;
    public bool hasClip;
    public bool hasClimb;
    public bool hasLadder;

    public bool climbing;
    public bool climbingwithimms;

    public int numDecoy;
    private bool displayDecoy;
    public GameObject Decoy;

    public GameObject thingNear;
    public SpriteRenderer thingNearArt;
    public Collider2D thingCol;

    public ImmigrantManager imms;

    public Tilemap Fencetiles;
    public TilemapCollider2D WallCol; 
    public Tilemap Walltiles;
    public GameObject boom;

    public bool hidden;
    public bool bushhide;

    private SpriteRenderer playerSprite;
    private SpriteRenderer shadowSprite;

    public bool jumpnotbreak;
    public bool climbnotlad;
    
    void Start()
    {
        gameHandler = GameObject.FindWithTag("GameController").GetComponent<GameHandler>();
        movement = gameObject.GetComponent<PlayerGridMove>();
        
        Timer = transform.GetChild(2).transform.GetChild(0).gameObject;
        TimeBar = Timer.transform.GetChild(0).GetComponent<Image>();

        GameObject allSigs = transform.GetChild(2).transform.GetChild(1).gameObject;

        ZButtonSig = allSigs.transform.GetChild(0).gameObject;
        XButtonSig = allSigs.transform.GetChild(5).gameObject;
        CButtonSig = allSigs.transform.GetChild(9).gameObject;

        // Z
        BushSig = allSigs.transform.GetChild(1).gameObject;
        ShopSig = allSigs.transform.GetChild(2).gameObject;
        FenceSig = allSigs.transform.GetChild(3).gameObject;
        ClimbSig = allSigs.transform.GetChild(4).gameObject;

        // X 
        ImmSig = allSigs.transform.GetChild(6).gameObject;
        ClipSig = allSigs.transform.GetChild(7).gameObject;
        LadderSig = allSigs.transform.GetChild(8).gameObject;

        // C
        DecoySig = allSigs.transform.GetChild(10).gameObject;

        // Text 
        ZSig = allSigs.transform.GetChild(11).gameObject;
        ZSigtxt = allSigs.transform.GetChild(11).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        XSig = allSigs.transform.GetChild(12).gameObject;
        XSigtxt = allSigs.transform.GetChild(12).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        CSig = allSigs.transform.GetChild(13).gameObject;
        CSigtxt = allSigs.transform.GetChild(13).gameObject.GetComponent<TMPro.TextMeshProUGUI>();


        ShopUI = GameObject.FindWithTag("shop");
        hideArt = transform.GetChild(3).gameObject;
        anim = gameObject.GetComponentInChildren<Animator>();
        

        ZButtonSig.SetActive(false);
        XButtonSig.SetActive(false);
        CButtonSig.SetActive(false);

        ZSig.SetActive(false);
        XSig.SetActive(false);
        CSig.SetActive(false);

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

        climbing = false;
        climbingwithimms = false;

        numDecoy = 0;
        displayDecoy = false;

        Fencetiles = GameObject.FindGameObjectWithTag("Fence").GetComponent<Tilemap>();
        WallCol = GameObject.FindGameObjectWithTag("Wall").GetComponent<TilemapCollider2D>();
        Walltiles = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();

        hidden = false;
        bushhide = false;

        jumpnotbreak = true;
        climbnotlad = true;

        imms = GameObject.FindGameObjectWithTag("ImmManager").GetComponent<ImmigrantManager>();

        playerSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        shadowSprite = gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>();
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
                BushSig.SetActive(true);
                ZSig.SetActive(true);
                ZSigtxt.text = "Hide";
            }
            if (other.gameObject.tag == "Immigrant") 
            {
                XButtonSig.SetActive(true);
                thingNear = other.gameObject;
                nearImm = true;

                thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
                thingNearArt.color = Color.yellow;
                ImmSig.SetActive(true);
                XSig.SetActive(true);
                XSigtxt.text = "Follow";
            }
            if (other.gameObject.tag == "Fence") {
                Timer.SetActive(true);
                thingNear = other.gameObject;
                nearFence = true;

                //thingNearArt = other.transform.GetChild.GetComponent<TilemapRenderer>();
                //thingNearArt.color = Color.yellow;
                ZButtonSig.SetActive(true);
                FenceSig.SetActive(true);
                ZSig.SetActive(true);
                ZSigtxt.text = "Jump";
                if (hasClip) { 
                    XButtonSig.SetActive(true);
                    ClipSig.SetActive(true);
                    XSig.SetActive(true);
                    XSigtxt.text = "Break";
                }
            }
            if (other.gameObject.tag == "Wall") {
                if (hasClimb) {
                    Timer.SetActive(true);
                    thingNear = other.gameObject;
                    nearWall = true;

                    //thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    // thingNearArt.color = Color.yellow;
                    ZButtonSig.SetActive(true);
                    ClimbSig.SetActive(true);
                    ZSig.SetActive(true);
                    ZSigtxt.text = "Climb";
                    if (hasLadder) { 
                        XButtonSig.SetActive(true);
                        LadderSig.SetActive(true);
                        XSig.SetActive(true);
                        XSigtxt.text = "With Imms";
                    }
                }
            }
            if (other.gameObject.tag == "ShopOverworld") {
                ZButtonSig.SetActive(true);
                nearShop = true;

                thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
                thingNearArt.color = Color.yellow;
                ShopSig.SetActive(true);
                ZSig.SetActive(true);
                ZSigtxt.text = "Shop";
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
                    if(Time.time - timer > bushholdDur)
                    {   
                        //by making it positive inf, we won't subsequently run this code by accident,
                        //since X - +inf = -inf, which is always less than holdDur
                        timer = float.PositiveInfinity;
                    
                        //perform your action
                        hidden = true;
                        bushhide = true;
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

                        // teleport imms 
                        foreach (var immigrant in imms.immsFollowing)
                        {
                            ImmigrantFollowSpots_new followcode = immigrant.GetComponent<ImmigrantFollowSpots_new>();
                            Vector2 immnewpos = new Vector2 (newpos.x, newpos.y + (.1f * (followcode.followIndex + 1)));
                            
                            followcode.IsFollowing = false;
                            immigrant.transform.position = immnewpos;
                        }                     
                        // go to hidden code
                    }
                }
                else
                {
                    timer = float.PositiveInfinity;
                }
                // update timer visually 
                TimeBar.fillAmount = 1 - (Time.time - timer)/bushholdDur;
            }

            // !!! CHANGE TO FIT NEW WALTER CODE 
            // if (nearImm) {
            //     if (Input.GetKeyDown(KeyCode.X)) {
            //         if (currImm.getIsFollow())
            //         {
            //             currImm.StopFollow();
            //         }
            //         else {
            //             currImm.FollowPlayer();
            //         }
            //     }
            // }

            if (nearFence) {
                // JUMP 
                if (Input.GetKeyDown(KeyCode.Z)) {
                    // Player will jump 
                    timer = Time.time;
                    jumpnotbreak = true;
                }
                else if(Input.GetKey(KeyCode.Z))
                {
                    if(Time.time - timer > jumpholdDur)
                    {   
                        //by making it positive inf, we won't subsequently run this code by accident,
                        //since X - +inf = -inf, which is always less than holdDur
                        timer = float.PositiveInfinity;
                    
                        //perform your action
                        // tp the player to other side 

                        // Use player orientation to determine where to jump
                        if (anim.GetBool("Up")) {
                            // tp the player up 2 tiles
                            Vector2 newpos = new Vector2 (transform.position.x, transform.position.y + 2f); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }
                        else if (anim.GetBool("Down")) {
                            // tp the player down 2 tiles
                            Vector2 newpos = new Vector2 (transform.position.x, transform.position.y - 2f); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }
                        else if (anim.GetBool("Right")) {
                            // tp the player to the right 2 tiles
                            Vector2 newpos = new Vector2 (transform.position.x + 2f, transform.position.y); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }
                        else if (anim.GetBool("Left")) {
                            // tp the player to the left 2 tiles
                            Vector2 newpos = new Vector2 (transform.position.x - 2f, transform.position.y); 
                            //Debug.Log(movement.movePoint.position);
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }

                        // leave immigrants behind 
                        foreach (var immigrant in imms.immsFollowing)
                        {
                            ImmigrantFollowSpots_new followcode = immigrant.GetComponent<ImmigrantFollowSpots_new>();
                            followcode.followIndex = 0;
                            followcode.IsFollowing = false;
                        }
                        imms.immsFollowing.Clear();

                    }
                }
                
                
                
                // BREAK 
                // make sure to check they have the clippers!!
                else if (hasClip && Input.GetKeyDown(KeyCode.X)) {
                    // Player will break
                    timer = Time.time;
                    jumpnotbreak = false;
                }
                else if(hasClip && Input.GetKey(KeyCode.X))
                {
                    if(Time.time - timer > breakholdDur)
                    {   
                        //by making it positive inf, we won't subsequently run this code by accident,
                        //since X - +inf = -inf, which is always less than holdDur
                        timer = float.PositiveInfinity;
                    
                        //perform your action
                        // Use player orientation to determine where to BREAK
                        Vector2 breakpos;
                        if (anim.GetBool("Up")) {
                            // break the tile 1 above player
                            breakpos = new Vector2 (transform.position.x, transform.position.y + 1f); 
                            
                        }
                        else if (anim.GetBool("Down")) {
                            // break the tile 1 below player
                            breakpos = new Vector2 (transform.position.x, transform.position.y - 1f); 
                            
                        }
                        else if (anim.GetBool("Right")) {
                            // break the tile to the right of the player
                            breakpos = new Vector2 (transform.position.x + 1f, transform.position.y); 
                            
                        }
                        else if (anim.GetBool("Left")) {
                            // break the tile to the left of the player 
                            breakpos = new Vector2 (transform.position.x - 1f, transform.position.y); 
                            
                        }
                        else {
                            breakpos = new Vector2 (transform.position.x - 1f, transform.position.y);
                        }

                        Fencetiles.SetTile(Fencetiles.WorldToCell(breakpos), null);
                        // destructableTilemap.SetTile(destructableTilemap.WorldToCell(hitPosition), null);
                        
                        // instantiate boom  
                        Instantiate(boom, breakpos, Quaternion.identity);
                    }
                }
                else
                {
                    timer = float.PositiveInfinity;
                }


                // update timer visually 
                if (jumpnotbreak) {
                    TimeBar.fillAmount = 1 - (Time.time - timer)/jumpholdDur;
                }
                else {
                    TimeBar.fillAmount = 1 - (Time.time - timer)/breakholdDur;
                }
                
                // CLARIFY WHICH IS BEING PRESSED !!!
            }

            if (nearWall) {
                // both tp. Ladder tp's with imms.
                if (hasClimb && Input.GetKeyDown(KeyCode.Z)) {
                    // Player will climb
                    timer = Time.time;
                    climbnotlad = true;
                }
                else if(hasClimb && Input.GetKey(KeyCode.Z))
                {
                    if(Time.time - timer > climbholdDur)
                    {   
                        //by making it positive inf, we won't subsequently run this code by accident,
                        //since X - +inf = -inf, which is always less than holdDur
                        timer = float.PositiveInfinity;
                    
                        //perform your action
                        // tp the player to the wall 

                        WallCol.enabled = !WallCol.enabled;
                        // change player's layer to appear above 

                        playerSprite.sortingLayerName = "Overlays";
                        playerSprite.sortingOrder = 100;
                        shadowSprite.sortingLayerName = "Overlays";
                        shadowSprite.sortingOrder = 95;

                        climbing = true;

                        if (anim.GetBool("Up")) {
                            // tp the player up 1 tiles
                            Vector2 newpos = new Vector2 (transform.position.x, transform.position.y + 1f); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }
                        else if (anim.GetBool("Down")) {
                            // tp the player down 1 tiles
                            Vector2 newpos = new Vector2 (transform.position.x, transform.position.y - 1f); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }
                        else if (anim.GetBool("Right")) {
                            // tp the player to the right 1 tiles
                            Vector2 newpos = new Vector2 (transform.position.x + 1f, transform.position.y); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }
                        else if (anim.GetBool("Left")) {
                            // tp the player to the left 1 tiles
                            Vector2 newpos = new Vector2 (transform.position.x - 1f, transform.position.y); 
                            //Debug.Log(movement.movePoint.position);
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }

                        // thingCol = thingNear.GetComponent<Collider2D>();
                        // thingCol.enabled = !thingCol.enabled;
                        // Vector2 newpos = new Vector2 (thingNear.transform.position.x, thingNear.transform.position.y); 
                        // movement.movePoint.position = newpos;
                        //transform.position = newpos;

                        // make player not visible to guards on walls
                        hidden = true;

                        // leave immigrants behind!!!
                        foreach (var immigrant in imms.immsFollowing)
                        {
                            ImmigrantFollowSpots_new followcode = immigrant.GetComponent<ImmigrantFollowSpots_new>();
                            followcode.followIndex = 0;
                            followcode.IsFollowing = false;
                        }
                        imms.immsFollowing.Clear();
                    }
                }


                else if (hasLadder && Input.GetKeyDown(KeyCode.X)) {
                    // Player will use ladder
                    timer = Time.time;
                    climbnotlad = false;
                }
                else if(hasLadder && Input.GetKey(KeyCode.X))
                {
                    if(Time.time - timer > ladholdDur)
                    {   
                        //by making it positive inf, we won't subsequently run this code by accident,
                        //since X - +inf = -inf, which is always less than holdDur
                        timer = float.PositiveInfinity;
                    
                        //perform your action
                        // tp the player to the wall 
                        WallCol.enabled = !WallCol.enabled;
                        // change player's layer to appear above 

                        playerSprite.sortingLayerName = "Overlays";
                        playerSprite.sortingOrder = 100;
                        shadowSprite.sortingLayerName = "Overlays";
                        shadowSprite.sortingOrder = 95;

                        climbingwithimms = true;

                        if (anim.GetBool("Up")) {
                            // tp the player up 1 tiles
                            Vector2 newpos = new Vector2 (transform.position.x, transform.position.y + 1f); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }
                        else if (anim.GetBool("Down")) {
                            // tp the player down 1 tiles
                            Vector2 newpos = new Vector2 (transform.position.x, transform.position.y - 1f); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }
                        else if (anim.GetBool("Right")) {
                            // tp the player to the right 1 tiles
                            Vector2 newpos = new Vector2 (transform.position.x + 1f, transform.position.y); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }
                        else if (anim.GetBool("Left")) {
                            // tp the player to the left 1 tiles
                            Vector2 newpos = new Vector2 (transform.position.x - 1f, transform.position.y); 
                            //Debug.Log(movement.movePoint.position);
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                        }

                        // make player not visible to guards on walls
                        hidden = true;

                        // immigrants stay with 
                        foreach (var immigrant in imms.immsFollowing)
                        {
                            SpriteRenderer currImmSprite = immigrant.GetComponentInChildren<SpriteRenderer>();
                            currImmSprite.sortingLayerName = "Overlays";
                            currImmSprite.sortingOrder = 100;
                            
                            SpriteRenderer immShadowSprite = immigrant.transform.GetChild(1).GetComponent<SpriteRenderer>();
                            immShadowSprite.sortingLayerName = "Overlays";
                            immShadowSprite.sortingOrder = 95;
                        }
                    }
                }
                else
                {
                    timer = float.PositiveInfinity;
                }


                // update timer visually 
                if (climbnotlad) {
                    TimeBar.fillAmount = 1 - (Time.time - timer)/climbholdDur;
                }
                else {
                    TimeBar.fillAmount = 1 - (Time.time - timer)/ladholdDur;
                }
                // CLARIFY WHICH IS BEING PRESSED !!!
            }
            



            if (hidden && bushhide) {
                    // signify you can leave bush with Z (another button signifier?)
                    // check if they move if they do get them out of bush
                    if (Input.GetKeyDown(KeyCode.Z)) {
                            // get em out
                            hidden = false;
                            bushhide = false;
                            hideArt.SetActive(false);
                            Vector2 newpos = new Vector2 (transform.position.x, transform.position.y + 1f); 
                            movement.movePoint.position = newpos;
                            transform.position = newpos;
                            thingCol.enabled = !thingCol.enabled;

                            foreach (var immigrant in imms.immsFollowing)
                            {
                                ImmigrantFollowSpots_new followcode = immigrant.GetComponent<ImmigrantFollowSpots_new>();
                                
                                followcode.IsFollowing = true;
                            }   
                    }
            }

            if (hasDecoy) {
                    if (!displayDecoy) {
                        CButtonSig.SetActive(true);
                        DecoySig.SetActive(true);
                        CSig.SetActive(true);

                        displayDecoy = true;
                    }
                    if (Input.GetKeyDown(KeyCode.C) && numDecoy != 0) {
                        // if facing certain way 
                        Vector2 decoypos;
                        if (anim.GetBool("Up")) {
                            decoypos = new Vector2 (transform.position.x, transform.position.y + 1f);
                        }
                        else if (anim.GetBool("Down")) {
                            decoypos = new Vector2 (transform.position.x, transform.position.y - 1f);
                        }
                        else if (anim.GetBool("Right")) {
                            decoypos = new Vector2 (transform.position.x + 1f, transform.position.y);
                        }
                        else if (anim.GetBool("Left")) {
                            decoypos = new Vector2 (transform.position.x - 1f, transform.position.y);
                        }
                        else {
                            decoypos = new Vector2 (transform.position.x, transform.position.y + 1f);
                        }
                        
                        Instantiate(Decoy, decoypos, Quaternion.identity);

                        numDecoy--;
                        gameHandler.AmtDecoytxt.text = numDecoy.ToString();
                    }
            }
    }

    void FixedUpdate()
    {
        // player is no longer on a wall 
        if (climbing && !Walltiles.HasTile(Walltiles.WorldToCell(gameObject.transform.position))) {
            WallCol.enabled = !WallCol.enabled;

            // change player's layer to appear at ground again
            playerSprite.sortingLayerName = "Ground";
            playerSprite.sortingOrder = 50;
            shadowSprite.sortingLayerName = "Ground";
            shadowSprite.sortingOrder = 45;

            climbing = false;
        }
        if (climbingwithimms && !Walltiles.HasTile(Walltiles.WorldToCell(gameObject.transform.position))) {
            WallCol.enabled = !WallCol.enabled;

            // change player's layer to appear at ground again
            playerSprite.sortingLayerName = "Ground";
            playerSprite.sortingOrder = 50;
            shadowSprite.sortingLayerName = "Ground";
            shadowSprite.sortingOrder = 45;
            
            // change imm layers
            foreach (var immigrant in imms.immsFollowing)
            {
                SpriteRenderer currImmSprite = immigrant.GetComponentInChildren<SpriteRenderer>();
                currImmSprite.sortingLayerName = "Ground";
                currImmSprite.sortingOrder = 50;
                
                SpriteRenderer immShadowSprite = immigrant.transform.GetChild(1).GetComponent<SpriteRenderer>();
                immShadowSprite.sortingLayerName = "Ground";
                immShadowSprite.sortingOrder = 45;
            }

            climbingwithimms = false;
        }
    }

    public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Bush") 
        {
            if (!hidden) {
                thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
                thingNearArt.color = Color.white;

                ZButtonSig.SetActive(false);
                BushSig.SetActive(false);
                ZSig.SetActive(false);
                
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
            ImmSig.SetActive(false);
            XSig.SetActive(false);
            
            thingNear = null;
            nearImm = false;
        }
        if (other.gameObject.tag == "Fence") {
            //thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
            //thingNearArt.color = Color.white;

            ZButtonSig.SetActive(false);
            FenceSig.SetActive(false);
            ZSig.SetActive(false);
            if (hasClip) {
                XButtonSig.SetActive(false);
                ClipSig.SetActive(false);
                XSig.SetActive(false);
            }

            Timer.SetActive(false);
            thingNear = null;
            nearFence = false;
        }
        if (other.gameObject.tag == "Wall") {
            if (hasClimb) {
                //thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
                //thingNearArt.color = Color.white;
                
                ZButtonSig.SetActive(false);
                ClimbSig.SetActive(false);
                ZSig.SetActive(false);
                if (hasLadder) {
                    XButtonSig.SetActive(false);
                    LadderSig.SetActive(false);
                    XSig.SetActive(false);
                }

                Timer.SetActive(false);
                thingNear = null;
                nearWall = false;
            }
        }
        if (other.gameObject.tag == "ShopOverworld") {
            thingNearArt = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
            thingNearArt.color = Color.white;
            
            ZButtonSig.SetActive(false);
            ShopSig.SetActive(false);
            ZSig.SetActive(false);
            nearShop = false;
        }

    }

}
