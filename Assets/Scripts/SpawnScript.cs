using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    public GameObject prefab;
    private GameObject player;
    public float startWait = 0;
    public float spawnWait = 3;
    public float spawnWaitRandomPercent = 0.3f;
    
    public float spawnX = 12;
    public float spawnYMin = 0;
    public float spawnYMax = 0;
    public float spawnZMin = 0;
    public float spawnZMax = 0;
    

    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            Vector3 spawnPosition = new Vector3(player.transform.position.x + spawnX, Random.Range(spawnYMin, spawnYMax), Random.Range(spawnZMin, spawnZMax));
            Quaternion rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
            Instantiate(prefab, spawnPosition, rotation);
            yield return new WaitForSeconds(spawnWait + Random.Range(-spawnWait * spawnWaitRandomPercent, spawnWait * spawnWaitRandomPercent));
        }
    }
}
