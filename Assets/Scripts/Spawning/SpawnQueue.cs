using UnityEngine;
using UnityEditor;
public class SpawnQueue : MonoBehaviour
{

    public ObjectPool pool;
    public SpawnArea spawnArea;
    public SpawnTable spawnTable;
    void Awake()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

        }
    }


    void SpawnRandom()
    {
        SpawnArea.SpawnDirs spawnDirs = spawnArea.RandomSpawn();
        string tag = spawnTable.RandomSpawnTag();
        pool.SpawnFromPool(tag, spawnDirs.spawnPos, new Quaternion(0, 0, spawnDirs.spawnRot, 0));
    }


}
    /*
     * we want this script to:
     * check for a vector2 transform on the cameras viewport. 
     * move the transform on 1 axis further from the viewport. 
     * spawn at that transform with a rotation aloing the line it moved from
     * 
     */


