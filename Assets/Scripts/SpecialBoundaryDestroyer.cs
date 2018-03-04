using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBoundaryDestroyer : MonoBehaviour {

    public Spawner spawner;
    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<SpecialDestroyer>() != null)
            {
                other.gameObject.GetComponent<SpecialDestroyer>().InitiateDestruction();
            }
            Destroy(other.gameObject);
            spawner.SpawnNew();
        }
    }
}
