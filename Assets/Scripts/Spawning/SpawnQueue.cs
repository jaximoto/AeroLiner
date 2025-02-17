using UnityEngine;
using UnityEditor;
public class SpawnQueue : MonoBehaviour
{

    ObjectPool pool;
    SpawnArea spawnArea;

    void Awake()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"));
    }


    void SpawnOffScreen()
    {

    }
}
    /*
     * we want this script to:
     * check for a vector2 transform on the cameras viewport. 
     * move the transform on 1 axis further from the viewport. 
     * spawn at that transform with a rotation aloing the line it moved from
     * 
     */


