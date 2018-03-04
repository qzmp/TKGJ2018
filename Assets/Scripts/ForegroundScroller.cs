using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ForegroundScroller : MonoBehaviour {

    public GameObject[] prefabs;
    public PlayerController PlayerController;
    public float speedFactor;

    private LinkedList<GameObject> managedObjects;

    private float xPosition;

    public class Comp : IComparer<GameObject>
    {
        // Compares by Height, Length, and Width.
        public int Compare(GameObject x, GameObject y)
        {
            return y.transform.position.x.CompareTo(x.transform.position.x);
        }
    }

    // Use this for initialization
    void Start () {
        List<GameObject> startingObjects = new List<GameObject>();
        foreach (Transform child in transform)
        {
            startingObjects.Add(child.gameObject);
        }
        startingObjects.Sort(new Comp());
        
        managedObjects = new LinkedList<GameObject>(startingObjects);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    
	    var transformPosition = Camera.main.transform.position;
	    transformPosition.z = 0;
	    xPosition += (speedFactor * PlayerController.horizontalSpeed);
	    transformPosition.x  = transformPosition.x - xPosition;
//	    transformPosition.x = xPosition;
	    transform.position = transformPosition;

        if (managedObjects.Last.Value == null)
        {
            managedObjects.RemoveLast();
            Vector3 spawnPosition = managedObjects.First.Value.transform.position;
            float objectsDifference = Mathf.Abs(managedObjects.Last.Value.transform.position.x - managedObjects.Last.Previous.Value.transform.position.x);
            spawnPosition.x += objectsDifference;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            managedObjects.AddFirst(Instantiate(prefabs[UnityEngine.Random.Range(0, prefabs.Length)], spawnPosition, rotation, transform));
        }

    }
}
