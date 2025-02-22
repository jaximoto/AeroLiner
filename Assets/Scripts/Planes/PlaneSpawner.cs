using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    GameObject Plane;
   void SpawnPlane(Transform spot)
    {
        Instantiate(Plane, spot.position, Quaternion.identity);
    }
}
