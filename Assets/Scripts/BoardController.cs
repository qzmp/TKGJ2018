using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    public GameObject[] spawns;
    public GameObject player;
    public float startWait = 5;
    public float spawnWait = 5;
    public float spawnWaitRandomPercent = 0.3f;

    public Vector3 spawnLocation;

    
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            Vector3 spawnPosition = new Vector3(player.transform.position.x + spawnLocation.x, Random.Range(-spawnLocation.y, spawnLocation.y), spawnLocation.z);
            Instantiate(spawns[Random.Range(0, spawns.Length - 1)], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnWait);
        }
    }
}
