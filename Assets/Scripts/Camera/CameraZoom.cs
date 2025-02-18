using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{
    Camera cam;
    [SerializeField] private float size;
    public float scale;
    public float zoom;
    [SerializeField] private bool zooming;

    void Awake()
    {
        cam = GetComponent<Camera>();
        

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !zooming) 
        {
            Debug.Log("KeyDown");
            scaleCamera();
        }
    }



    //scale up
    void scaleCamera()
    {
        
        size = cam.orthographicSize;
        float nextSize = size + (size * scale);
        zooming = true;
        Debug.Log($"scaleStarted. size is {size}, cam.orthoSize is {cam.orthographicSize}, nextSize is {nextSize}.");
        StartCoroutine(ZoomOut(nextSize));
    }

   private IEnumerator ZoomOut(float nextSize)
   {
        while (cam.orthographicSize < nextSize) 
        {
            cam.orthographicSize += zoom;
            yield return new WaitForSeconds(Time.deltaTime);
            Debug.Log("zoomin");
        }

        if (cam.orthographicSize >= nextSize) 
        {
            Debug.Log("zoom ceasin");
            cam.orthographicSize = nextSize;
            zooming = false;
            yield return null;
        }    
   }

}
