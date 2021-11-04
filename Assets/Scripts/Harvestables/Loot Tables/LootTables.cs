using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gives count and item. Then drops the loot.
/// </summary>
[System.Serializable]
public struct SpawnInstuction
{
    #region  Variables
    /// <summary>
    /// How many to spawn.
    /// </summary>
    int Count;
    /// <summary>
    /// What object to spawn.
    /// </summary>
    GameObject spawnObject;
    #endregion

    #region Getters
    /// <summary>
    /// Gets the number of Items.
    /// </summary>
    /// <returns>Count of objects to spawn</returns>
    public int GetCount()
    {
        return Count;
    }
    /// <summary>
    /// Items to spawn.
    /// </summary>
    /// <returns>Objects to spawn</returns>
    public GameObject GetSpawnObject()
    {
        return spawnObject;
    }
    #endregion


    //Constuctor.
    public SpawnInstuction(int Count, GameObject spawnObject)
    {
        this.Count = Count;
        this.spawnObject = spawnObject;
    }
}

/// <summary>
/// Struct that handles loot tables.
/// </summary>
/// [System.Serializable]
[System.Serializable]
public struct LootTables
{
    #region Variables
    /// <summary>
    /// A list of all Tables.
    /// </summary>
    [SerializeField] List<Table> tables;

    /// <summary>
    /// A list that contains each TableEntry's ID duplicated by the entries weight.
    /// </summary>
    List<int> entryTokens;
    #endregion


    /// <summary>
    /// Initialise the loot table system.
    /// </summary>
    public void Initialise()
    {
        //Initilizes entryTokens.
        entryTokens = new List<int>();
        //Loops through tables.
        for (int i = 0; i < tables.Count; i++)
        {
            // Set the ID of the table.
            tables[i].SetID(i);
            //Initialize the Table
            tables[i].Initialise();

            // Loops for the value of table weight.
            for (int j = 0; j < tables[i].GetWeight(); j++)
            {
                //Adds an entry token to the pool.
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
        //Initializes the list Results.
        List<SpawnInstuction> Results = new List<SpawnInstuction>();
        //Checks if entryTokens equals to 0.
        if (entryTokens.Count > 0)
        {
            // Get a random value for indexing the pool.
            int randomIndex = Random.Range(0, entryTokens.Count);
            //Get a list of entries that contain data for spawning loot.
            List<TableEntry> tableEntries = tables[entryTokens[randomIndex]].GetTableEntries();
            //loops through tableEntries.
            for (int i = 0; i < tableEntries.Count; i++)
            {
                //How many to spawn.
                int count = Random.Range(tableEntries[i].GetMin(), tableEntries[i].GetMax());
                // Save the results.
                Results.Add(new SpawnInstuction(count, tableEntries[i].GetDroppable()));
            }
        }
        return Results;
    }
}
