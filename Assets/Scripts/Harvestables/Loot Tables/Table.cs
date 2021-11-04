using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that handles weighted loot generation.
/// </summary>
[System.Serializable]
public class Table
{
    #region Variables
    /// <summary>
    /// Weight handles the likeliness of this table being selected.
    /// </summary>
    [SerializeField] int weight;
    /// <summary>
    /// NumberOfRolls handle the amount of times to select from the pool.
    /// </summary>
    [SerializeField] int numberOfRolls;
    /// <summary>
    /// Entries that will be rolled for, respecting weights.
    /// </summary>
    [SerializeField] List<TableEntry> randomEntries;
    /// <summary>
    /// Entries that are guaranteed to drop.
    /// </summary>
    [SerializeField] List<TableEntry> guaranteedEntries;
    /// <summary>
    /// The ID of the table.
    /// </summary>
    int tableID;
    /// <summary>
    /// A list that contains each random entry's ID duplicated by the entries weight.
    /// </summary>
    List<int> entryTokens;


    #endregion

    #region Getters
    /// <summary>
    /// Gets the weight.
    /// </summary>
    /// <returns> Returns the weight. </returns>
    public int GetWeight()
    {
        return weight;
    }

    /// <summary>
    /// Gets the ID.
    /// </summary>
    /// <returns> Returns the ID. </returns>
    public int GetID()
    {
        return tableID;
    }
    #endregion

    #region Setters
    /// <summary>
    /// Sets the ID to the specified value.
    /// </summary>
    /// <param name="id"> The value to set. </param>
    public void SetID(int id)
    {
        this.tableID = id;
    }
    #endregion



    /// <summary>
    /// Initialise the loot table.
    /// </summary>
    public void Initialise()
    {
        //Initializes entryTokens.
        entryTokens = new List<int>();
        //Loops through each entry.
        for (int i = 0; i < randomEntries.Count; i++)
        {
            // Sets the ID of the entry.
            randomEntries[i].SetID(i);

            //Loops fore the value of weight.
            for (int j = 0; j < randomEntries[i].GetWeight(); j++)
            {
                //Adds a token to the pool.
                entryTokens.Add(randomEntries[i].GetID());
            }
        }
    }
    /// <summary>
    /// A function used to calculate what an object drops.
    /// </summary>
    /// <returns>What Items will be dropped: Results.</returns>
    public List<TableEntry> GetTableEntries()
    {
        //Initializes the results, making sure to include the guaranteed entries.
        List<TableEntry> results = guaranteedEntries;
        // Ensures the number of rolls is no more than the number of entries.
        int rollCount = numberOfRolls >= randomEntries.Count ? randomEntries.Count : numberOfRolls;
        
        // Loops for the value of rollCount
        for (int i = 0; i < rollCount; i++)
        {
            // Assigns a value between 0 and entryToken's value to randomIndex.
            int randomIndex = Random.Range(0, entryTokens.Count);
            // Gets the ID of a random entry.
            int randomID = entryTokens[randomIndex];
            //Loops through randomEntries.
            for (int j = 0; j < randomEntries.Count; j++)
            {
                //Checks if randomEntries contains randomID.
                if (randomEntries[j].GetID() == randomID)
                {
                    //Adds the outcome of randomEntries to results.
                    results.Add(randomEntries[j]);
                    //Resets removeIndex.
                    int removeIndex = 0;
                    //Loops for the value of entryTokens.
                    for (int k = 0; k < entryTokens.Count; k++)
                    {
                        //Checks if randomID is equal to entryTokens[k].
                        if(randomID == entryTokens[k])
                        {
                            //Sets removeIndex to entryTokens[k].
                            removeIndex = k;

                            break;
                        }
                    }
                    //Removes already called outcomes to prevent re-rolls.
                    entryTokens.RemoveRange(removeIndex, randomEntries[j].GetWeight());
                }
            }
        }

        return results;
    }
}