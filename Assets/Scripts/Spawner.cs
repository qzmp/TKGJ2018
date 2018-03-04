using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] prefabs;
    public Vector3 location;

	// Update is called once per frame
	public void SpawnNew () {
        Vector3 spawnPosition = new Vector3(transform.position.x + location.x, location.y, location.z);
        Quaternion rotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));
        Instantiate(prefabs[UnityEngine.Random.Range(0, prefabs.Length - 1)], spawnPosition, rotation).GetComponent<SpecialDestroyer>().spawner = this;
    }
}
