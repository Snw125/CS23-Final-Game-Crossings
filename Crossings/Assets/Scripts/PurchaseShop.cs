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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void purchaseOne(){
      if (!itemOnePurchased){
        Debug.Log("Buying Item 1, balance: " + balance);
        if (balance >= 75){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";

          itemOnePurchased = true;
          Debug.Log("Bought item 1 successfully");
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
        Debug.Log("Already purchases Item 1");
        feedbackText.color = Color.blue;
        feedbackText.text = "Already in Inventory";
      }
    }

    public void purchaseTwo(){
      if (!itemTwoPurchased){
        Debug.Log("Buying Item 2, balance: " + balance);
        if (balance >= 75){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";
          itemTwoPurchased = true;
          Debug.Log("Bought item 2 successfully");
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
        Debug.Log("Already purchased Item 2");
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
          Debug.Log("Bought item 3 successfully");
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
        Debug.Log("Already purchased Item 3");
        feedbackText.color = Color.blue;
        feedbackText.text = "Already in Inventory";
      }
    }

    public void purchaseFour(){
      if (!itemFourPurchased){
        Debug.Log("Buying Item 4, balance: " + balance);
        if (balance >= 75){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";

          itemFourPurchased = true;
          Debug.Log("Bought item 4 successfully");
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
        Debug.Log("Already purchases Item 4");
        feedbackText.color = Color.blue;
        feedbackText.text = "Already in Inventory";
      }
    }

    public void purchaseFive(){
      if (!itemFivePurchased){
        Debug.Log("Buying Item 5, balance: " + balance);
        if (balance >= 75){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";

          itemFivePurchased = true;
          Debug.Log("Bought item 5 successfully");
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
        Debug.Log("Already purchased Item 5");
        feedbackText.color = Color.blue;
        feedbackText.text = "Already in Inventory";
      }
    }

    public void purchaseSix(){
      if (!itemSixPurchased){
        Debug.Log("Buying Item 6, balance: " + balance);
        if (balance >= 75){
          feedbackText.color = Color.green;
          feedbackText.text = "Successful Purchase!";

          itemSixPurchased = true;
          Debug.Log("Bought item 1 successfully");
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
        Debug.Log("Already purchases Item 6");
        feedbackText.color = Color.blue;
        feedbackText.text = "Already in Inventory";
      }
    }

    public void ExitButton(){
      Debug.Log("Exit Button Clicked");
      feedbackText.color = Color.red;
      feedbackText.text = "Exiting, BYE!";
    }
}
