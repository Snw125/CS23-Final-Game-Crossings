using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseShop : MonoBehaviour
{
    static public bool itemOnePurchased;
    static public bool itemTwoPurchased;
    static public bool itemThreePurchased;
    static public bool itemFourPurchased;
    static public bool itemFivePurchased;
    static public bool itemSixPurchased;

    static public int balance;

    public Text feedbackText;
    public Text balanceText;


    public GameObject ShopUI;

    // Start is called before the first frame update
    void Start()
    {
      itemOnePurchased = false;
      itemTwoPurchased = false;
      itemThreePurchased = false;
      itemFourPurchased = false;
      itemFivePurchased = false;
      itemSixPurchased = false;

      balance = 300;
      balanceText.text = balance.ToString();

      ShopUI = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void purchaseOne(){
      if (!itemOnePurchased){
        Debug.Log("Buying Ladder, balance: " + balance);
        if (balance >= 75){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";

          itemOnePurchased = true;
          Debug.Log("Bought ladder successfully");
          balance-= 75;
          balanceText.text = balance.ToString();

        }
        else{
          Debug.Log("Cannot buy, need more money");
          feedbackText.color = Color.red;
          feedbackText.text = "Error, Insufficient funds";
        }

      }
      else{
        Debug.Log("Already purchases Ladder");
        feedbackText.color = Color.blue;
        feedbackText.text = "Already in Inventory";
      }
    }

    public void purchaseTwo(){
      if (!itemTwoPurchased){
        Debug.Log("Buying Climbing Tool, balance: " + balance);
        if (balance >= 75){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";
          itemTwoPurchased = true;
          Debug.Log("Bought Climbing tool successfully");
          balance -= 75;
          balanceText.text = balance.ToString();

        }
        else{
          Debug.Log("Cannot buy, need more money");
          feedbackText.color = Color.red;
          feedbackText.text = "Error, Insufficient funds";
        }

      }
      else{
        Debug.Log("Already purchased Climbing tool");
        feedbackText.color = Color.blue;
        feedbackText.text = "Already in Inventory";
      }
    }

    public void purchaseThree(){
      if (!itemThreePurchased){
        Debug.Log("Buying Item 3, balance: " + balance);
        if (balance >= 75){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";

          itemThreePurchased = true;
          Debug.Log("Bought boat successfully");
          balance-= 75;
          balanceText.text = balance.ToString();

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
      if (!itemFourPurchased){
        Debug.Log("Buying Decoy Art, balance: " + balance);
        if (balance >= 75){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";

          itemFourPurchased = true;
          Debug.Log("Bought Decoy Art successfully");
          balance-= 75;
          balanceText.text = balance.ToString();

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

    public void purchaseFive(){
      if (!itemFivePurchased){
        Debug.Log("Buying Wireclipper, balance: " + balance);
        if (balance >= 75){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";

          itemFivePurchased = true;
          Debug.Log("Bought Wireclipper successfully");
          balance-= 75;
          balanceText.text = balance.ToString();

        }
        else{
          Debug.Log("Cannot buy, need more money");
          feedbackText.color = Color.red;
          feedbackText.text = "Error, Insufficient funds";
        }

      }
      else{
        Debug.Log("Already purchased Wireclipper");
        feedbackText.color = Color.blue;
        feedbackText.text = "Already in Inventory";
      }
    }

    public void purchaseSix(){
      if (!itemSixPurchased){
        Debug.Log("Buying Bush, balance: " + balance);
        if (balance >= 75){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";

          itemSixPurchased = true;
          Debug.Log("Bought bush successfully");
          balance-= 75;
          balanceText.text = balance.ToString();

        }
        else{
          Debug.Log("Cannot buy, need more money");
          feedbackText.color = Color.red;
          feedbackText.text = "Error, Insufficient funds";
        }

      }
      else{
        Debug.Log("Already purchased Bush");
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
