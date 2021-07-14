using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TableEntry
{
    // Min is the minimum number of times the item will drop.
    // Max is the maximum number of times the item will drop.
    // The weight is the chance of the item being selected from the pool.
    [SerializeField] int min, max, weight;
    // The Gameobject to drop.
    [SerializeField] GameObject droppable;

    private int id;

    /// <summary>
    /// Get the minimum drop amount.
    /// </summary>
    /// <returns> Returns the minimum drop amount. </returns>
    public int GetMin()
    {
        return min;
    }

    /// <summary>
    /// Get the maximum drop amount.
    /// </summary>
    /// <returns> Returns the maximum drop amount. </returns>
    public int GetMax()
    {
        return max;
    }

    /// <summary>
    /// Get the weight.
    /// </summary>
    /// <returns> Returns the weight. </returns>
    public int GetWeight()
    {
        return weight;
    }

    /// <summary>
    /// Get the droppable object.
    /// </summary>
    /// <returns> Returns the droppable object. </returns>
    public GameObject GetDroppable()
    {
        return droppable;
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
    /// Set the ID to the specified value.
    /// </summary>
    /// <param name="ID"> The ID to set. </param>
    public void SetID(int id)
    {
        this.id = id;
    }
}