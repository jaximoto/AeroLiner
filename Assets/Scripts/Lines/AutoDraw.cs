using System.Collections;
using UnityEngine;

public class AutoDraw : MonoBehaviour
{
    public Transform startPoint, controlPoint1, controlPoint2, endPoint;
    public GameObject linePrefab, plane;
    public Line activeLine;
    public PlaneController activePlane;
    public int segments = 50;
    public float drawDuration = 2f; // Time in seconds to fully draw

    private Vector3[] points;
    private int currentSegment = 0;

    void Start()
    {
        
        MakeLine();
    }

    public void MakeLine()
    {
        points = new Vector3[segments];

        // Precompute all Bezier points
        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);
            points[i] = GetBezierPoint(t, startPoint.position, controlPoint1.position, controlPoint2.position, endPoint.position);
        }
        GameObject newLine = Instantiate(linePrefab);
        newLine.transform.parent = plane.transform;
        activeLine = newLine.GetComponent<Line>();

        // assign plane to line
        activeLine.AssignedPlane = plane;

        // assign line to plane
        activePlane = plane.GetComponent<PlaneController>();
        StartCoroutine(activePlane.AssignPath(activeLine));
        StartCoroutine(DrawCurve());
    }
    IEnumerator DrawCurve()
    {
        float interval = drawDuration / segments;

        for (int i = 0; i < segments; i++)
        {
            activeLine.UpdateLine(points[i]);
           
            yield return new WaitForSeconds(interval);
        }
    }

    Vector3 GetBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float uu = u * u;
        float uuu = uu * u;
        float tt = t * t;
        float ttt = tt * t;

        Vector3 point = (uuu * p0) + (3 * uu * t * p1) + (3 * u * tt * p2) + (ttt * p3);
        return point;
    }
}
