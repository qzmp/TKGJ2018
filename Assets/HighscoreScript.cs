using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreScript : MonoBehaviour
{
    public Text HighscoreValue;

    // Use this for initialization
    void Start()
    {
        var highscore = PlayerPrefs.GetInt("highscore");
        HighscoreValue.text = highscore.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}