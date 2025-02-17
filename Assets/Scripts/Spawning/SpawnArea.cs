using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public Camera cam;
    public float padd;

    Rect spawnRect;
    Rect camRect;


    void Awake()
    {
        cam = Camera.main;
    }

    void Start()
    {
        GetSpawnBounds();
        
    }

    // Update is called once per frame
    void Update()
    {
}
    }


    void GetSpawnBounds()
    {
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        camRect = new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);

        // Define a larger rectangle around the camera's view
        spawnRect = new Rect(camRect.xMin - padd, camRect.yMin - padd, camRect.width + 2 * padd, camRect.height + 2 * padd);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(camRect.center, new Vector2(camRect.width, camRect.height));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnRect.center, new Vector2(spawnRect.width, spawnRect.height));


    }

    public Vector2 RandomSpawn() 
    {
        Vector2 randPos = new Vector2(Random.Range(camRect.xMin, camRect.xMax), Random.Range(camRect.yMin, camRect.yMax));
        Debug.Log($"randompos is {randPos}");


        float xDiff;
        float yDiff;

        float spawnX;
        float spawnY; 


        // DO NOT TOUCH!!!!!!!
        //NIXON WUZ HERE
        if (randPos.x >= 0)
        {
            xDiff = camRect.xMax - randPos.x;
            if (randPos.y >= 0)
            {
                yDiff = camRect.yMax - randPos.y;
                if (xDiff <= yDiff)
                {
                    spawnX = spawnRect.xMax;
                    spawnY = randPos.y;
                }
                else
                {
                    spawnX = randPos.x;
                    spawnY = spawnRect.yMax;
                }
            }
            else
            {
                yDiff = camRect.yMin - randPos.y;
                if (xDiff <= yDiff)
                {
                    spawnX = spawnRect.xMax;
                    spawnY = randPos.y;
                }
                else
                {
                    spawnX = randPos.x;
                    spawnY = spawnRect.yMin;
                }
            }
        } 
        else
        {
            xDiff = camRect.xMin - randPos.x;
            if (randPos.y >= 0) 
            {
                yDiff = camRect.yMax - randPos.y;
                if (xDiff <= yDiff)
                {
                    spawnX = spawnRect.xMin;
                    spawnY = randPos.y;
                }
                else
                {
                    spawnX = randPos.x;
                    spawnY = spawnRect.yMax;
                }
            }
            else
            {
                yDiff = camRect.yMin - randPos.y;
                if (xDiff <= yDiff)
                {
                    spawnX = spawnRect.xMin;
                    spawnY = randPos.y;
                }
                else
                {
                    spawnX = randPos.x;
                    spawnY = spawnRect.yMin;
                }
            }
        }
        Vector2 spawnPoint = new Vector2(spawnX, spawnY);
        return spawnPoint;
    }
}
