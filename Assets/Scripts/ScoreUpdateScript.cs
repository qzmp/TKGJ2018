using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdateScript : MonoBehaviour
{

    private PlayerController playerController;

	// Use this for initialization
	void Start ()
	{
	    playerController = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    SetTextPanelContent();
	}

    private void SetTextPanelContent()
    {
        GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + playerController.score;
        GameObject.Find("HpText").GetComponent<Text>().text = "Hp: " + playerController.hp;
    }
}
