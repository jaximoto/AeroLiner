using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Line : MonoBehaviour
{
    public LineRenderer _lineRenderer;
    public GameObject AssignedPlane;
    
    [SerializeField] public List<Vector2> linePoints { get; private set; }

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

    /*public List<Vector2> ReturnPath()
    {
        return linePoints.ToList();
    }
    */
    public void RemovePoint(int index)
    {
        linePoints.RemoveAt(index);
        int newPositionCount = _lineRenderer.positionCount - 1;
        RemovePointLine(index);
        //_lineRenderer.positionCount = linePoints.Count;


    }

    // Removes the first point and shifts all the others
    // Removes the first point and shifts all the others
    int firstPointIndex = 0;
    void RemovePointLine(int index)
    {
        if (_lineRenderer.positionCount <= 1)
            return;

        // Loop through the points to find the first point the plane has reached or passed
        int indexToDeleteBefore = index;
       

        // If no point is found (the plane hasn't reached any point yet), exit
        if (indexToDeleteBefore == -1)
            return;

        // Create a new list of positions, excluding the points before indexToDeleteBefore
        var positions = new Vector3[_lineRenderer.positionCount - indexToDeleteBefore];

        // Copy the positions from indexToDeleteBefore onward into the new array
        for (int i = indexToDeleteBefore; i < _lineRenderer.positionCount; i++)
        {
            positions[i - indexToDeleteBefore] = _lineRenderer.GetPosition(i);
        }

        // Set the new position array back to the LineRenderer
        _lineRenderer.positionCount = positions.Length;
        _lineRenderer.SetPositions(positions);
    }

    public void SetLineColor(Color color)
    {
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;
    }
}
