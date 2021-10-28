using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that handes the player's inventory.
/// </summary>
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
    #region Variables
    /// <summary>
    /// -Holds an ID and its amount.
    /// </summary>
    [SerializeField] Dictionary<int, int> inventory;
    /// <summary>
    /// Used to configure the UI.
    /// </summary>
    [SerializeField] InventoryUI inventoryUI;
    /// <summary>
    /// A reference to the Crafting UI script.
    /// </summary>
    [SerializeField] CraftingUI craftingUI;
    /// <summary>
    /// A reference to the crafting script.
    /// </summary>
    [SerializeField] Crafting Crafting;
    //[SerializeField] int minimumToolID;
    //[SerializeField] int maximumToolID;
    /// <summary>
    /// A reference to the ToolBarUI script.
    /// </summary>
    [SerializeField] ToolBarUI ToolBarUI;
    /// <summary>
    /// 
    /// </summary>
    //[SerializeField] Item assignTools;
    /// <summary>
    /// A dictionary that contains a tool's type and ?ID
    /// </summary>
    [SerializeField] Dictionary<ToolType, int> tools;
    /// <summary>
    /// A refererence to the play interact script.
    /// </summary>
    [SerializeField] PlayerInteract playerInteract;
    #endregion

    /// <summary>
    /// Goes through the Inventory and finds itemID and count.
    /// </summary>
    /// <returns>What's inside the inventory.</returns>
    public Dictionary<int, int> getInventory()
    {
        return inventory;
    }

    /// <summary>
    /// Adds Items to the inventory.
    /// </summary>
    /// <param name="item">-References an item that wants to enter the inventory</param>
    /// <param name="count">The amount of an item</param>
    /// <param name="silent">-A bool used to fetch craftable recipes</param>
    public void AddItem(Item item, int count, bool silent = false)
    {
        //Checks if the item is null.
        if (item != null)
        {
            //-Fetches the ID
            int ID = item.GetID();
            //-asks if item is a tool.
            if (item.GetComponent<PlayerTool>())
            {
                //-Measures ID against tools[item.GetComponent<PlayerTool>().GetToolID().GetToolType()] - what I assume to be the tool's ID
                if (ID > tools[item.GetComponent<PlayerTool>().GetToolID().GetToolType()])
                {
                    //Adds the Tool to the ToolBarUI.
                    ToolBarUI.AddItem(ID, item.GetTexture());
                    //Calls SetPlayerTool in PlayerInteract
                    playerInteract.SetPlayerTool(item.gameObject);
                    return;
                }
                
            }
            //Checks if an instance of the object you wish to pick up already exists in the inventory.
            else if (!inventory.ContainsKey(ID))
            {
                //Destroys the physical copy of the object.
                Destroy(item.gameObject);
                //Establishes a specific slot in the inventory to host specific item.
                inventory.Add(ID, count);
                //Adds the item to the inventory.
                inventoryUI.AddItem(ID, count, item.GetTexture());
            }
            //Triggers if an instance of an object that you with to pickup does exsist in the inventory.
            else
            {
                //Destroys the physical copy of the object.
                Destroy(item.gameObject);
                //Adds the count of item to the existing stack.
                inventory[ID] += count;
                // -Adjust the number on the UI.
                inventoryUI.ModifyAmount(inventory[ID], ID);
            }
            if (silent == false)
            {
                //-Fetches craftable recipes. ||&& Triggers the GetCraftables function
                craftingUI.SetRecipes(Crafting.GetCraftables());
            }
        }
    }
    /// <summary>
    /// Removes an item from the inventory.
    /// </summary>
    /// <param name="ID">ID of an Item</param>
    /// <param name="count">Count of said Item</param>
    /// <param name="silent">A bool</param>
    public void RemoveItem(int ID, int count, bool silent = false)
    {
        //Asks if removing the item is possible
        if (CanRemoveItem(ID, count))
        {
            //Lowers the count of item.
            inventory[ID] -= count;
            //Triggers when there are less than zero of an item in the inventory.
            if (inventory[ID] <= 0)
            {
                //Vacates the designated space for new items.
                inventory.Remove(ID);
                //Removes the UI.
                inventoryUI.RemoveItem(ID);
            }
            //Checks if an instance of the object you wish to pick up already exists in the inventory.
            if (inventory.ContainsKey(ID))
            {
                //Modifies the UI to match the inventory.
                inventoryUI.ModifyAmount(inventory[ID], ID);
            }
            if (silent == false)
            {
                // -Fetches craftable recipes. ||&& Triggers the GetCraftables function
                craftingUI.SetRecipes(Crafting.GetCraftables());
            }
        }
    }
    /// <summary>
    /// Checks if you can remove an Item.
    /// </summary>
    /// <param name="ID">ID of the item to remove</param>
    /// <param name="count">The number of the item to remove</param>
    /// <returns>If it can successfully remove.</returns>
    public bool CanRemoveItem(int ID, int count)
    {
        //Checks If ID exists in inventory.
        if (inventory.ContainsKey(ID))
        {
            //Checks if we have enough of the item.
            if (inventory[ID] - count < 0)
            {
                return false;
            }
            return true;
        }
        return false;
    }
    /// <summary>
    /// Updates the available recipes to only items that you have portions of an item for.
    /// </summary>
    public void UpdateRecipes()
    {
        // -Fetches craftable recipes. ||&& Triggers the GetCraftables function
        craftingUI.SetRecipes(Crafting.GetCraftables());
    }
    /// <summary>
    /// A tool to check what's inside the inventory when the UI is unavailable.
    /// </summary>
    public void DebugInventory()
    {
        //Checks for when you hit the L key.
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Goes through each occupied space and remembers its name and count.
            foreach (KeyValuePair<int, int> keyValuePair in inventory)
            {
                //prints each items name and count.
                Debug.Log(keyValuePair);
            }
        }
        //A tool to discard items from the inventory without crafting
        //Checks for when you hit the F key.
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Removes an item.
            RemoveItem(1, 1);
        }
    }
    //Runs through its contents every frame.
    private void Update()
    {
        //Calls the DebugInventory script.
        DebugInventory();
    }
    //Calls when the game starts.
    private void Start()
    {
        //Initialize's inventory.
        inventory = new Dictionary<int, int>();
        //Initialize's tools.
        tools = new Dictionary<ToolType, int>();
        //Adds the tools
        #region Adding tools
        //Adds Tool of type Axe.
        tools.Add(ToolType.Axe, -1);
        //Adds Tool of type Pickaxe.
        tools.Add(ToolType.Pickaxe, -1);
        //Adds Tool of type Shovel.
        tools.Add(ToolType.Shovel, -1);
        //Adds Tool of type Flask.
        tools.Add(ToolType.Flask, -1);
        //Adds Tool of type Gloves.
        tools.Add(ToolType.Gloves, -1);
        //Adds Tool of type Shears.
        tools.Add(ToolType.Shears, -1);
        #endregion

        //AddItem(assignTools, 1, true);
    }
}
