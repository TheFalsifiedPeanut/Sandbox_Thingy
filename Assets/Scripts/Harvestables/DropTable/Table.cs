using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Table
{
    
    [SerializeField] int weight, numberOfRolls;
    [SerializeField] List<TableEntry> randomEntries;
    [SerializeField] List<TableEntry> guaranteedEntries;
    private List<int> EntryToken;
    private int ID;
    


    public void initialize()
    {
        EntryToken = new List<int>();
        for (int i = 0; i < randomEntries.Count; i++)
        {
            randomEntries[i].SetID(i);
            for (int j = 0; j < randomEntries[i].GetWeight(); j++)
            {
                EntryToken.Add(randomEntries[i].GetID());
            }
        }
    }

    public List<TableEntry> TableEntries()
    {
        List<TableEntry> results = guaranteedEntries;
        int RollCount = numberOfRolls >= randomEntries.Count ? randomEntries.Count : numberOfRolls;
        for (int i = 0; i < RollCount; i++)
        {
            int randomNumber = Random.Range(0, EntryToken.Count);
            int randomID = EntryToken[randomNumber];
            for (int j = 0; j < randomEntries.Count; j++)
            {
                if (randomEntries[j].GetID() == randomID)
                {
                    results.Add(randomEntries[j]);
                    int removeIndex = 0;
                    for (int k = 0; k < EntryToken.Count; k++)
                    {
                        if(randomID == EntryToken[k])
                        {
                            removeIndex = k;
                            break;
                        }
                    }
                    EntryToken.RemoveRange(removeIndex, randomEntries[j].GetWeight());
                }

            }
        }
        return results;
    }

    public int GetID()
    {
        return ID;
    }
    public void SetID(int ID)
    {
        this.ID = ID;
    }
    public int GetWeight()
    {
        return weight;
    }



}
