using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    public GameObject background;
    public GameObject[] managedBackgroundsArray;
    private LinkedList<GameObject> managedBackgrounds;

    private float planesDifference;

	// Use this for initialization
	void Start () {
        managedBackgrounds = new LinkedList<GameObject>(managedBackgroundsArray);
        planesDifference = Mathf.Abs(managedBackgrounds.Last.Value.transform.position.x - managedBackgrounds.Last.Previous.Value.transform.position.x);
	}
	
	// Update is called once per frame
	void Update () {
		if(managedBackgrounds.Last.Value == null)
        {
            managedBackgrounds.RemoveLast();
            Vector3 spawnPosition = managedBackgrounds.First.Value.transform.position;
            spawnPosition.x += planesDifference;
            Quaternion rotation = Quaternion.Euler(new Vector3(-270, 180, 0));
            managedBackgrounds.AddFirst(Instantiate(background, spawnPosition, rotation));
        }

	}
}
