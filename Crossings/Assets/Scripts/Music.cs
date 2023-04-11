using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public static Music instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Tilemap")
            Music.instance.GetComponent<AudioSource>().Pause();
            //BGmusic.instance.GetComponent<AudioSource>().Play();
    }
}
