using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour {

    public Color color;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(other.gameObject.GetComponent<PlayerController>().color != this.color)
            {
                other.gameObject.GetComponent<PlayerController>().hp--;
            }
        }
    }
}
