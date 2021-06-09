using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Table
{
    [SerializeField] int weight, numberOfRolls;
    [SerializeField] List<TableEntry> tableEntries;
    private List<int> EntryToken;


    public void initialize()
    {
        EntryToken = new List<int>();
        for (int i = 0; i < tableEntries.Count; i++)
        {
            for (int j = 0; j < tableEntries[i].GetWeight(); j++)
            {
                EntryToken.Add(tableEntries[i].GetID());
            }
        }
    }

    public List<TableEntry> TableEntries()
    {
        List<TableEntry> results = new List<TableEntry>();
        int RollCount = numberOfRolls >= tableEntries.Count ? tableEntries.Count : numberOfRolls;
        for (int i = 0; i < RollCount; i++)
        {
            int randomNumber = Random.Range(0, EntryToken.Count);
            int randomID = EntryToken[randomNumber];
            for (int j = 0; j < tableEntries.Count; j++)
            {
                if (tableEntries[j].GetID() == randomID)
                {
                    results.Add(tableEntries[j]);
                    int removeIndex = 0;
                    for (int k = 0; k < EntryToken.Count; k++)
                    {
                        if(randomID == EntryToken[k])
                        {
                            removeIndex = k;
                            break;
                        }
                    }
                    EntryToken.RemoveRange(removeIndex, tableEntries[j].GetWeight());
                }

            }
        }
        return results;
    }


}
