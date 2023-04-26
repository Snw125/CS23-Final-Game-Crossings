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
    public Text balanceText;


    public GameObject ShopUI;

    public GameObject GameHandler;

    // Start is called before the first frame update
    void Start()
    {
        WireClipPurchased = false;
        DecoyPurchased = false;
        ClimbToolPurchased = false;
        LadderPurchased = false;

        ShopUI = transform.parent.gameObject;

        GameObject[] arr = GameObject.FindGameObjectsWithTag("GameController");
        GameHandler = arr[0];
        balance = GameHandler.GetComponent<GameHandler>().getCurrentBankBalance();
        balanceText.text = balance.ToString();
    }

    public void purchaseOne(){
      if (!WireClipPurchased){
        //Debug.Log("Buying wire clippers, balance: " + balance);
        if (balance >= 75){
            feedbackText.color = Color.green;
            feedbackText.text = "Successful Purchase!";

            WireClipPurchased = true;
            //Debug.Log("Bought wire clippers successfully");
            
            balance-= 75;
            balanceText.text = balance.ToString();
            GameHandler.GetComponent<GameHandler>().setBankBalance(balance);
            GameHandler.GetComponent<GameHandler>().purchaseWireClipper();
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
        if (balance >= 75){
            feedbackText.color = Color.green;
            feedbackText.text = "Successful Purchase!";
            
            DecoyPurchased = true;
            //Debug.Log("Bought Decoy successfully");
            
            balance -= 75;
            balanceText.text = balance.ToString();
            GameHandler.GetComponent<GameHandler>().setBankBalance(balance);
            GameHandler.GetComponent<GameHandler>().purchaseDecoy();
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
        Debug.Log("Buying Item 3, balance: " + balance);
        if (balance >= 75){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";

          ClimbToolPurchased = true;
          Debug.Log("Bought boat successfully");
          balance-= 75;
          balanceText.text = balance.ToString();
          GameHandler.GetComponent<GameHandler>().setBankBalance(balance);
          GameHandler.GetComponent<GameHandler>().purchaseBoat();

        }
        else{
          Debug.Log("Cannot buy, need more money");
          feedbackText.color = Color.red;
          feedbackText.text = "Error, Insufficient funds";
        }

      }
      else{
        Debug.Log("Already purchased Boat");
        feedbackText.color = Color.blue;
        feedbackText.text = "Already in Inventory";
      }
    }

    public void purchaseFour(){
      if (!LadderPurchased){
        Debug.Log("Buying Decoy Art, balance: " + balance);
        if (balance >= 75){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";

          LadderPurchased = true;
          Debug.Log("Bought Decoy Art successfully");
          balance-= 75;
          balanceText.text = balance.ToString();
          GameHandler.GetComponent<GameHandler>().setBankBalance(balance);
          GameHandler.GetComponent<GameHandler>().purchaseDecoyArt();

        }
        else{
          Debug.Log("Cannot buy, need more money");
          feedbackText.color = Color.red;
          feedbackText.text = "Error, Insufficient funds";
        }

      }
      else{
        Debug.Log("Already purchased Decoy Art");
        feedbackText.color = Color.blue;
        feedbackText.text = "Already in Inventory";
      }
    }

    // public void purchaseFive(){
    //   if (!itemFivePurchased){
    //     Debug.Log("Buying Wireclipper, balance: " + balance);
    //     if (balance >= 75){
    //       feedbackText.color = Color.green;
    //       feedbackText.text = "Successful Purchase!";

    //       itemFivePurchased = true;
    //       Debug.Log("Bought Wireclipper successfully");
    //       balance-= 75;
    //       balanceText.text = balance.ToString();
    //       GameHandler.GetComponent<GameHandler>().setBankBalance(balance);
    //       GameHandler.GetComponent<GameHandler>().purchaseWireClipper();

    //     }
    //     else{
    //       Debug.Log("Cannot buy, need more money");
    //       feedbackText.color = Color.red;
    //       feedbackText.text = "Error, Insufficient funds";
    //     }

    //   }
    //   else{
    //     Debug.Log("Already purchased Wireclipper");
    //     feedbackText.color = Color.blue;
    //     feedbackText.text = "Already in Inventory";
    //   }
    // }

    // public void purchaseSix(){
    //   if (!itemSixPurchased){
    //     Debug.Log("Buying Bush, balance: " + balance);
    //     if (balance >= 75){
    //       feedbackText.color = Color.green;
    //       feedbackText.text = "Successful Purchase!";

    //       itemSixPurchased = true;
    //       Debug.Log("Bought bush successfully");
    //       balance-= 75;
    //       balanceText.text = balance.ToString();
    //       GameHandler.GetComponent<GameHandler>().setBankBalance(balance);
    //       GameHandler.GetComponent<GameHandler>().purchaseBush();

    //     }
    //     else{
    //       Debug.Log("Cannot buy, need more money");
    //       feedbackText.color = Color.red;
    //       feedbackText.text = "Error, Insufficient funds";
    //     }

    //   }
    //   else{
    //     Debug.Log("Already purchased Bush");
    //     feedbackText.color = Color.blue;
    //     feedbackText.text = "Already in Inventory";
    //   }
    // }

    public void ExitButton(){
      Debug.Log("Exit Button Clicked");
    //   feedbackText.color = Color.red;
    //   feedbackText.text = "Exiting, BYE!";
      Time.timeScale = 1f;
      ShopUI.SetActive(false);

    }
}
