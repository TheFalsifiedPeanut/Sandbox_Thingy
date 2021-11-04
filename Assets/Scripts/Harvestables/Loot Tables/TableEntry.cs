using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A stuct that handles the dropping of an item.
/// </summary>
[System.Serializable]
public struct TableEntry
{

    #region Variables
    /// <summary>
    /// The weight is the chance of the item being selected from the pool.
    /// </summary>
    [SerializeField] int weight;
    /// <summary>
    /// Min is the minimum number of times the item will drop.
    /// </summary>
    [SerializeField] int min;
    /// <summary>
    /// Max is the maximum number of times the item will drop.
    /// </summary>
    [SerializeField] int max;
    /// <summary>
    /// The Gameobject to drop.
    /// </summary>
    [SerializeField] GameObject droppable;
    /// <summary>
    /// ID of the TableEntry.
    /// </summary>
    int id;
    #endregion

    #region Getters
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
    #endregion

    #region Setters
    /// <summary>
    /// Set the ID to the specified value.
    /// </summary>
    /// <param name="ID"> The ID to set. </param>
    public void SetID(int id)
    {
        this.id = id;
    }
    #endregion

}