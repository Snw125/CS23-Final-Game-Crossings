using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName; 
    static public bool tutorialComplete = false;
    public GameHandler gameHandler;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            LoadScene(sceneName);
        }
    }

    static public void LoadScene(string sceneName)
    {
        if (sceneName == "Forward") {
            tutorialComplete = true;
            sceneName = "Levels";
        }
        if (sceneName == "Back") {
            if (!tutorialComplete) {
                sceneName = "TutorialLevel";
            }
            else {
                sceneName = "Levels";
            }
        }
        if (sceneName == "TutorialLevel" || sceneName == "Levels") {
            MusicPlayer.PlayLevelMusic();
        }
        if (sceneName == "MainMenu" || sceneName == "EndScene") {
            MusicPlayer.PlayMenuMusic();
        }
        
        SceneManager.LoadScene(sceneName);
    }
}
