using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A struct describing an item in a recipe.
/// </summary>
[System.Serializable]
public struct RecipeItem
{
    #region Variables
    /// <summary>
    /// The ID of a an Item.
    /// </summary>
    [SerializeField] int ID;

    /// <summary>
    /// The ID of a a Count.
    /// </summary>
    [SerializeField] int Count;
    #endregion
    #region Getters
    public int GetID()
    {
        return ID;
    }
    
    public int GetCount()
    {
        return Count;
    }
    #endregion
    ///
    public RecipeItem(int ID, int Count)
    {
        this.ID = ID;
        this.Count = Count;
        
    }
}

/// <summary>
/// Describes the Items required to craft an Item.
/// </summary>
[System.Serializable]
public struct Recipe
{
    #region Variables
    /// <summary>
    /// The name of a recipe.
    /// </summary>
    [SerializeField] string RecipeName;
    /// <summary>
    /// Required Items to Craft.
    /// </summary>
    [SerializeField] List<RecipeItem> RecipeItems;
    /// <summary>
    /// Item resulting from a successful craft.
    /// </summary>
    [SerializeField] int outputID;
    /// <summary>
    /// The amount of an item from a successful craft.
    /// </summary>
    [SerializeField] int outputCount;
    #endregion

    #region Getters;
    public string GetName()
    {
        return RecipeName;
    }
    /// <summary>
    /// Fetches all items in a recipe.
    /// </summary>
    /// <returns>List of items.</returns>
    public List<RecipeItem> GetRecipeItems()
    {
        return RecipeItems;
    }
    public int GetOutputID()
    {
        return outputID;
    }public int GetOutputCount()
    {
        return outputCount;
    }
    #endregion
    public Recipe(string RecipeName, List<RecipeItem> RecipeItems, int outputID, int outputCount)
    {
        this.RecipeName = RecipeName;
        this.RecipeItems = RecipeItems;
        this.outputID = outputID;
        this.outputCount = outputCount;
    }
}

/// <summary>
/// Used for Crafting Items.
/// </summary>
public class Crafting : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// A list of all craftable items.
    /// </summary>
    [SerializeField] List<Recipe> recipes;
    /// <summary>
    /// A list of items to be instantiated into the inventory.
    /// </summary>
    [SerializeField] List<Item> ItemObjects;
    /// <summary>
    /// A reference to the Player's inventory.
    /// </summary>
    [SerializeField] PlayerInventory playerInventory;
    #endregion

    /// <summary>
    /// Gets current craftable recipes. 
    /// </summary>
    /// <returns>Craftable Recipes</returns>
    public List<Recipe> GetCraftables()
    {
        List<Recipe> Craftables = new List<Recipe>();
        //Cycles through all items in inventory. The key is the itemID and the Value is Item.count
        foreach (KeyValuePair<int, int> item in playerInventory.getInventory())
        {
            //looping over recipes.
            for (int i = 0; i < recipes.Count; i++)
            {
                //Checks if you have seen the recipe before.
                if (!Craftables.Contains(recipes[i]))
                {
                    //Loops through Items in the recipe.
                    for (int j = 0; j < recipes[i].GetRecipeItems().Count; j++)
                    {
                        //Checks if the current item is in the recipe, and the item.count is greater than the required count.
                        if (item.Key == recipes[i].GetRecipeItems()[j].GetID() && item.Value >= recipes[i].GetRecipeItems()[j].GetCount())
                        {
                            //Adds the recipe.
                            Craftables.Add(recipes[i]);
                        }
                    }
                }
            }
        }
        return Craftables;
    }
    /// <summary>
    /// Crafts the item and adds to the inventory.
    /// </summary>
    /// <param name="recipe">Recipe to craft</param>
    public void CraftItem(Recipe recipe)
    {
        Item craftedItem = null;
        //Loops itemObjects;
        for (int i = 0; i < ItemObjects.Count; i++)
        {
            //Checks if ItemObjectID matches the correct recipe outputID.
            if (ItemObjects[i].GetID() == recipe.GetOutputID())
            {
                //Sets Crafted Item to be ItemObjects.
                craftedItem = ItemObjects[i];
                break;
            }
        }
        if (craftedItem != null)
        {
            //Loops over Recipeitems.
            for (int i = 0; i < recipe.GetRecipeItems().Count; i++)
            {
                //Checks if a remove is possible.
                if (!playerInventory.CanRemoveItem(recipe.GetRecipeItems()[i].GetID(), recipe.GetRecipeItems()[i].GetCount()))
                {
                    return;
                }
            }
            //Loops over Recipeitems.
            for (int i = 0; i < recipe.GetRecipeItems().Count; i++)
            {
                //Removes Item from Inventory.
                playerInventory.RemoveItem(recipe.GetRecipeItems()[i].GetID(), recipe.GetRecipeItems()[i].GetCount(), true);
            }
        }
        else
        {
            Debug.LogWarning("No Item was found to be crafted");
        }
        //Adds Item to inventory.
        playerInventory.AddItem(craftedItem, recipe.GetOutputCount());
    }
    //TO DO: CLEANUP TRIANGLE.
    public void UpdateRecipes()
    {
        playerInventory.UpdateRecipes();
    }
}


