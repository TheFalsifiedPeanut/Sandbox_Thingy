using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Table
{
    // Weight handles the likeliness of a entry being selected. NumberOfRolls handle the amount of times to select from the pool.
    [SerializeField] int weight, numberOfRolls;
    // Entries that will be rolled for, respecting weights.
    [SerializeField] List<TableEntry> randomEntries;
    // Entries that will happen regardless.
    [SerializeField] List<TableEntry> guaranteedEntries;

    int id;
    // A pool for all the entries.
    List<int> entryTokens;

    /// <summary>
    /// Get the weight.
    /// </summary>
    /// <returns> Returns the weight. </returns>
    public int GetWeight()
    {
        return weight;
    }

    /// <summary>
    /// Get the ID.
    /// </summary>
    /// <returns> Returns the ID. </returns>
    public int GetID()
    {
        return id;
    }

    /// <summary>
    /// Sets the ID to the specified value.
    /// </summary>
    /// <param name="id"> The value to set. </param>
    public void SetID(int id)
    {
        this.id = id;
    }

    /// <summary>
    /// Initialise the loot table.
    /// </summary>
    public void Initialise()
    {
        entryTokens = new List<int>();

        for (int i = 0; i < randomEntries.Count; i++)
        {
            // Set the ID of the entry.
            randomEntries[i].SetID(i);

            // For the weight of the entry, add a token to the pool.
            for (int j = 0; j < randomEntries[i].GetWeight(); j++)
            {
                entryTokens.Add(randomEntries[i].GetID());
            }
        }
    }

    public List<TableEntry> GetTableEntries()
    {
        List<TableEntry> results = guaranteedEntries;
        // Get the number of times to roll for entries.
        int rollCount = numberOfRolls >= randomEntries.Count ? randomEntries.Count : numberOfRolls;

        // For the number of rolls, select token from pool.
        for (int i = 0; i < rollCount; i++)
        {
            // Get a random value for indexing the pool.
            int randomIndex = Random.Range(0, entryTokens.Count);
            // Get the ID of a random entry.
            int randomID = entryTokens[randomIndex];

            for (int j = 0; j < randomEntries.Count; j++)
            {
                if (randomEntries[j].GetID() == randomID)
                {
                    results.Add(randomEntries[j]);
                    int removeIndex = 0;

                    for (int k = 0; k < entryTokens.Count; k++)
                    {
                        if(randomID == entryTokens[k])
                        {
                            removeIndex = k;

                            break;
                        }
                    }

                    entryTokens.RemoveRange(removeIndex, randomEntries[j].GetWeight());
                }
            }
        }

        return results;
    }
}