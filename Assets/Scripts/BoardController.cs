using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    public GameObject[] spawns;
    public float startWait = 5;
    public float spawnWait = 5;
    public float spawnWaitRandomPercent = 0.3f;

    private float xBoundaryMax;

    void Start()
    {
        StartCoroutine(Spawn());
    }
        
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            xBoundaryMax = GetComponent<Collider>().bounds.max.x;
            GameObject obj = ObjectPooler.SharedInstance.GetPooledObject(spawns[Random.Range(0, spawns.Length)]);
            obj.transform.position = new Vector3(xBoundaryMax, obj.transform.position.y, obj.transform.position.z);
            obj.SetActive(true);
            foreach(Transform child in obj.transform)
            {
                child.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(spawnWait + Random.Range(-spawnWait * spawnWaitRandomPercent, spawnWait * spawnWaitRandomPercent));
        }
    }
}
