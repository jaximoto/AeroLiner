using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using static ObjectPool;

public class WarningIndicators : MonoBehaviour
{
    Rect warningRect, camRect, indRect;
    
    BoxCollider2D bc;
    public SpawnArea area;
    public float padd;
    
    public GameObject warningSign;
    public int signCount;
    Queue<GameObject> signs;
    

    void Awake()
    {

        signs = new Queue<GameObject>();
        for (int i = 0; i < signCount; i++)
        {
            GameObject obj = Instantiate(warningSign);
            obj.SetActive(false);
            signs.Enqueue(obj);
        }
    }

    void Start()
    {
        warningRect = area.spawnRect;

        bc = GetComponent<BoxCollider2D>();
        ResizeColliders();

        Debug.Log($"signs count = {signs.Count}");
    }

    public GameObject SpawnSign(Rigidbody2D rb)
    {

        GameObject objectToSpawn = signs.Dequeue();
        objectToSpawn.transform.position = AssignSignPos(rb);
        objectToSpawn.SetActive(true);
        signs.Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    Vector2 AssignSignPos(Rigidbody2D rb)
    {
        float x, y;
        Vector2 indPos;
        //Vector2 objWorldPos;

        if (Mathf.Abs(rb.linearVelocityX) >= Mathf.Abs(rb.linearVelocityY)) 
        {
            x = Mathf.Clamp(rb.position.x, indRect.xMin, indRect.xMax);
            y = rb.position.y;

            indPos = new Vector2(x, y);
            Debug.Log($"indPos = {indPos} and vX is non0");
            Debug.Log($"rb.pos.y is {rb.position.y}, indRect.yMin is {indRect.yMin}, indRect.yMax is {indRect.yMax}, returned y is {y}");
            return indPos;
        }
        else if (Mathf.Abs(rb.linearVelocityX) < Mathf.Abs(rb.linearVelocityY))
        {
            x = rb.position.x;
            y = Mathf.Clamp(rb.position.y, indRect.yMin, indRect.yMax);

            indPos = new Vector2(x, y);
            Debug.Log($"indPos = {indPos} and vY is non0");
            Debug.Log($"rb.pos.x is {rb.position.x}, indRect.xMin is {indRect.xMin}, indRect.xMax is {indRect.xMax}, returned x is {x}");
            return indPos;
        }

        

        else return new Vector2(20,20);
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log($"collision at {col.transform.position}");
        SpawnSign(col.attachedRigidbody);
    }



    void ResizeColliders()
    {
        
        bc.size = warningRect.size;
        bc.transform.position = warningRect.center;

        camRect = area.GetCamRect();
        indRect = new Rect(camRect.xMin + padd, camRect.yMin + padd, camRect.width - 2 * padd, camRect.height - 2 * padd);

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(indRect.center, new Vector2(indRect.width, indRect.height));
    }

}
