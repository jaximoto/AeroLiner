using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{
    GameSettings settings;

    Camera cam;
    [SerializeField] private float size;
    public float scale;
    public float zoomAmount;
    [SerializeField] private bool zooming;

    public int zoomLevel;

    /* TODO
     * Make the zoom have a tween effect of some sort
     * add zoomLevel
     */


    void Awake()
    {
        cam = GetComponent<Camera>();
        settings = FindFirstObjectByType<GameSettings>();

    }


    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !zooming) 
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
        if (zoomLevel >= settings.zoomMax)
        {
            Debug.Log("zoom is at max");
            yield return null;
        }
        while (cam.orthographicSize < nextSize) 
        {
            cam.orthographicSize += zoomAmount;
            yield return new WaitForSeconds(Time.deltaTime);
            //Debug.Log("zoomin");
        }

        if (cam.orthographicSize >= nextSize) 
        {
            //Debug.Log("zoom ceasin");
            cam.orthographicSize = nextSize;
            zoomLevel += 1;
            Debug.Log($"zoom level = {zoomLevel}");
            zooming = false;
            yield return null;
        }    
   }

}
