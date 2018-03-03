using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour {
    public Color color;
    private bool damaged = false;

    private BoardController boardController;

    private void Start()
    {
        boardController = FindObjectOfType<BoardController>();
        GetComponent<Renderer>().material.SetColor("_Color", this.color);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!damaged && other.tag == "Player")
        {
            if(other.gameObject.GetComponent<PlayerController>().color == this.color)
            {
                var playerController = other.gameObject.GetComponent<PlayerController>();
                playerController.score++;
                playerController.horizontalSpeed = playerController.horizontalSpeed + playerController.score * playerController.horizontalSpeedIncrease;
                playerController.verticalSpeed = playerController.verticalSpeed + playerController.score * playerController.getVerticalSpeedIncrease();
                boardController.spawnWait -= boardController.spawnWaitDecrease;
                Destroy(gameObject);
            }
            else
            {
                other.gameObject.GetComponent<PlayerController>().hp--;
            }
            damaged = true;
        }
    }
}
