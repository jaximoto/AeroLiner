using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public Color planeColor;
    public float _speed;
    public float maxSpeed;
    public float switchDistance;
    public float turnSpeed;
    public Line activeLine;
    public List<Vector2> currentPath;
    public bool lineWasAssigned = false;
    public float pathDelay = .75f;
    
    Rigidbody2D rb;
    SpriteRenderer sprite;
    int currentTargetIndex = 0;
    Transform _transform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (lineWasAssigned)
        {
            FollowPath();
        }
        
    }

    private void FixedUpdate()
    {
        //Debug.Log(rb.linearVelocity.magnitude);
        rb.AddForce(transform.up * _speed);
        
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        
    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with " + collision.ToString());
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
        {
            // Hit another plane
            // Destroy yourself and end game
            Destroy(gameObject);
        }

        
    }
    // Okay so plane needs to know when line isn't done so it can wait to destroy a line, maybe like .5f secs
    // Maybe set a boolean that a couroutine launched by assign path sets to true
    public IEnumerator AssignPath(Line line)
    {
        Debug.Log("Called Assign Path");
        
        if (activeLine != null)
        {
            clearPath();
            
        }

        yield return StartCoroutine(WaitForSeconds(pathDelay));
        activeLine = line;
        
        currentPath = activeLine.linePoints;
        foreach (var point in currentPath)
        {
            //Debug.Log(point.ToString());
        }

        lineWasAssigned = true;
        
    }
    
    public void FollowPath()
    {
        if (currentPath == null || currentPath.Count == 0)
        {
            //Debug.Log("Current path is null or count is 0");
            return;
        }

        //Debug.Log("Made it past null check in followPath");
        // Check if the plane has reached the current point
        if (Vector2.Distance(transform.position, currentPath[currentTargetIndex]) < switchDistance)
        {
            //activeLine.RemovePoint(currentTargetIndex);
            currentTargetIndex++;
            if (currentTargetIndex >= currentPath.Count)
            {
                clearPath();
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

    void clearPath()
    {
        currentTargetIndex = 0;
        currentPath = null;
        Destroy(activeLine.gameObject);
        activeLine = null;
        lineWasAssigned = false;
    }
    IEnumerator WaitForSeconds(float seconds)
    {
        float timePassed = 0f;
        while (timePassed < seconds)
        {
            timePassed += Time.deltaTime;
            yield return null;
        }
    }

    public void ApplyColor(Color color)
    {
        planeColor = color;
        sprite.color = planeColor;
    }


    
    public IEnumerator StartLanding(Vector3 targetScale, float duration)
    {
        
        LeanTween.scale(gameObject, targetScale, duration).setEase(LeanTweenType.easeInQuad);
        // Wait until the tween is finished
        yield return new WaitUntil(() => !LeanTween.isTweening(gameObject));

        yield return WaitForSeconds(duration / 2);
    }
}
