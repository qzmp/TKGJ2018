using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialDestroyer : MonoBehaviour {

    public Spawner spawner;
    private void OnBecameInvisible()
    {
        InitiateDestruction();
    }

    public void InitiateDestruction()
    {
        spawner.SpawnNew();
        Destroy(gameObject);
    }
}
