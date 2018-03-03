using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    public GameObject[] prefabs;
    private LinkedList<GameObject> managedObjects;

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
	void Update () {
        for (LinkedListNode<GameObject> i = managedObjects.Last; i != null; i = i.Previous)
        {
            if(i.Value == null)
            {
                managedObjects.Remove(i);
                Vector3 spawnPosition = managedObjects.First.Value.transform.position;
                float objectsDifference = Mathf.Abs(managedObjects.Last.Value.transform.position.x - managedObjects.Last.Previous.Value.transform.position.x);
                spawnPosition.x += objectsDifference;
                Quaternion rotation;
                if (prefabs[0].name == "background")
                {
                    rotation = Quaternion.Euler(new Vector3(-270, 180, 0));
                }
                else
                {
                    rotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));
                }
                managedObjects.AddFirst(Instantiate(prefabs[UnityEngine.Random.Range(0, prefabs.Length - 1)], spawnPosition, rotation));
                managedObjects.First.Value.transform.SetParent(transform);
            }
        }
		//if(managedObjects.Last.Value == null)
  //      {
  //          managedObjects.RemoveLast();
  //          Vector3 spawnPosition = managedObjects.First.Value.transform.position;
  //          float objectsDifference = Mathf.Abs(managedObjects.Last.Value.transform.position.x - managedObjects.Last.Previous.Value.transform.position.x);
  //          spawnPosition.x += objectsDifference;
  //          Quaternion rotation;
  //          if(prefabs[0].name == "background")
  //          {
  //              rotation = Quaternion.Euler(new Vector3(-270, 180, 0));
  //          }
  //          else
  //          {
  //              rotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));
  //          }
  //          managedObjects.AddFirst(Instantiate(prefabs[UnityEngine.Random.Range(0, prefabs.Length - 1)], spawnPosition, rotation));
  //          managedObjects.First.Value.transform.SetParent(transform);
  //      }

	}
}
