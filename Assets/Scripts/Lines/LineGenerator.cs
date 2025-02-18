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

    public int lineCount = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // New Line needs to start at gameobject that is a plane:

        // get mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       
        // Check if clicking left mouse button and on a plane
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(mousePos, Vector2.zero, layerMask);
            if (hit.collider != null)
            {
                GameObject newLine = Instantiate(linePrefab);
                newLine.transform.parent = hit.transform;
                activeLine = newLine.GetComponent<Line>();

                // assign plane to line
                activeLine.AssignedPlane = hit.collider.gameObject;
                
                // assign line to plane
                activePlane = hit.collider.GetComponent<PlaneController>();
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
}
