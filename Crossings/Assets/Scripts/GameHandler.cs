using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameHandler : MonoBehaviour
{
    // Puchase Bools
    private bool ladderPurchased;
    private bool climbingToolPurchased;
    private bool decoyArtPurchased;
    private bool wireClipperPurchased;

    // private int inventoryIndex;

    // // Inventory Spots and array to track what we place in each inventorySpot
    // public GameObject[] inventorySpots;
    // public string[] inventorySpotTracker;

    // // Arrays to Access Inventory Names and Sprite Names
    // string[] names = {"Ladder", "Climbing Tool", "Boat", "DecoyArt", "Wire Clipper", "Bush"};
    // string[] artNames = {"Art/Objects/ladderUIArt", "Art/Objects/climbingToolsArt", "Art/Objects/boatUIArt", "Art/Objects/decoyArt", "Art/Objects/wireClippersArt", "Art/Objects/bush"};

    // Balance Stuff
    const int STARTING_BALANCE = 1050;
    private int bankBalance = STARTING_BALANCE;
    public Text balanceText;


    // The inventory GameObject to toggle its display
    private GameObject theInventory;




    // Start is called before the first frame update
    void Start()
    {
      ladderPurchased = false;
      climbingToolPurchased = false;
      decoyArtPurchased = false;
      wireClipperPurchased = false;

    //   inventoryIndex = 0;

    //   // Find inventorySpots and deactivate all of them on init
    //   inventorySpots = GameObject.FindGameObjectsWithTag("inventorySpot");
    //   inventorySpotTracker = new string[inventorySpots.Length];
    //   for(int i = 0; i < inventorySpots.Length; i++){
    //     inventorySpots[i].SetActive(false);
    //     inventorySpotTracker[i] = "empoty";
    //   }

      // Get BalanceText and Set Balance
      GameObject[] arr = GameObject.FindGameObjectsWithTag("balanceText");
      balanceText = arr[0].GetComponent<Text>();
      setBankBalance(bankBalance);

      // Get Inventory Game Objects and Toggle Display Inventory On/Off
      arr = GameObject.FindGameObjectsWithTag("Inventory");
      theInventory = arr[0];
      theInventory.SetActive(true);

      // Testing Runs
      // purchaseBush();
      // purchaseWireClipper();
      //
      // purchaseLadder();
      // purchaseLadder();
      // purchaseClimbingTool();
      //
      // purchaseDecoyArt();
      // purchaseBoat();


    }


    public void purchaseWireClipper(){
      if(!wireClipperPurchased){
        // display image 


        // // Get Image and Text of the Inventory Spot to Use
        // GameObject currText = inventorySpots[inventoryIndex].transform.GetChild(0).gameObject;
        // GameObject currImage = inventorySpots[inventoryIndex].transform.GetChild(1).gameObject;

        // Text buttonText = currText.GetComponent<Text>();
        // Image buttonImage = currImage.GetComponent<Image>();

        // // Debug.Log("Text: " + buttonText.text);
        // // Debug.Log("Image Name: " + buttonImage.sprite.name);

        // // Assign the actual values
        // buttonText.text = names[4];
        // buttonImage.sprite = Resources.Load<Sprite>(artNames[4]);

        // // Debug.Log("New Text: " + buttonText.text);
        // // Debug.Log("New Image Name: " + buttonImage.sprite.name);

        // // InventorySpot Tracking
        // inventorySpotTracker[inventoryIndex] = names[4];

        // // Activate and set bool
        // inventorySpots[inventoryIndex].SetActive(true);

        wireClipperPurchased = true;
        
        //inventoryIndex++;
      }
    }


    public void purchaseDecoy(){
      if(!decoyArtPurchased){
        // display image 
        // update counter beneath 

        // // Get Image and Text of the Inventory Spot to Use
        // GameObject currText = inventorySpots[inventoryIndex].transform.GetChild(0).gameObject;
        // GameObject currImage = inventorySpots[inventoryIndex].transform.GetChild(1).gameObject;

        // Text buttonText = currText.GetComponent<Text>();
        // Image buttonImage = currImage.GetComponent<Image>();

        // // Debug.Log("Text: " + buttonText.text);
        // // Debug.Log("Image Name: " + buttonImage.sprite.name);

        // // Assign the actual values
        // buttonText.text = names[3];
        // buttonImage.sprite = Resources.Load<Sprite>(artNames[3]);

        // // InventorySpot Tracking
        // inventorySpotTracker[inventoryIndex] = names[3];

        // // Debug.Log("New Text: " + buttonText.text);
        // // Debug.Log("New Image Name: " + buttonImage.sprite.name);

        // // Activate and set bool
        // inventorySpots[inventoryIndex].SetActive(true);
        
        decoyArtPurchased = true;
        
        //inventoryIndex++;
      }
      else{
          // add to counter 
      }
    }

    public void purchaseClimbingTool(){
      if(!climbingToolPurchased){
        // display image 

        // // Get Image and Text of the Inventory Spot to Use
        // GameObject currText = inventorySpots[inventoryIndex].transform.GetChild(0).gameObject;
        // GameObject currImage = inventorySpots[inventoryIndex].transform.GetChild(1).gameObject;

        // Text buttonText = currText.GetComponent<Text>();
        // Image buttonImage = currImage.GetComponent<Image>();

        // // Debug.Log("Text: " + buttonText.text);
        // // Debug.Log("Image Name: " + buttonImage.sprite.name);

        // // Assign the actual values
        // buttonText.text = names[1];
        // buttonImage.sprite = Resources.Load<Sprite>(artNames[1]);
        // //
        // // Debug.Log("New Text: " + buttonText.text);
        // // Debug.Log("New Image Name: " + buttonImage.sprite.name);

        // // InventorySpot Tracking
        // inventorySpotTracker[inventoryIndex] = names[1];

        // // Activate and set bool
        // inventorySpots[inventoryIndex].SetActive(true);

        climbingToolPurchased = true;
        
        //inventoryIndex++;
      }
    }


    public void purchaseLadder(){
      if(!ladderPurchased){
        // display image 

        // // Get Image and Text of the Inventory Spot to Use
        // GameObject currText = inventorySpots[inventoryIndex].transform.GetChild(0).gameObject;
        // GameObject currImage = inventorySpots[inventoryIndex].transform.GetChild(1).gameObject;

        // Text buttonText = currText.GetComponent<Text>();
        // Image buttonImage = currImage.GetComponent<Image>();

        // // Debug.Log("Text: " + buttonText.text);
        // // Debug.Log("Image Name: " + buttonImage.sprite.name);

        // // Assign the actual values
        // buttonText.text = names[0];
        // buttonImage.sprite = Resources.Load<Sprite>(artNames[0]);

        // // InventorySpot Tracking
        // inventorySpotTracker[inventoryIndex] = names[0];


        // // Debug.Log("New Text: " + buttonText.text);
        // // Debug.Log("New Image Name: " + buttonImage.sprite.name);

        // // Activate and set bool
        // inventorySpots[inventoryIndex].SetActive(true);

        ladderPurchased = true;
        
        //inventoryIndex++;
      }
    }



    public void setBankBalance(int newBalance){
      if(newBalance >= 0){
        bankBalance = newBalance;
        balanceText.text = newBalance.ToString();
      }
      else{
        //Debug.Log("Failed to setBankBalance, newBalance < 0");
      }
    }

    public int getCurrentBankBalance(){
      return bankBalance;
    }

}
