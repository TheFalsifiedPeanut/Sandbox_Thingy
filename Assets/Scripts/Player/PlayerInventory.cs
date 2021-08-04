using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    #region Old Code
    //// The inventory width used to generate the size of the inventory.
    //// The inventory height used to generate the size of the inventory.
    //[SerializeField] int inventoryWidth, inventoryHeight;
    //// A prefab that inventory items use as UI.
    //[SerializeField] GameObject inventoryItemPrefab;
    //// The inventory UI panel.
    //[SerializeField] GameObject inventoryUI;
    //// A list of the UI objects child to the UI.
    //[SerializeField] List<GameObject> gridUI;
    //
    //// The actual grid using the IDs as reference.
    //int[,] inventoryGrid;
    //// A reference to the Player Input script.
    //PlayerInput playerInput;
    //// A dictionary for referencing the actual items using their ID as a key.
    //Dictionary<int, IInventoryItem> inventoryItemReference;
    //
    //void Start()
    //{
    //    // Generate the inventory based on the specified width and height.
    //    inventoryGrid = new int[inventoryWidth, inventoryHeight];
    //    inventoryItemReference = new Dictionary<int, IInventoryItem>();
    //    playerInput = GetComponent<PlayerInput>();
    //    playerInput.SubscribeToToggleUI(ToggleUI);
    //
    //    // Loop through and initialise all IDs in the inventory as -1.
    //    for (int x = 0; x < inventoryGrid.GetLength(0); x++)
    //    {
    //        for (int y = 0; y < inventoryGrid.GetLength(1); y++)
    //        {
    //            inventoryGrid[x, y] = -1;
    //        }
    //    }
    //}
    //
    ///// <summary>
    ///// Toggle the UI visibility.
    ///// </summary>
    //void ToggleUI()
    //{
    //    // Check if it is already active.
    //    if (inventoryUI.activeSelf)
    //    {
    //        // Set it inactive.
    //        inventoryUI.SetActive(false);
    //    }
    //
    //    else
    //    {
    //        // Set it active.
    //        inventoryUI.SetActive(true);
    //    }
    //}
    //
    //// Find a start location in the grid to place the item.
    //public void FindItemStartPosition(IInventoryItem item)
    //{
    //    // Loop over the grid looking for an empty slot to place the item.
    //    for (int x = 0; x < inventoryGrid.GetLength(0); x++)
    //    {
    //        for (int y = 0; y < inventoryGrid.GetLength(1); y++)
    //        {
    //            // If the slot ID is -1 then it is empty.
    //            if (inventoryGrid[x, y] == -1)
    //            {
    //                // Attempt to add the item starting at the current position. If it returns false it will continue the search. If no place is suitable the item won't be added.
    //                if (AddItem(item, x, y))
    //                {
    //                    return;
    //                }
    //            }
    //        }
    //    }
    //}
    //
    ///// <summary>
    ///// Attempt to add the item to the inventory grid.
    ///// </summary>
    ///// <param name="item"> The item to add. </param>
    ///// <param name="xStart"> The start position on the x axis. </param>
    ///// <param name="yStart"> The start position on the y axis. </param>
    ///// <returns> Returns whether or not the item was successfully added. </returns>
    //public bool AddItem(IInventoryItem item, int xStart, int yStart)
    //{
    //    // Check the start position is in bounds.
    //    if (xStart >= 0 && yStart >= 0 && xStart < inventoryGrid.GetLength(0) && yStart < inventoryGrid.GetLength(1))
    //    {
    //        // Check the maximum size of the item doesn't exceed the limits of the inventory.
    //        if (xStart + item.GetWidth() >= inventoryGrid.GetLength(0) || yStart + item.GetHeight() >= inventoryGrid.GetLength(1))
    //        {
    //            // If it has. return false and continue the start position search.
    //            return false;
    //        }
    //
    //        // Loop the dimensions of the item.
    //        for (int x = 0; x < item.GetWidth(); x++)
    //        {
    //            for (int y = 0; y < item.GetHeight(); y++)
    //            {
    //                // Check if the current position of the item's dimensions is -1.
    //                if (inventoryGrid[xStart + x, yStart + y] != -1)
    //                {
    //                    // If it isn't, the item overlaps. Return false and continue the start position search.
    //                    return false;
    //                }
    //            }
    //        }
    //
    //        // The item can be successfully added. Find a free ID for the item.
    //        int ID = FindFreeID();
    //        // Add the item to the reference dictionary using it's new ID as it's key.
    //        inventoryItemReference.Add(ID, item);
    //
    //        // Loop the dimensions of the item.
    //        for (int x = 0; x < item.GetWidth(); x++)
    //        {
    //            for (int y = 0; y < item.GetHeight(); y++)
    //            {
    //                // Set the positions on the grid the item covers to have the item's new ID.
    //                inventoryGrid[xStart + x, yStart + y] = ID;
    //            }
    //        }
    //
    //        // Generate a new UI object to represent the item. It's position is set by using the Grid UI position and offsetting the items UI Element.
    //        GameObject itemUI = Instantiate(inventoryItemPrefab, gridUI[Index2DTo1D(xStart, yStart)].transform.position + new Vector3(-50, 50, 0), Quaternion.identity, inventoryUI.transform);
    //        // Set the size of the UI Element.
    //        itemUI.GetComponent<RectTransform>().sizeDelta = (new Vector3(item.GetWidth(), item.GetHeight(), 1) * 100);
    //        // Set the UI Element as a reference for the item.
    //        item.SetUIElement(itemUI);
    //
    //        // The item has been successfully added.
    //        return true;
    //    }
    //
    //    // The position is out of bounds. This could mean something is quite wrong.
    //    return false;
    //}
    //
    ///// <summary>
    ///// Remove and item from the inventory grid.
    ///// </summary>
    ///// <param name="xStart"> The x axis postion to start the removal at. </param>
    ///// <param name="yStart"> The y axis postion to start the removal at. </param>
    //public void RemoveItem(int xStart, int yStart)
    //{
    //    // Check if the start position is in bounds.
    //    if (xStart >= 0 && xStart < inventoryGrid.GetLength(0) && yStart >= 0 && yStart < inventoryGrid.GetLength(1))
    //    {
    //        // Check if the start position's item ID is not -1.
    //        if (inventoryGrid[xStart, yStart] != -1)
    //        {
    //            // Cache the ID of the item to remove.
    //            int removeID = inventoryGrid[xStart, yStart];
    //            // Destroy the UI element for the item.
    //            Destroy(inventoryItemReference[removeID].GetUIElement());
    //            // Remove the reference to the item from the dictionary.
    //            inventoryItemReference.Remove(removeID);
    //
    //            // Loop over the entire inventory.
    //            for (int x = 0; x < inventoryGrid.GetLength(0); x++)
    //            {
    //                for (int y = 0; y < inventoryGrid.GetLength(1); y++)
    //                {
    //                    // Find instances of the ID of the removed item.
    //                    if (inventoryGrid[x, y] == removeID)
    //                    {
    //                        // Set the instances of the removed item's ID to -1 so they are empty cells.
    //                        inventoryGrid[x, y] = -1;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
    //
    ///// <summary>
    ///// A helper function to convert 2D grid coordinates to a 1D coordinate.
    ///// </summary>
    ///// <param name="x"> The x position to convert. </param>
    ///// <param name="y"> The y position to convert. </param>
    ///// <returns> Returns the 1D coordinate of the converted 2D coordinates. </returns>
    //private int Index2DTo1D(int x, int y)
    //{
    //    // The y position is multiplied by the width of the inventory. This represents whole rows of positions. The x is then added as the final partial row.
    //    return y * inventoryWidth + x;
    //}
    //
    ///// <summary>
    ///// Finds a free ID for the new item.
    ///// </summary>
    ///// <returns></returns>
    //private int FindFreeID()
    //{
    //    // Loop to infinity.
    //    for (int i = 0; i < Mathf.Infinity; i++)
    //    {
    //        // Check if the item reference contains the key.
    //        if (!inventoryItemReference.ContainsKey(i))
    //        {
    //            // Return the unused integer as the new ID.
    //            return i;
    //        }
    //    }
    //
    //    // Return -1 if there are no free integers. This is theortically impossible but it is required for the function to have a return for all outcomes.
    //    return -1;
    //}
    #endregion
    [SerializeField]Dictionary<int, int> inventory;
    [SerializeField]InventoryUI inventoryUI;
    public void AddItem(IInventoryItem item, int count)
    {
        int ID = item.GetID();
        if(!inventory.ContainsKey(ID))
        {
            inventory.Add(ID, count);
            inventoryUI.AddItem(item.GetTexture());
        }
        else
        {
            inventory[ID] += count;
        }
    }
    public void RemoveItem(int ID, int count)
    {
        if(inventory.ContainsKey(ID))
        {
            inventory[ID] -= count;
            if(inventory[ID] <= 0 )
            {
                inventory.Remove(ID);
            }
        }
    }
    public void DebugInventory()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            foreach (KeyValuePair<int, int> keyValuePair in inventory)
            {
                Debug.Log(keyValuePair);
            }
        }
    }
    private void Update()
    {
        DebugInventory();
    }
    private void Start()
    {
        inventory = new Dictionary<int, int>();
    }

}