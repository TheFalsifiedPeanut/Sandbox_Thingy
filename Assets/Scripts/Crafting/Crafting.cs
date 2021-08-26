using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RecipeItem
{
    [SerializeField] int ID;
    [SerializeField] int Count;
    public int GetID()
    {
        return ID;
    }public int GetCount()
    {
        return Count;
    }

    public RecipeItem(int ID, int Count)
    {
        this.ID = ID;
        this.Count = Count;
        
    }
}


[System.Serializable]
public struct Recipe
{
    [SerializeField] string RecipeName;
    [SerializeField] List<RecipeItem> RecipeItems;
    [SerializeField] int outputID;
    [SerializeField] int outputCount;
    public string GetName()
    {
        return RecipeName;
    }
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
    
    public Recipe(string RecipeName, List<RecipeItem> RecipeItems, int outputID, int outputCount)
    {
        this.RecipeName = RecipeName;
        this.RecipeItems = RecipeItems;
        this.outputID = outputID;
        this.outputCount = outputCount;
    }
}


public class Crafting : MonoBehaviour
{
    [SerializeField]List<Recipe> recipes;
    [SerializeField] List<Item> ItemObjects;
    List<IInventoryItem> inventoryItems;
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField, SerializeReference] IInventoryItem test;
    

    public List<Recipe> GetCraftables()
    {
        List<Recipe> Craftables = new List<Recipe>();
        foreach (KeyValuePair<int, int> item in playerInventory.getInventory())
        {
            for (int i = 0; i < recipes.Count; i++)
            {
                if(!Craftables.Contains(recipes[i]))
                {
                    for (int j = 0; j < recipes[i].GetRecipeItems().Count; j++)
                    {
                        if (item.Key == recipes[i].GetRecipeItems()[j].GetID() && item.Value >= recipes[i].GetRecipeItems()[j].GetCount())
                        {
                            Craftables.Add(recipes[i]);
                        }
                    }
                }
            }
        }
        return Craftables;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            List<Recipe> craftRecipe = GetCraftables();
            for (int i = 0; i < craftRecipe.Count; i++)
            {
                Debug.Log(craftRecipe[i].GetName());
            }
        }
    }

    public void CraftItem(Recipe recipe)
    {
        Debug.Log("Crafting " + recipe.GetName());
        for (int i = 0; i < recipe.GetRecipeItems().Count; i++)
        {
            playerInventory.RemoveItem(recipe.GetRecipeItems()[i].GetID(), recipe.GetRecipeItems()[i].GetCount(), true);
        }
        Debug.Log(inventoryItems.Count);
        IInventoryItem craftedItem = inventoryItems[0];
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if(inventoryItems[i].GetID() == recipe.GetOutputID())
            {
                craftedItem = inventoryItems[i];
            }
        }
        playerInventory.AddItem(craftedItem, recipe.GetOutputCount(), true);
    }

    public void UpdateRecipes()
    {
        playerInventory.UpdateRecipes();
    }

    private void Start()
    {
        inventoryItems = new List<IInventoryItem>();
        for (int i = 0; i < ItemObjects.Count; i++)
        {
            if (ItemObjects[i].GetComponent<IInventoryItem>() != null)
            {
                inventoryItems.Add(ItemObjects[i].GetComponent<IInventoryItem>());
            }
        }
    }
}


