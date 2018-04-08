using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	//#Audio
	private AudioSource audioSource;
	public AudioClip point;
	public AudioClip miss;
	//---

    public Text scoreText;

    public GameObject HPPanel;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject controlPanel;

    bool isGameOver;

    private PlayerController pc;

    private int activeHP = 3;

    private void Start()
    {
		//#Audio
		audioSource = GetComponent<AudioSource>();
		//---

        pc = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            ActivatePause();
        }
    }

    public void UpdateScore(int score)
    {
		//#Audio
		audioSource.clip = point;
		audioSource.Play ();
		//---

        scoreText.text = score.ToString();
    }

    public void RemoveHP()
    {
		//#Audio
		audioSource.clip = miss;
		audioSource.Play ();
		//---

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
        pc.pauseGame();
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        pc.resumeGame();
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
