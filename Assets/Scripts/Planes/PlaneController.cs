using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float _speed;
    public float maxSpeed;
    public float switchDistance;
    public float turnSpeed;
    public Line activeLine;
    public List<Vector2> currentPath;
    
    Rigidbody2D rb;
    int currentTargetIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        //Debug.Log(rb.linearVelocity.magnitude);
        rb.AddForce(transform.up * _speed);
        
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        FollowPath();
    }

    public void AssignPath(Line line)
    {
        activeLine = line;
        currentPath = activeLine.ReturnPath();
    }
    public void FollowPath()
    {
        if (currentPath == null || currentPath.Count == 0)
            return;

        // Check if the plane has reached the current point
        if (Vector2.Distance(transform.position, currentPath[currentTargetIndex]) < switchDistance)
        {
            currentTargetIndex++;
            if (currentTargetIndex >= currentPath.Count)
            {
                currentTargetIndex = 0;
                currentPath = null;
                Destroy(activeLine.gameObject);
                return; // End of path
            }
        }

        // Rotate towards the next point
        Vector2 target = currentPath[currentTargetIndex];
        RotateTowards(target);
    }

    void RotateTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Smooth rotation
        float newAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, turnSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }
}
