using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {
    public GameObject[] spawnees;
    public Vector3 spawnValues;
    public float spawnWait;
    public float spawnLeastWait;
    public float spawnMostWait;
    public int startWait;
    public bool stop;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update()
    {
       
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        if (Timer.timeLeft <= 0)
        {
            stop = true;
        }
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);
        
        while (!stop)
        {
            Vector3 spawnPosition;
            for (int i=0; i<3;i++)
            {
                spawnPosition = new Vector3(Random.Range(-14, 14), 10, 0);
                Instantiate(spawnees[0], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
            }
            spawnPosition = new Vector3(Random.Range(-14, 14), 10, 0);
            Instantiate(spawnees[1], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }
}
