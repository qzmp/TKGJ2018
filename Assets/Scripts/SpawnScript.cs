using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnScript : MonoBehaviour {

    public GameObject[] prefabs;
    public GameObject[] startingObjects;
    public LinkedList<GameObject> managedObjects;


    public class Comp : IComparer<GameObject>
    {
        // Compares by Height, Length, and Width.
        public int Compare(GameObject x, GameObject y)
        {
            return x.transform.position.x.CompareTo(y.transform.position.x);
        }
    }
    void Start()
    {
        Array.Sort(startingObjects, new Comp());
        managedObjects = new LinkedList<GameObject>(startingObjects);
    }

    private void Update()
    {
        if (managedObjects.Last.Value == null)
        {
            managedObjects.RemoveLast();
            Vector3 spawnPosition = managedObjects.First.Value.transform.position;
            spawnPosition.x += managedObjects.Last.Previous.Value.transform.position.x - managedObjects.Last.Value.transform.position.x;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));
            managedObjects.AddFirst(Instantiate(prefabs[UnityEngine.Random.Range(0, prefabs.Length - 1)], spawnPosition, rotation));
        }
    }
}
