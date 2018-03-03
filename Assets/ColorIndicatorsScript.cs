using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorIndicatorsScript : MonoBehaviour
{
    private PlayerController playerController;
    private Image redIndicator;
    private Image greenIndicator;
    private Image blueIndicator;

    // Use this for initialization
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        SetUpIndicators();
    }

    private void SetUpIndicators()
    {
        redIndicator = GameObject.Find("RedIndicator").GetComponent<Image>();
        greenIndicator = GameObject.Find("GreenIndicator").GetComponent<Image>();
        blueIndicator = GameObject.Find("BlueIndicator").GetComponent<Image>();

        redIndicator.color = Color.grey;
        greenIndicator.color = Color.grey;
        blueIndicator.color = Color.grey;
    }

    // Update is called once per frame
    void Update()
    {
        redIndicator.color = playerController.IsRedEnabled() ? Color.red : Color.grey;
        greenIndicator.color = playerController.IsGreenEnabled() ? Color.green : Color.grey;
        blueIndicator.color = playerController.IsBlueEnabled() ? Color.blue : Color.grey;
    }
}