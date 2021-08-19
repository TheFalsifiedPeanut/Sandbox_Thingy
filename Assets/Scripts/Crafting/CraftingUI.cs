using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] Crafting crafting;
    [SerializeField] ScrollRect CraftingList;
    [SerializeField] Button CraftingRecipe;
    List<Recipe> recipes;
    
    public void SetRecipes(List<Recipe> recipes)
    {
        this.recipes = recipes;
        GenerateCraftingList();
    }

    public void GenerateCraftingList()
    {
        for (int i = CraftingList.content.childCount - 1; i > -1; i--)
        {
            Transform itemTransform = CraftingList.content.GetChild(i);
            Destroy(itemTransform.gameObject);
        }
         
        for (int i = 0; i < recipes.Count; i++)
        {
            Button recipeButton = Instantiate(CraftingRecipe, CraftingList.content);
            recipeButton.onClick += new Button.ButtonClickedEvent(() => crafting.CraftItem(recipes[i]));
            recipeButton.GetComponentInChildren<Text>().text = recipes[i].GetName();
        }
    }

}
