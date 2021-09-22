using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This interface is added to items to make them interactiable with the Player Inventory.
/// </summary>
public interface IInventoryItem
{
    public bool InInventory();
 
    public Texture2D GetTexture();

    /// <summary>
    /// Get the ID of the item.
    /// </summary>
    /// <returns> Returns the ID of the item. </returns>
    public int GetID();

    /// <summary>
    /// Get the width of the item in the inventory grid.
    /// </summary>
    /// <returns> Returns the width of the item. </returns>
    public int GetWidth();

    /// <summary>
    /// Get the height of the item in the inventory grid.
    /// </summary>
    /// <returns> Returns the height of the item. </returns>
    public int GetHeight();

    /// <summary>
    /// Get the item name.
    /// </summary>
    /// <returns> Returns the name of the item. </returns>
    public string GetItemName();

    /// <summary>
    /// Get the element used as UI.
    /// </summary>
    /// <returns> Returns the UI element. </returns>
    public GameObject GetUIElement();

    /// <summary>
    /// Set the element to be used as UI.
    /// </summary>
    /// <param name="gameObject"> The element to set. </param>
    public void SetUIElement(GameObject gameObject);
}