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
    private bool decoyPurchased;
    private bool wireClipperPurchased;

    
    private int inventoryIndex;

    // // Inventory Spots and array to track what we place in each inventorySpot
    public GameObject[] inventorySpots;
    public string[] inventorySpotTracker;

    // // Arrays to Access Inventory Names and Sprite Names
    string[] names = {"Ladder", "Climbing Tool", "Boat", "DecoyArt", "Wire Clipper", "Bush"};
    string[] artNames = {"Art/Objects/ladderUIArt", "Art/Objects/climbingToolsArt", "Art/Objects/boatUIArt", "Art/Objects/decoyArt", "Art/Objects/wireClippersArt", "Art/Objects/bush"};


    // The inventory GameObject to toggle its display
    private GameObject theInventory;

    private PlayerInteractions interact; 
    private GameObject player; 

    private GameObject ClipImg;
    private GameObject DecoyImg;
    private GameObject ClimbImg;
    private GameObject LadderImg;

    private GameObject AmtDecoy;
    public Text AmtDecoytxt;

    // Balance Stuff
    const int STARTING_BALANCE = 1000;
    private int bankBalance = STARTING_BALANCE;
    public GameObject balance; 
    public Text balanceText;


    // Start is called before the first frame update
    void Start()
    {
        ladderPurchased = false;
        climbingToolPurchased = false;
        decoyPurchased = false;
        wireClipperPurchased = false;

        inventoryIndex = 0;

        

        player = GameObject.FindGameObjectWithTag("Player");      
        interact = player.GetComponent<PlayerInteractions>();

        // Get Inventory Game Objects and Toggle Display Inventory On/Off
        theInventory = player.transform.GetChild(2).transform.GetChild(2).gameObject;

        // Find inventorySpots and deactivate all of them on init
        
        // inventorySpots = GameObject.FindGameObjectsWithTag("inventorySpot");
        // inventorySpotTracker = new string[inventorySpots.Length];
        // for(int i = 0; i < inventorySpots.Length; i++){
        //     inventorySpots[i].SetActive(false);
        //     inventorySpotTracker[i] = "empty";
        // }        
        
        ClipImg = theInventory.transform.GetChild(0).gameObject;
        DecoyImg = theInventory.transform.GetChild(1).gameObject;
        ClimbImg = theInventory.transform.GetChild(2).gameObject;
        LadderImg = theInventory.transform.GetChild(3).gameObject;

        ClipImg.SetActive(false);
        DecoyImg.SetActive(false);
        ClimbImg.SetActive(false);
        LadderImg.SetActive(false);

        AmtDecoy = theInventory.transform.GetChild(4).gameObject;
        AmtDecoytxt = AmtDecoy.GetComponent<Text>();

        AmtDecoy.SetActive(false);

        // Get BalanceText and Set Balance
        balance = theInventory.transform.GetChild(6).gameObject; 
        balanceText = balance.GetComponent<Text>();
        setBankBalance(bankBalance); 

        //   GameObject[] arr = GameObject.FindGameObjectsWithTag("balanceText");
        //   balanceText = arr[0].GetComponent<Text>();

        //   arr = GameObject.FindGameObjectsWithTag("Inventory");
        //   theInventory = arr[0];
        //   theInventory.SetActive(true);


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

    public void IncreaseBankBalance(int amount)
    {
        bankBalance += amount;
        balanceText.text = bankBalance.ToString();
    }


    public void purchaseWireClipper(){
        if(!wireClipperPurchased){
            wireClipperPurchased = true;
            interact.hasClip = true;

            // display image 
            ClipImg.SetActive(true);
        
            // // Get Image and Text of the Inventory Spot to Use
            // GameObject currText = inventorySpots[inventoryIndex].transform.GetChild(0).gameObject;
            // GameObject currImage = inventorySpots[inventoryIndex].transform.GetChild(1).gameObject;

            // Text buttonText = currText.GetComponent<Text>();
            // Image buttonImage = currImage.GetComponent<Image>();

            // Debug.Log("Text: " + buttonText.text);
            // Debug.Log("Image Name: " + buttonImage.sprite.name);

            // Assign the actual values
            // buttonText.text = names[4];
            // buttonImage.sprite = Resources.Load<Sprite>(artNames[4]);

            // Debug.Log("New Text: " + buttonText.text);
            // Debug.Log("New Image Name: " + buttonImage.sprite.name);

            // // InventorySpot Tracking
            // inventorySpotTracker[inventoryIndex] = names[4];

            // // Activate and set bool
            // inventorySpots[inventoryIndex].SetActive(true);
        
            inventoryIndex++;
        }
    }


    public void purchaseDecoy(){
        if(!decoyPurchased){
            decoyPurchased = true;
            interact.hasDecoy = true;
            interact.numDecoy = 1; 
            
            // display image 
            DecoyImg.SetActive(true);
            AmtDecoy.SetActive(true);

            AmtDecoytxt.text = interact.numDecoy.ToString();
        
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
            
            inventoryIndex++;
        }
        else{
            // add to counter 
            interact.numDecoy++; 
            AmtDecoytxt.text = interact.numDecoy.ToString();
        }
    }

    public void purchaseClimbingTool(){
        if(!climbingToolPurchased){
            climbingToolPurchased = true;
            interact.hasClimb = true;
            
            // display image 
            ClimbImg.SetActive(true);

            // // Get Image and Text of the Inventory Spot to Use
            // GameObject currText = inventorySpots[inventoryIndex].transform.GetChild(0).gameObject;
            // GameObject currImage = inventorySpots[inventoryIndex].transform.GetChild(1).gameObject;

            // Text buttonText = currText.GetComponent<Text>();
            // Image buttonImage = currImage.GetComponent<Image>();

            // Debug.Log("Text: " + buttonText.text);
            // Debug.Log("Image Name: " + buttonImage.sprite.name);

            // Assign the actual values
            // buttonText.text = names[1];
            // buttonImage.sprite = Resources.Load<Sprite>(artNames[1]);

            // Debug.Log("New Text: " + buttonText.text);
            // Debug.Log("New Image Name: " + buttonImage.sprite.name);

            // // InventorySpot Tracking
            // inventorySpotTracker[inventoryIndex] = names[1];

            // // Activate and set bool
            // inventorySpots[inventoryIndex].SetActive(true);
            
            inventoryIndex++;
        }
    }


    public void purchaseLadder(){
        if(!ladderPurchased){
            ladderPurchased = true;
            interact.hasLadder = true;
            
            // display image 
            LadderImg.SetActive(true);

            // // Get Image and Text of the Inventory Spot to Use
            // GameObject currText = inventorySpots[inventoryIndex].transform.GetChild(0).gameObject;
            // GameObject currImage = inventorySpots[inventoryIndex].transform.GetChild(1).gameObject;

            // Text buttonText = currText.GetComponent<Text>();
            // Image buttonImage = currImage.GetComponent<Image>();

            // Debug.Log("Text: " + buttonText.text);
            // Debug.Log("Image Name: " + buttonImage.sprite.name);

            // Assign the actual values
            // buttonText.text = names[0];
            // buttonImage.sprite = Resources.Load<Sprite>(artNames[0]);

            // // InventorySpot Tracking
            // inventorySpotTracker[inventoryIndex] = names[0];

            // Debug.Log("New Text: " + buttonText.text);
            // Debug.Log("New Image Name: " + buttonImage.sprite.name);

            // // Activate and set bool
            // inventorySpots[inventoryIndex].SetActive(true);

            inventoryIndex++;
        }
    }


    public void setBankBalance(int newBalance){
      if(newBalance >= 0){
        bankBalance = newBalance;
        balanceText.text = newBalance.ToString();
      }
      else{
        Debug.Log("Failed to setBankBalance, newBalance < 0");
      }
    }

    public int getCurrentBankBalance(){
      return bankBalance;
    }

}
