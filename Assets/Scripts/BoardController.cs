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
            GameObject obj = ObjectPooler.SharedInstance.GetPooledObject(spawns[Random.Range(0, spawns.Length)].name);
            obj.transform.position = new Vector3(transform.position.x + xExtent, obj.transform.position.y, obj.transform.position.z);
            obj.SetActive(true);
            foreach(Transform child in obj.transform)
            {
                child.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(spawnWait + Random.Range(-spawnWait * spawnWaitRandomPercent, spawnWait * spawnWaitRandomPercent));
        }
    }
}
