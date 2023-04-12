using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    //gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
    
    // TimeBar = Timer.transform.GetChild(0).GetComponent<Image>();
    // Timer.SetActive(true);
    // currtime = time;
    float timer;
    float holdDur;


    //private GameHandler gameHandler;
    public GameObject XButtonSig;
    public GameObject ZButtonSig;
    
    public GameObject Timer;
    public Image TimeBar;

    public GameObject ShopUI;

    public bool nearShop;
    public bool nearBush;

    public bool hidden;
    
    void Start()
    {
        //gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();

        Timer = transform.GetChild(4).transform.GetChild(0).gameObject;
        TimeBar = Timer.transform.GetChild(0).GetComponent<Image>();
        // Timer.SetActive(true);

        holdDur = 2f;

        XButtonSig = transform.GetChild(2).gameObject;
        ZButtonSig = transform.GetChild(3).gameObject;
        ShopUI = GameObject.FindWithTag("shop");
        
        XButtonSig.SetActive(false);
        ZButtonSig.SetActive(false);
        ShopUI.SetActive(false);

        nearShop = false;
        nearBush = false;

        hidden = false;
    }


    public void OnTriggerEnter2D(Collider2D other){
            if (other.gameObject.tag == "Bush") {
                    // display button signifier
                    ZButtonSig.SetActive(true);
                    nearBush = true;
            }
            if (other.gameObject.tag == "Fence") {
                    // display button signifier
                    XButtonSig.SetActive(true);
                    // look for player input in trigger stay 
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
                        // update timer visually 
                        //TimeBar.fillAmount = time/holdDur;

                        //by making it positive inf, we won't subsequently run this code by accident,
                        //since X - +inf = -inf, which is always less than holdDur
                        timer = float.PositiveInfinity;
                    
                        //perform your action
                        hidden = true;
                        Debug.Log("nice!");
                        // lock player to bush
                    }
                }
                else
                {
                    timer = float.PositiveInfinity;
                }
                            
            }
    }

    public void OnTriggerExit2D(Collider2D other){
            XButtonSig.SetActive(false);
            ZButtonSig.SetActive(false);
            
            nearShop = false;
            nearBush = false;
    }

}
