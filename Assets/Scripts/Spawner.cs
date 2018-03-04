using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] prefabs;
    public Vector3 location;

	// Update is called once per frame
	public void SpawnNew ()
	{
	    var isCloud = prefabs[0].tag == "cloud";

        var y = location.y;
	    if (isCloud)
	    {
	        y = UnityEngine.Random.Range(location.y-1, location.y+1);
	    }

        Vector3 spawnPosition = new Vector3(transform.position.x + location.x, y, location.z);
        Quaternion rotation = Quaternion.Euler(new Vector3(0, !isCloud ? UnityEngine.Random.Range(0, 360) : 0, 0));
        GameObject newObject = Instantiate(prefabs[UnityEngine.Random.Range(0, prefabs.Length - 1)], spawnPosition, rotation);
        newObject.GetComponent<SpecialDestroyer>().spawner = this;
        //newObject.transform.localScale = new Vector3(3, 3, 3);
    }
}
