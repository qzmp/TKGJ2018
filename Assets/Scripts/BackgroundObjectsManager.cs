using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjectsManager : MonoBehaviour {
     
    public GameObject[] backgroundTilePrefabs;
    public float commonPartX;

    private float xBoundaryExtents;
    private GameObject lastSpawn;
    
	void Start () {
        xBoundaryExtents = GetComponent<Collider>().bounds.extents.x;
        SpawnRandomTile(GetComponent<Collider>().bounds.center.x - xBoundaryExtents);
        fillStartingBoundary();
	}
	
	void Update () {
        Debug.Log("extents: " + GetComponent<Collider>().bounds.extents.x);

        if (GetComponent<Collider>().bounds.center.x + xBoundaryExtents > lastSpawn.transform.position.x)
        {
            SpawnRandomTile();
        }
    }

    void fillStartingBoundary()
    {
        
        while (GetComponent<Collider>().bounds.center.x + xBoundaryExtents > lastSpawn.GetComponent<TerrainCollider>().bounds.center.x)
        {
            SpawnRandomTile();
        }
    }

    void SpawnRandomTile()
    {
        GameObject obj = ObjectPooler.SharedInstance.GetPooledObject(backgroundTilePrefabs[Random.Range(0, backgroundTilePrefabs.Length)]);
        float x = CalculateXPosition(lastSpawn, obj);
        obj.transform.position = new Vector3(x, obj.transform.position.y, obj.transform.position.z);
        obj.SetActive(true);
        lastSpawn = obj;
    }

    void SpawnRandomTile(float x)
    {
        GameObject obj = ObjectPooler.SharedInstance.GetPooledObject(backgroundTilePrefabs[Random.Range(0, backgroundTilePrefabs.Length)]);
        obj.transform.position = new Vector3(x, obj.transform.position.y, obj.transform.position.z);
        obj.SetActive(true);
        lastSpawn = obj;
    }

    float CalculateXPosition(GameObject existingObject, GameObject newObject)
    {
        //TODO remake to work with meshes
        return existingObject.GetComponent<TerrainCollider>().bounds.max.x + newObject.GetComponent<TerrainCollider>().bounds.extents.x - commonPartX;
    }
}
