﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public Text scoreText;

    public GameObject HPPanel;
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    bool isGameOver;

    private int activeHP = 3;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            ActivatePause();
        }
    }

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
        isGameOver = true;
    }

    public void ActivatePause()
    {
        pausePanel.SetActive(true);
        FindObjectOfType<PlayerController>().pauseGame();
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        FindObjectOfType<PlayerController>().resumeGame();
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
