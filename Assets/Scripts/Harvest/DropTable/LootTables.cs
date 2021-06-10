using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct LootTables
{
    // Start is called before the first frame update
    [SerializeField] List<Table> tables;
     List<int> entryTokens;


    public void initialize()
    {
        entryTokens = new List<int>();
        for (int i = 0; i < tables.Count; i++)
        {
            tables[i].SetID(i);
            tables[i].initialize();
            for (int j = 0; j < tables[i].GetWeight(); j++)
            {
                entryTokens.Add(tables[i].GetID());
            }
        }
    }

    public List<SpawnInstuction> spawnInstuctions()
    {
        int randomIndex = Random.Range(0, entryTokens.Count);
        List<TableEntry> tableEntries = tables[randomIndex].TableEntries();
        List<SpawnInstuction> Results = new List<SpawnInstuction>();
        for (int i = 0; i < tableEntries.Count; i++)
        {
            int Count = Random.Range(tableEntries[i].GetMin(), tableEntries[i].GetMax());
            Results.Add(new SpawnInstuction(Count, tableEntries[i].GetDroppable()));
        }
        return Results;
    }

}
[System.Serializable]
public struct SpawnInstuction
{
    int Count;
    GameObject spawnObject;
    public SpawnInstuction(int Count, GameObject spawnObject)
    {
        this.Count = Count;
        this.spawnObject = spawnObject;
    }

    public int GetCount()
    {
        return Count;
    }

    public GameObject GetSpawnObject()
    {
        return spawnObject;
    }



}



