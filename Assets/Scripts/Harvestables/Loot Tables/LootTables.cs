using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct LootTables
{
    [SerializeField] List<Table> tables;

    // A pool for all the loot tables.
    List<int> entryTokens;

    /// <summary>
    /// Initialise the loot table system.
    /// </summary>
    public void Initialise(GameObject callingObject)
    {
        
        entryTokens = new List<int>();

        for (int i = 0; i < tables.Count; i++)
        {
            Debug.Log("Initializing " + callingObject.name);
            // Set the ID of the table.
            tables[i].SetID(i);
            tables[i].Initialise();

            // For the weight of the table, add a token to the pool.
            for (int j = 0; j < tables[i].GetWeight(); j++)
            {
                entryTokens.Add(i);
            }
        }
    }

    /// <summary>
    /// Generates a list of spawn instructions for loot.
    /// </summary>
    /// <returns> Returns a list of instructions for spawning loot. </returns>
    public List<SpawnInstuction> GetSpawnInstuctions()
    {
        List<SpawnInstuction> Results = new List<SpawnInstuction>();

        if (entryTokens.Count > 0)
        {
            // Get a random value for indexing the pool.
            int randomIndex = Random.Range(0, entryTokens.Count);
            //Get a list of entries that contain data for spawning.
            List<TableEntry> tableEntries = tables[entryTokens[randomIndex]].GetTableEntries();
            
            for (int i = 0; i < tableEntries.Count; i++)
            {
                // Get how many to spawn.
                int count = Random.Range(tableEntries[i].GetMin(), tableEntries[i].GetMax());
                // Save the results.
                Results.Add(new SpawnInstuction(count, tableEntries[i].GetDroppable()));
            }
        }

        return Results;
    }
}

[System.Serializable]
public struct SpawnInstuction
{
    // How many to spawn.
    int Count;
    // What object to spawn.
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