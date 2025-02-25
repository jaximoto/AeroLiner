using UnityEngine;
using UnityEditor;
using System.Collections;
using static SpawnArea;

public class SpawnQueue : MonoBehaviour
{
    
    ObjectPool pool;
    SpawnArea spawnArea;
    SpawnTable spawnTable;
    public CameraZoom zoom;


    //setupSpawn variation based off levels
    float minSpawnTimer;
    float maxSpawnTimer;

    bool spawning;
    float spawnCount;
    public float spawnMax;
    void Awake()
    {
        pool = GetComponent<ObjectPool>();
        spawnArea = GetComponent<SpawnArea>();
        spawnTable = GetComponent<SpawnTable>();
        CameraZoom.zoomedOut += StartSpawning;
        GameSettings.ZoomTriggered += StopSpawning; 
    }
    void Start()
    {
        StartSpawning();
    }

    void SpawnRandom()
    {
        SpawnArea.SpawnDirs spawnDirs = spawnArea.RandomSpawn();
        string tag = spawnTable.RandomSpawnTag();
        pool.SpawnFromPool(tag, spawnDirs.spawnPos, spawnDirs.spawnRot);
    }


    private IEnumerator WaitAndSpawn()
    {
        while (spawning)
        {
            SpawnRandom();
            yield return new WaitForSecondsRealtime(Random.Range(2,5 - (zoom.zoomLevel / 2)));
        }

        if (!spawning) yield return null;
    }


    void StartSpawning()
    {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(WaitAndSpawn());
        }

    }

    void StopSpawning()
    {
        if (spawning)
        {
            StopCoroutine(WaitAndSpawn());
            spawning = false;
        }

    }
}
    /*
     * we want this script to:
     * check for a vector2 transform on the cameras viewport. 
     * move the transform on 1 axis further from the viewport. 
     * spawn at that transform with a rotation aloing the line it moved from
     * 
     */


