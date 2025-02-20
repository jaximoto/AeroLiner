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
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Vector2 planeVelocity = rb.linearVelocity.normalized;
            Vector2 landingDirection = -transform.up.normalized; // Assumes the trigger is rotated correctly

            // Check if the approach is near straight using the dot product
            float approachDot = Vector2.Dot(planeVelocity, landingDirection);
            if (approachDot >= requiredAngleThreshold)
            {
                // Check if the plane is approaching from the correct angle
                Debug.Log("Landing Plane");
                float angleDifference = Vector2.SignedAngle(planeVelocity, landingDirection);
                StartCoroutine(OrchestrateLanding(collision.gameObject, angleDifference));

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

    private IEnumerator OrchestrateLanding(GameObject plane, float angleDifference)
    {
        if (IsValidPlane(plane))
        {
            StartCoroutine(RotatePlane(plane, angleDifference));
            yield return StartCoroutine(plane.GetComponent<PlaneController>().StartLanding(landingScale, duration));
            Destroy(plane);
        } 
        
    }

    

    private bool IsValidPlane(GameObject plane)
    {
        if (plane.GetComponent<SpriteRenderer>().color == color)
        {
            return true;
        }
        return false;
    }

    private IEnumerator RotatePlane(GameObject plane, float angleDifference)
    {
        float rotationSpeed = 100f; // Adjust for smoothness
        Rigidbody2D rb = plane.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float targetRotation = rb.rotation - angleDifference;
            while (Mathf.Abs(Mathf.DeltaAngle(rb.rotation, targetRotation)) > 0.1f) // Keep rotating until close
            {
                rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetRotation, Time.deltaTime * rotationSpeed));
                yield return null; // Wait for the next frame
            }

            rb.MoveRotation(targetRotation); // Ensure final alignment
        }
    }

}
