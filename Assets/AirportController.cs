using UnityEngine;
using System.Collections.Generic;
public class AirportController : MonoBehaviour
{
    public CameraZoom zoom;
    public AirportColor[] airports;


    void Start()
    {
        CameraZoom.zoomedOut += ActivateAirports;
        ActivateAirports();
    }
    void ActivateAirports()
    {
        for (int i = 0; i < airports.Length; i++)
        {
            //Debug.Log($"i is {i}, airports[i].colorIndex is {airports[i].colorIndex}, airports[i].gameObject.activeSelf is {airports[i].gameObject.activeSelf}");
            if (airports[i].colorIndex <= zoom.zoomLevel && !airports[i].gameObject.activeSelf)
            {
                airports[i].gameObject.SetActive(true);
            }
        }
    }
    

    void ListAirports()
    {
        
    }
}
