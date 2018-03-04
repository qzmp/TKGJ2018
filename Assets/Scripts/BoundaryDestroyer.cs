using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryDestroyer : MonoBehaviour {

	// Update is called once per frame
	void OnTriggerExit (Collider other) {
        if (!other.CompareTag("Player") && !other.CompareTag("Background"))
	    {
            if (other.gameObject.GetComponent<SpecialDestroyer>() != null)
            {
                other.gameObject.GetComponent<SpecialDestroyer>().InitiateDestruction();
            }
            Destroy(other.gameObject);
	    }
	}
}
