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

    List<string> spawnTable_0;
    List<string> spawnTable_1;
    List<string> spawnTable_2;    
    List<string> spawnTable_3;
    List<string> spawnTable_4;
   
    List<List<string>> spawnTableList;

    void Awake()
    {
    spawnTable_0 = new List<string>();
    spawnTable_1 = new List<string>();
    spawnTable_2 = new List<string>();
    spawnTable_3 = new List<string>();
    spawnTable_4 = new List<string>();
    spawnTableList= new List<List<string>>();
    FillSpawnTables();
    FillSpawnTableList();
    }

    void FillSpawnTables()
    {
        //fill the spawnTable for zoomLvl 0
        for (int i = 0; i < spawns_0.Count; i++)
        {
         
            for (int j = 0; j < spawns_0[i].size; j++)
            {
                Debug.Log($"spawns_0[j].size {spawns_0[i].size}, spawnTag is {spawns_0[i].tag} ");
                spawnTable_0.Add(spawns_0[i].tag);
            }
            Debug.Log($"count is {spawnTable_0.Count}");

        }

        //fill the spawnTable for zoomLvl 1
        for (int i = 0; i < spawns_1.Count; i++)
        {
            for (int j = 0; j < spawns_1[i].size; j++)
            {
                spawnTable_1.Add(spawns_1[i].tag);
            }
            
        }

        //fill the spawnTable for zoomLvl 2
        for (int i = 0; i < spawns_2.Count; i++)
        {
            for (int j = 0; j < spawns_2[i].size; j++)
            {
                spawnTable_2.Add(spawns_2[i].tag);
            }
        }

        //fill the spawnTable for zoomLvl 3
        for (int i = 0; i < spawns_3.Count; i++)
        {
            for (int j = 0; j < spawns_3[i].size; j++)
            {
                spawnTable_3.Add(spawns_3[i].tag);
            }
        }

        //fill the spawnTable for zoomLvl 4
        for (int i = 0; i < spawns_4.Count; i++)
        {
            for (int j = 0; j < spawns_4[i].size; j++)
            {
                spawnTable_4.Add(spawns_4[i].tag);
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


    public string RandomSpawnTag()
    {
        //spawnTag is {spawnTableList[zoom.zoomLevel][Random.Range(0, spawnTableList[zoom.zoomLevel].Count)]}
        //Debug.Log($"zoom level is{zoom.zoomLevel}, spawnTable is {spawnTableList[zoom.zoomLevel] } ");
        string randTag = spawnTableList[zoom.zoomLevel][Random.Range(0, spawnTableList[zoom.zoomLevel].Count)];
        return randTag;
    }
}
