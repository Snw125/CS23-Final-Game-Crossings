using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseShop : MonoBehaviour
{
    static public bool WireClipPurchased;
    static public bool DecoyPurchased;
    static public bool ClimbToolPurchased;
    static public bool LadderPurchased;

    static public int balance;

    public Text feedbackText;
    public TMPro.TextMeshProUGUI balanceText;


    public GameObject ShopUI;

    public GameHandler gameHandler;

    // Start is called before the first frame update
    void Start()
    {
        WireClipPurchased = false;
        DecoyPurchased = false;
        ClimbToolPurchased = false;
        LadderPurchased = false;

        ShopUI = transform.parent.gameObject;

        gameHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameHandler>();
        balance = gameHandler.getCurrentBankBalance();
        balanceText.text = balance.ToString();
    }

    void Awake() {
        //Debug.Log(GameHandler.bankBalance);

        gameHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameHandler>();

        balance = gameHandler.getCurrentBankBalance();
        balanceText.text = balance.ToString();
    }

    public void UpdateShop() {
        balance = gameHandler.getCurrentBankBalance();
        balanceText.text = balance.ToString();
    }

    public void purchaseOne(){
      if (!WireClipPurchased){
        //Debug.Log("Buying wire clippers, balance: " + balance);
        if (balance >= 300){
            feedbackText.color = Color.green;
            feedbackText.text = "Successful Purchase!";

            WireClipPurchased = true;
            //Debug.Log("Bought wire clippers successfully");
            
            balance-= 300;
            balanceText.text = balance.ToString();
            gameHandler.setBankBalance(balance);
            gameHandler.purchaseWireClipper();
        }
        else{
            //Debug.Log("Cannot buy, need more money");
            feedbackText.color = Color.red;
            feedbackText.text = "Error, Insufficient funds";
        }
      }
      else{
            //Debug.Log("Already purchased Wire Clippers");
            feedbackText.color = Color.blue;
            feedbackText.text = "Already in Inventory";
      }
    }

// Decoy - not limited
    public void purchaseTwo(){
        //Debug.Log("Buying Decoy, balance: " + balance);
        if (balance >= 100){
            feedbackText.color = Color.green;
            feedbackText.text = "Successful Purchase!";
            
            DecoyPurchased = true;
            //Debug.Log("Bought Decoy successfully");
            
            balance -= 100;
            balanceText.text = balance.ToString();
            gameHandler.setBankBalance(balance);
            gameHandler.purchaseDecoy();
            // add one to decoy counter 
        }
        else{
            //Debug.Log("Cannot buy, need more money");
            feedbackText.color = Color.red;
            feedbackText.text = "Error, Insufficient funds";
        }
    }

    public void purchaseThree(){
      if (!ClimbToolPurchased){
        //Debug.Log("Buying Climbing Tools, balance: " + balance);
        if (balance >= 700){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";

          ClimbToolPurchased = true;
          //Debug.Log("Bought boat successfully");
          
          balance-= 700;
          balanceText.text = balance.ToString();
          gameHandler.setBankBalance(balance);
          gameHandler.purchaseClimbingTool();
        }
        else{
          //Debug.Log("Cannot buy, need more money");
          feedbackText.color = Color.red;
          feedbackText.text = "Error, Insufficient funds";
        }

      }
      else{
        //Debug.Log("Already purchased Boat");
        feedbackText.color = Color.blue;
        feedbackText.text = "Already in Inventory";
      }
    }

    public void purchaseFour(){
      if (!LadderPurchased){
        //Debug.Log("Buying Ladder, balance: " + balance);
        if (balance >= 1000){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";

          LadderPurchased = true;
          //Debug.Log("Bought Decoy Art successfully");
          
          balance-= 1000;
          balanceText.text = balance.ToString();
          gameHandler.setBankBalance(balance);
          gameHandler.purchaseLadder();
        }
        else{
          //Debug.Log("Cannot buy, need more money");
          feedbackText.color = Color.red;
          feedbackText.text = "Error, Insufficient funds";
        }

      }
      else{
        //Debug.Log("Already purchased Decoy Art");
        feedbackText.color = Color.blue;
        feedbackText.text = "Already in Inventory";
      }
    }

    public void ExitButton(){
      Debug.Log("Exit Button Clicked");
    //   feedbackText.color = Color.red;
    //   feedbackText.text = "Exiting, BYE!";
      Time.timeScale = 1f;
      ShopUI.SetActive(false);

    }
}
