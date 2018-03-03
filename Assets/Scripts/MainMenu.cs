using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private const string ScoreSceneName = "ScoreScene";
    private const string CreditsSceneName = "CreditsScene";


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartCredits()
    {
        SceneManager.LoadScene(CreditsSceneName);
    }

    public void ShowScoreBoard()
    {
        SceneManager.LoadScene(ScoreSceneName);
    }
}
