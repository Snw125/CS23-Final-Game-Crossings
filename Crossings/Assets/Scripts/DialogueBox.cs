using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] GameObject WhateverTextThingy;  //Add reference to UI Text here via the inspector
    private float timeToAppear = 4f;
    private float timeWhenDisappear;

    //Call to enable the text, which also sets the timer
    public void EnableText()
    {

    }

    void Start()
    {
        WhateverTextThingy.SetActive(true);
        timeWhenDisappear = Time.time + timeToAppear;
    }

    //We check every frame if the timer has expired and the text should disappear
    void Update()
    {
        if (Time.time >= timeWhenDisappear)
        {
            WhateverTextThingy.SetActive(false);
        }
    }
}
