using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    public GameObject linePrefab;
    public LayerMask layerMask;

    // private vars
    Line activeLine;
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
                activeLine = newLine.GetComponent<Line>();
            }
           
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            activeLine = null;
        }

        if (activeLine != null)
        {
            
            activeLine.UpdateLine(mousePos);
        }
    }
}
