using UnityEngine;
using System.Collections;
public class SpawnArea : MonoBehaviour
{
    


    public Camera cam;
    public float padd;

    public Rect spawnRect;
    Rect camRect;

    public Rect GetCamRect()
    {
        return camRect;
    }



    //Test Objects
    //public GameObject testPlane;
    //public GameObject lookAt;

    public float waitBetweenSpawns;
    int spawnCount;
    public int spawnMax;



    public class SpawnDirs
    {
        public Vector2 spawnPos;
        public float spawnRot;
    }




    void Awake()
    {
        cam = Camera.main;
        GetSpawnBounds();
        CameraZoom.zoomedOut += GetSpawnBounds;
        //StartCoroutine(WaitAndSpawn());
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //testPlane.transform.position = RandomSpawn();
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

    public SpawnDirs RandomSpawn() 
    {
        Vector2 randPos = new Vector2(Random.Range(camRect.xMin, camRect.xMax), Random.Range(camRect.yMin, camRect.yMax));
        //Debug.Log($"randompos is {randPos}");


        float xDiff;
        float yDiff;

        float spawnX;
        float spawnY;
        float rot;


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
                    rot = 90;
                    spawnX = spawnRect.xMax;
                    spawnY = randPos.y;
                }
                else
                {
                    rot = 180;
                    spawnX = randPos.x;
                    spawnY = spawnRect.yMax;
                }
            }
            else
            {
                yDiff = camRect.yMin - randPos.y;
                if (xDiff <= yDiff)
                {
                    rot = 90;
                    spawnX = spawnRect.xMax;
                    spawnY = randPos.y;
                }
                else
                {
                    rot = 0;
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
                    rot = 270; 
                    spawnX = spawnRect.xMin;
                    spawnY = randPos.y;
                }
                else
                {
                    rot = 180;
                    spawnX = randPos.x;
                    spawnY = spawnRect.yMax;
                }
            }
            else
            {
                yDiff = camRect.yMin - randPos.y;
                if (xDiff <= yDiff)
                {
                    rot = 270;
                    spawnX = spawnRect.xMin;
                    spawnY = randPos.y;
                }
                else
                {
                    rot = 0;
                    spawnX = randPos.x;
                    spawnY = spawnRect.yMin;
                }
            }
        }
        SpawnDirs spawnDir = new SpawnDirs();
        spawnDir.spawnPos = new Vector2(spawnX, spawnY);
        spawnDir.spawnRot = rot;
        //lookAt.transform.position = randPos;

        return spawnDir;
    }


    /*
    //tester for spawning, currently takes a test plane object and moves it, changing the rotation
    private IEnumerator WaitAndSpawn()
    {
        while (spawnCount <= spawnMax)
        {
            SpawnDirs dir = RandomSpawn();

            testPlane.transform.position = dir.spawnPos;
            testPlane.transform.rotation = Quaternion.Euler(0, 0, dir.spawnRot);
            
            spawnCount++;
            yield return new WaitForSeconds(waitBetweenSpawns);
        }

        if (spawnCount > spawnMax) yield return null;
    }
    */
}
