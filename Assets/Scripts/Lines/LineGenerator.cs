using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    public GameObject linePrefab;
    public LayerMask layerMask;
    public int maxLineLength = 100;
    // private vars
    public Line activeLine;
    public PlaneController activePlane;
    RaycastHit2D hit;
    GameSettings gameSettings;
    
    public int lineCount = 0;
    public bool takingInput = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameSettings = FindAnyObjectByType<GameSettings>();
        GameSettings.ZoomTriggered += Transitioning;
        CameraZoom.zoomedOut += DoneTransition;
    }

    // Update is called once per frame
    void Update()
    {
        // New Line needs to start at gameobject that is a plane:

        // get mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       
        if (takingInput)
        {
            // Check if clicking left mouse button and on a plane collider
            if (Input.GetMouseButtonDown(0))
            {
                hit = Physics2D.Raycast(mousePos, Vector2.down, Mathf.Infinity, layerMask);
                if (hit.collider != null)
                {
                    // Get collider's parent
                    GameObject plane = hit.collider.transform.parent.gameObject;
                    GameObject newLine = Instantiate(linePrefab);
                    newLine.transform.parent = plane.transform;
                    activeLine = newLine.GetComponent<Line>();



                    // assign plane to line
                    activeLine.AssignedPlane = plane;

                    // assign line to plane
                    activePlane = plane.GetComponent<PlaneController>();
                    // Get plane color and assign to line
                    Color planeColor = gameSettings.colorTable[activePlane.colorIndex];
                    activeLine.SetLineColor(planeColor);
                    StartCoroutine(activePlane.AssignPath(activeLine));


                }

            }

            if (Input.GetMouseButtonUp(0))
            {

                ResetLine();

            }

            if (activeLine != null)
            {
                lineCount++;
                activeLine.UpdateLine(mousePos);

                if (lineCount >= maxLineLength)
                {
                    ResetLine();
                }
            }
       
            /*
            if (lineCount == 10)
            {
                activePlane.AssignPath(activeLine);
            }
            */
            //activePlane.AssignPath(activeLine);
        }
    }

    void ResetLine()
    {
        activeLine = null;
        activePlane = null;
        lineCount = 0;
    }

    public void Transitioning()
    {
        takingInput = false;
    }

    public void DoneTransition()
    {
        takingInput = true;
    }
}
