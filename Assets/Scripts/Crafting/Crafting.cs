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
    public string GetName()
    {
        return RecipeName;
    }
    public List<RecipeItem> GetRecipeItems()
    {
        return RecipeItems;
    }
    
    public Recipe(string RecipeName, List<RecipeItem> RecipeItems)
    {
        this.RecipeName = RecipeName;
        this.RecipeItems = RecipeItems;
    }
}


public class Crafting : MonoBehaviour
{
    [SerializeField]List<Recipe> recipes;
    [SerializeField] PlayerInventory playerInventory;
    

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
        for (int i = 0; i < recipe.GetRecipeItems().Count; i++)
        {
            playerInventory.RemoveItem(recipe.GetRecipeItems()[i].GetID(), recipe.GetRecipeItems()[i].GetCount(), true);
        }
        playerInventory.UpdateRecipes();
    }
}


