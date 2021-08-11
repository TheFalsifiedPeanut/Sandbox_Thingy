using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RecipeItem
{
    [SerializeField] int ID;
    [SerializeField] int Count;

    public RecipeItem(int ID, int Count)
    {
        this.ID = ID;
        this.Count = Count;
        
    }
}


[System.Serializable]
public struct Recipe
{
    [SerializeField] List<RecipeItem> RecipeItems;
    public Recipe(List<RecipeItem> RecipeItems)
    {
        this.RecipeItems = RecipeItems;
    }
}


public class Crafting : MonoBehaviour
{
    [SerializeField]List<Recipe> recipes;
}
