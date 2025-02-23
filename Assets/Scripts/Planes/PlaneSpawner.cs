using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public GameObject Plane;
    public Transform spawnArea;
    public AutoDraw lineCreator;
   public void SpawnPlane()
    {
        GameObject newPlane = Instantiate(Plane, spawnArea.position, Quaternion.identity);

        // give the autodraw script a ref to the new plane
        lineCreator.plane = newPlane;
        // call create line
        lineCreator.MakeLine();
    }
}
