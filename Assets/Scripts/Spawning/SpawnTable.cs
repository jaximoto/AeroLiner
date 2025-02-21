using UnityEngine;
using System.Collections.Generic;

public class SpawnTable : MonoBehaviour
{
    public CameraZoom zoom;

    [System.Serializable]
    public class Spawn
    {
        public string tag;
        public int size;
    }



    public List<Spawn> spawns_0;
    public List<Spawn> spawns_1;
    public List<Spawn> spawns_2;
    public List<Spawn> spawns_3;
    public List<Spawn> spawns_4;

    private List<string> spawnTable_0;
    private List<string> spawnTable_1;
    private List<string> spawnTable_2;    
    private List<string> spawnTable_3;
    private List<string> spawnTable_4;

    public List<List<string>> spawnTableList;

    void Awake()
    {
        FillSpawnTables();
        FillSpawnTableList();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log($"random spawn tag is {RandomSpawnTag()}");
        }
    }


    void FillSpawnTables()
    {
        //fill the spawnTable for zoomLvl 0
        for (int i = 0; i < spawns_0.Count; i++)
        {
            for (int j = 0; j < spawns_0[j].size; j++)
            {
                spawnTable_0.Add(spawns_0[j].tag);
            }
        }

        //fill the spawnTable for zoomLvl 1
        for (int i = 0; i < spawns_1.Count; i++)
        {
            for (int j = 0; j < spawns_1[j].size; j++)
            {
                spawnTable_1.Add(spawns_1[j].tag);
            }
        }

        //fill the spawnTable for zoomLvl 2
        for (int i = 0; i < spawns_2.Count; i++)
        {
            for (int j = 0; j < spawns_2[j].size; j++)
            {
                spawnTable_2.Add(spawns_2[j].tag);
            }
        }

        //fill the spawnTable for zoomLvl 3
        for (int i = 0; i < spawns_3.Count; i++)
        {
            for (int j = 0; j < spawns_3[j].size; j++)
            {
                spawnTable_3.Add(spawns_3[j].tag);
            }
        }

        //fill the spawnTable for zoomLvl 4
        for (int i = 0; i < spawns_4.Count; i++)
        {
            for (int j = 0; j < spawns_4[j].size; j++)
            {
                spawnTable_4.Add(spawns_4[j].tag);
            }
        }
    }


    void FillSpawnTableList() 
    {
        spawnTableList.Add(spawnTable_0);
        spawnTableList.Add(spawnTable_1);
        spawnTableList.Add(spawnTable_2);
        spawnTableList.Add(spawnTable_3);
        spawnTableList.Add(spawnTable_4);
    }


    private string RandomSpawnTag()
    {
        string randTag = spawnTableList[zoom.zoomLevel][Random.Range(0, spawnTableList[zoom.zoomLevel].Count)];
        return randTag;
    }
}
