using UnityEngine;
using System.Collections;
using System;

public class CameraZoom : MonoBehaviour
{
    GameSettings settings;

    Camera cam;
    [SerializeField] private float size;
    public float scale;
    public float zoomAmount;
    public bool zooming;
    public bool Tutorial;
    public int zoomLevel;

    public static event Action zoomedOut;

    /* TODO
     * Make the zoom have a tween effect of some sort
     * add zoomLevel
     */


    void Awake()
    {
        cam = GetComponent<Camera>();
        settings = FindFirstObjectByType<GameSettings>();
        if (!Tutorial)
            GameSettings.ZoomTriggered += scaleCamera;
    }

    private void OnDestroy()
    {
        zoomedOut = null;
    }
    /*
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !zooming) 
        {
            Debug.Log("KeyDown");
            scaleCamera();
        }
    }
    */


    //scale up
    public void scaleCamera()
    {
        
        size = cam.orthographicSize;
        float nextSize = size + (size * scale);
        
        Debug.Log($"scaleStarted. size is {size}, cam.orthoSize is {cam.orthographicSize}, nextSize is {nextSize}.");
        StartCoroutine(ZoomOut(nextSize));
    }

   private IEnumerator ZoomOut(float nextSize)
   {
        if (zoomLevel <= settings.zoomMax && !zooming)
        {
            zooming = true;
            while (cam.orthographicSize < nextSize)
            {
                cam.orthographicSize += zoomAmount;
                yield return new WaitForSeconds(Time.deltaTime);
                Debug.Log("zoomin");
            }
        }

        if (cam.orthographicSize >= nextSize) 
        {
            Debug.Log("zoom ceasin");
            cam.orthographicSize = nextSize;
            zoomLevel += 1;
            Debug.Log($"zoom level = {zoomLevel}");
            zooming = false;
            zoomedOut?.Invoke();
            yield return null;
        }    
   }

}
