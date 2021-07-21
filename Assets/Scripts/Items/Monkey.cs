using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Harvestable, IInventoryItem
{
    // The width is the size of the space taken by the item on the x axis.
    // The height is the size of the space taken by the item on the y axis.
    [SerializeField] int width, height;

    // The ID is set by the inventory system.
    int id;
    // The Ite Name is the name of the item. Simple.
    string itemName;
    // The UI element is the UI object representing this item.
    GameObject uiElement;

    /// <summary>
    /// Get the ID of the item.
    /// </summary>
    /// <returns> returns the ID of the item. </returns>
    public int GetID( ) { return id; }

    /// <summary>
    /// Get the width of the item.
    /// </summary>
    /// <returns> Returns the width of the item. </returns>
    public int GetWidth( ) { return width; }

    /// <summary>
    /// Get the height of the item.
    /// </summary>
    /// <returns> Returns the height of the item. </returns>
    public int GetHeight() { return height; }

    /// <summary>
    ///  Get the name of the item.
    /// </summary>
    /// <returns> Returns the name of the item. </returns>
    public string GetItemName() { return itemName; }

    /// <summary>
    /// Get the UI Element that represents the item.
    /// </summary>
    /// <returns> Returns the UI Element of the item. </returns>
    public GameObject GetUIElement() { return uiElement; }

    /// <summary>
    /// Set the UI Element that represents the item.
    /// </summary>
    /// <param name="uiElement"> The UI Element to set. </param>
    public void SetUIElement(GameObject uiElement) { this.uiElement = uiElement; }
}
