using System.Collections;
using UnityEngine;

public class AirportCollider : MonoBehaviour
{
    public Color color;
    public Vector3 landingScale = new Vector3(.04f, .04f, 1f);
    public float duration = 1f;
    float requiredAngleThreshold;
    float rotationSpeed;
    bool planeRotated = false;
    GameObject planeCountUI;
    GameSettings gameSettings;
    public PlaneSpawner planeSpawner;
    // Have this be a field in game settings:
    public bool tutorial = false;
    private void Start()
    {
        gameSettings = Component.FindFirstObjectByType<GameSettings>();
        requiredAngleThreshold = gameSettings.AngleThreshold;
        rotationSpeed = gameSettings.planeRotationSpeed;
        //planeCountUI = GameObject.Find("PlaneCount");
        
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

            Coroutine rotateCoroutine = StartCoroutine(RotatePlane(plane, angleDifference));
            yield return StartCoroutine(plane.GetComponent<PlaneController>().StartLanding(landingScale, duration));

            yield return new WaitUntil(() => planeRotated);
            //Debug.Log("PlaneRotated");
            
            plane.SetActive(false);
            if (tutorial)
                planeSpawner.SpawnPlane();
                
            else
            gameSettings.IncrementPlaneCount();

        } 
        
    }

    

    private bool IsValidPlane(GameObject plane)
    {
        if (plane.GetComponent<SpriteRenderer>().color == color)
        {
            return true;
        }
        //Debug.Log("Wrong color, plane = " + plane.GetComponent<SpriteRenderer>().color + " airport = " + color);
        return false;
    }

    private IEnumerator RotatePlane(GameObject plane, float angleDifference)
    {
        //Debug.Log("Called rotate plane coroutine");
        Rigidbody2D rb = plane.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float targetRotation = rb.rotation + angleDifference; // Adjusted to add instead of subtract

            while (Mathf.Abs(Mathf.DeltaAngle(rb.rotation, targetRotation)) > 0.3f) // Looser threshold
            {
                //Debug.Log("Still in while loop");
                rb.MoveRotation(Mathf.MoveTowardsAngle(rb.rotation, targetRotation, rotationSpeed * 20 * Time.deltaTime));
                //Debug.Log("Rotating plane");
                yield return null;
            }

            rb.MoveRotation(targetRotation); // Final correction
            planeRotated = true;
        }
    }

}
