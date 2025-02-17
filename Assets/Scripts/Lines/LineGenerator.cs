using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    public GameObject linePrefab;
    public LayerMask layerMask;

    // private vars
    Line activeLine;
    PlaneController activePlane;
    RaycastHit2D hit;
    
    
    
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
                
               

            }
           
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            // take a shallow copy of line points and plane controller can go through the points as the line adds them to line points?
            activePlane.AssignPath(activeLine);
            activeLine = null;
            activePlane = null;
        }

        if (activeLine != null)
        {
            
            activeLine.UpdateLine(mousePos);

        }
    }
}
