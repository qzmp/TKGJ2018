using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    public GameObject[] spawns;
    public float startWait = 5;
    public float spawnWait = 5;
    public float spawnWaitRandomPercent = 0.3f;

    private float xExtent;

    void Start()
    {
        xExtent = GetComponent<Collider>().bounds.extents.x;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x + xExtent, 0, 0);
            Instantiate(spawns[Random.Range(0, spawns.Length - 1)], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnWait + Random.Range(-spawnWait * spawnWaitRandomPercent, spawnWait * spawnWaitRandomPercent));
        }
    }
}
