using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryDestroyer : MonoBehaviour {

	// Update is called once per frame
	void OnTriggerExit (Collider other) {
        if (!other.CompareTag("Player") && other.CompareTag("Barrier"))
	    {
            other.gameObject.SetActive(false);
	    }
	}
}
