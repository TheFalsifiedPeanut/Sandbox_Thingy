using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct TableEntry
{
    [SerializeField] int min, max, weight;
    [SerializeField] GameObject droppable;
    private int ID;

    public int GetMin()
    {
        return min;
    }
    public int GetMax()
    {
        return max;
    }
    public int GetWeight()
    {
        return weight;
    }
    public GameObject GetDroppable()
    {
        return droppable;
    }
    public int GetID()
    {
        return ID;
    }
    public void SetID(int ID)
    {
        this.ID = ID;
    }

}
