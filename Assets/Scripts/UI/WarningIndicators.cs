using UnityEngine;
public class WarningIndicators : MonoBehaviour
{
    Rect warningRect;
    Rect camRect;
    BoxCollider2D bc;
    public SpawnArea area;

    public LayerMask mask;


    void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        warningRect = area.spawnRect;
        bc = GetComponent<BoxCollider2D>();
        ResizeColliders();
    }


    void OnCollisionStay2D(Collision2D col)
    {
        Debug.Log($"collision at {col.transform.position}");
        

        Debug.Log($"warning at {col.transform.position}");
    }



    void ResizeColliders()
    {
        
        bc.size = warningRect.size;
        bc.transform.position = warningRect.center;

    }
}
