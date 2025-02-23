using UnityEngine;
using UnityEditor;
using System.Collections;
using static SpawnArea;

public class SpawnQueue : MonoBehaviour
{
    
    public ObjectPool pool;
    public SpawnArea spawnArea;
    public SpawnTable spawnTable;


    //setupSpawn variation based off levels
    float minSpawnTimer;
    float maxSpawnTimer;

    bool spawning;
    float spawnCount;
    public float spawnMax;
    void Awake()
    {
        CameraZoom.zoomedOut += StartSpawning;
        GameSettings.ZoomTriggered += StopSpawning;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            SpawnRandom();
        }
    }


    void SpawnRandom()
    {
        SpawnArea.SpawnDirs spawnDirs = spawnArea.RandomSpawn();
        string tag = spawnTable.RandomSpawnTag();
        pool.SpawnFromPool(tag, spawnDirs.spawnPos, spawnDirs.spawnRot);
    }


    private IEnumerator WaitAndSpawn()
    {
        while (spawnCount <= spawnMax)
        {
            SpawnRandom();
            spawnCount++;
            yield return new WaitForSeconds(2);
        }

        if (spawnCount > spawnMax) yield return null;
    }


    void StartSpawning()
    {
        spawning = true;
        StartCoroutine(WaitAndSpawn());
    }

    void StopSpawning()
    {
        StopCoroutine(WaitAndSpawn());
        spawning = false;
        
    }
}
    /*
     * we want this script to:
     * check for a vector2 transform on the cameras viewport. 
     * move the transform on 1 axis further from the viewport. 
     * spawn at that transform with a rotation aloing the line it moved from
     * 
     */


