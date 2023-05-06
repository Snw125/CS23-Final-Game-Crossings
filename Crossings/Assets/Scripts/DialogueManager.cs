using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    public GameObject thisUIGameObject;// togle on and off
    public Text dialogueText;
    public float characterDelay = 0.05f;
    public float clearDelay = 4f;

    private string[] dialogueParts;
    private int currentPartIndex = 0;

    private int randomIndex = 0;
    System.Random rnd = new System.Random();

    public Image avatarImage;
    public int avatarIndex = 1;
    string[] artNames = {"Art/People/frontPlayerArt3", "Art/People/frontPatrolArt3"};

    private void Start()
    {

      thisUIGameObject.SetActive(true);
      avatarImage.sprite = Resources.Load<Sprite>(artNames[1]);
      avatarIndex = 1;

        string [,] allDialogues = {
            {"Hey you there! What are you doing out here so late?", "Oh, just taking a walk. It's a nice night, isn't it?", "Damn right, nice night for you to go prison!"},
            {"Stop right there! What's in the backpack?", "Just some clothes and food. We're camping nearby.", "Well tonight you're gonna be camping in prison!"},
            {"Stop right there! What's in the backpack?", "Just some clothes and food. We're camping nearby.", "Well tonight you're gonna be camping in prison!"},
            {"Hey, what are you doing with those people? Are they undocumented immigrants?", "No, of course not. We're just climbing on a hike.", "Climbing huh, how about you climb on over into this prison cell!"},
            {"Hold it! You can't cross here. This is a restricted area.", "Oh, sorry. We were just looking for a shortcut.", "Well you found one, this shortcut to prison!"},
            {"Hey, what are you doing out here?", "Just enjoying the scenery. It's peaceful out here.", "Not as peaceful as you'll be in this prison cell tonight!"},
            {"Stop right there! You're not authorized to be in this area.", "Oh, sorry. We must have taken a wrong turn somewhere.", "No wrong turns, but you did take the right turn into this prison cell!"}
            // {"What are you doing out here so late?", "Just taking a walk. I couldn't sleep.", "I could help with that. I know a peaceful spot in this prison cell!"}
        };

        randomIndex = rnd.Next(0, 8);
        dialogueParts = new string[allDialogues.GetLength(1)];

        // Get row part of 2D array and convert into 1D
        for (int i = 0; i < allDialogues.GetLength(1); i++) {
            dialogueParts[i] = allDialogues[randomIndex, i];
        }

        // Start the coroutine to display the first part of the dialogue
        StartCoroutine(DisplayDialoguePart(dialogueParts[currentPartIndex]));

        // thisUIGameObject.SetActive(false);
    }

    private IEnumerator DisplayDialoguePart(string dialoguePart)
    {
        // Clear the text object
        dialogueText.text = "";

        // Animate the text one character at a time
        for (int i = 0; i < dialoguePart.Length; i++)
        {
            dialogueText.text += dialoguePart[i];
            yield return new WaitForSeconds(characterDelay);
        }

        // Wait for a delay before clearing the text object and moving on to the next part
        yield return new WaitForSeconds(clearDelay);
        switchAvatar();

        // Move on to the next part of the dialogue
        currentPartIndex++;
        if (currentPartIndex < dialogueParts.Length)
        {
            StartCoroutine(DisplayDialoguePart(dialogueParts[currentPartIndex]));
        }
        else{
          // Finished with all Dialogue
          Debug.Log("At else Part");
          thisUIGameObject.SetActive(false);
        }


    }

    public void switchAvatar(){
      if (avatarIndex == 0){
        avatarImage.sprite = Resources.Load<Sprite>(artNames[1]);
        avatarIndex = 1;
      }
      else{
        avatarImage.sprite = Resources.Load<Sprite>(artNames[0]);
        avatarIndex = 0;
      }

    }
}
