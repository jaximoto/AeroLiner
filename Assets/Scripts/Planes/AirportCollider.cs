using System.Collections;
using UnityEngine;

public class AirportCollider : MonoBehaviour
{
    public Color color;
    public Vector3 landingScale = new Vector3(.04f, .04f, 1f);
    public float duration = 1f;
    float requiredAngleThreshold;

    /*
     * Also need to add some kind of global score later
     */

    private void Start()
    {
        requiredAngleThreshold = Component.FindFirstObjectByType<GameSettings>().AngleThreshold;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with something");
        // 6 is plane layer
        if (collision.gameObject.layer == 6)
        {
            PlaneController planeController = collision.GetComponent<PlaneController>();
            // Check if the plane is approaching from the correct angle
            if (IsValidLandingApproach(planeController))
            {
                Debug.Log("Landing Plane");
                StartCoroutine(OrchestrateLanding(collision.gameObject));
            }
            else
            {
                Debug.Log("Plane approached at the wrong angle, ignoring...");
            }
        }
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Collided with something");
        // 6 is plane layer
        if (collision.gameObject.layer == 6)
        {
            PlaneController planeController = collision.GetComponent<PlaneController>();
            // Check if the plane is approaching from the correct angle
            if (IsValidLandingApproach(planeController))
            {
                StartCoroutine(OrchestrateLanding(collision.gameObject));
            }
            else
            {
                Debug.Log("Plane approached at the wrong angle, ignoring...");
            }
        }
    }
    */

    private IEnumerator OrchestrateLanding(GameObject plane)
    {
        if (IsValidPlane(plane))
        {
            yield return StartCoroutine(plane.GetComponent<PlaneController>().StartLanding(landingScale, duration));
            Destroy(plane);
        } 
        
    }

    private bool IsValidLandingApproach(PlaneController plane)
    {
        Rigidbody2D rb = plane.GetComponent<Rigidbody2D>();
        if (rb == null) return false;
        
        Vector2 planeVelocity = rb.linearVelocity.normalized;
        Vector2 landingDirection = -transform.up.normalized; // Assumes the trigger is rotated correctly

        // Check if the approach is near straight using the dot product
        float approachDot = Vector2.Dot(planeVelocity, landingDirection);

        return approachDot >= requiredAngleThreshold;
    }

    private bool IsValidPlane(GameObject plane)
    {
        if (plane.GetComponent<SpriteRenderer>().color == color)
        {
            return true;
        }
        return false;
    }

}
