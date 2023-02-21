using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [Range(1, 10)] public float minTime = 3;
    [Range(1, 10)] public float maxTime = 5;
    [Range(1, 100)] public float radius = 2;
    public Transform spawnLocation = null;
    public GameObject[] prefabs;

    float spawnTimer = 0;

    protected void Start()
    {
        spawnTimer = Random.Range(minTime, maxTime);
    }

    public abstract void Clear();

    public abstract void Spawn();

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = Random.Range(minTime, maxTime);

            Vector3 position = spawnLocation.position + Quaternion.AngleAxis(Random.value * 360.0f, Vector3.up) * (Vector3.forward * radius);
            Instantiate(prefabs[Random.RandomRange(0,prefabs.Length)], position, Quaternion.identity);
        }
    }
}
