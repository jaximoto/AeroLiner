using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Line : MonoBehaviour
{
    public LineRenderer _lineRenderer;
    public GameObject AssignedPlane;

    List<Vector2> linePoints;
    
    /// <summary>
    /// Updates linePoints list with a new point, initializes list if this is first point.
    /// </summary>
    /// <param name="position">Point to add to line.</param>
    public void UpdateLine(Vector2 position)
    {
        // check if first point
        if (linePoints == null)
        {
            linePoints = new List<Vector2>();
            SetPoint(position);
            return;
        }

        // Make sure that this isn't a repeating point, needed to use System.Linq to make this not get syntax highlighted.
        if (Vector2.Distance(linePoints.Last(), position) > .1f)
        {
            SetPoint(position);
        }
    }

    /// <summary>
    /// Adds point to global list and updates the lineRenderer.
    /// </summary>
    /// <param name="point"></param>
    void SetPoint(Vector2 point)
    {
        linePoints.Add(point);

        _lineRenderer.positionCount = linePoints.Count;
        _lineRenderer.SetPosition(linePoints.Count - 1, point);
    }

    public List<Vector2> ReturnPath()
    {
        return linePoints.ToList();
    }
   
}
