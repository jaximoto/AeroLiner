using UnityEngine;
using UnityEditor;
public class SpawnQueue : MonoBehaviour
{
 
    ObjectPool pool;
    Camera cam;
    Rect camRect;
    Rect spawnRect;

    public GameObject square;
    Rect squareRect;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }
    void Start()
    {
        SpawnOffScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SpawnOffScreen()
    {
        camRect = cam.rect;
        spawnRect = new Rect(camRect.x - 0.1f, camRect.y - 0.1f, camRect.width + 0.1f, camRect.height + 0.1f);

        Debug.Log($"spawn rect is {spawnRect} and camRect is {camRect}");
        //squareRect = square.TryGetComponent<Rect rect>();
        squareRect = spawnRect;
    }

    /*
     * we want this script to:
     * check for a vector2 transform on the cameras viewport. 
     * move the transform on 1 axis further from the viewport. 
     * spawn at that transform with a rotation aloing the line it moved from
     * 
     */
}
