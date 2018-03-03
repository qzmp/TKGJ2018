﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public Text scoreText;

    public GameObject HPPanel;
    public GameObject gameOverPanel;

    private int activeHP = 3;

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void RemoveHP()
    {
        if(activeHP != 0)
        {
            HPPanel.transform.GetChild(--activeHP).gameObject.SetActive(false);
        }
    }
    public void ActivateGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
