using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] Crafting crafting;
    [SerializeField] ScrollRect CraftingList;
    [SerializeField] Button CraftingRecipe;
    [SerializeField] Button CRAFT;
    private int recipeCount;
    private Recipe currentRecipe;
    List<Recipe> recipes;

    public void SetRecipes(List<Recipe> recipes)
    {
        this.recipes = recipes;
        GenerateCraftingList();
    }

    public void GenerateCraftingList()
    {
        
        Debug.Log("GeneratingList");
        for (int i = CraftingList.content.childCount - 1; i > -1; i--)
        {
            Transform itemTransform = CraftingList.content.GetChild(i);
            Destroy(itemTransform.gameObject);
        }

        for (int i = 0; i < recipes.Count; i++)
        {
            Button recipeButton = Instantiate(CraftingRecipe, CraftingList.content);
            int index = i;
            if(recipeCount != recipes.Count)
            {
                recipeCount = recipes.Count;
                SetupCraft(currentRecipe);
            }
            recipeButton.onClick.RemoveAllListeners();
            recipeButton.onClick.AddListener(() => SetupCraft(recipes[index]));
            recipeButton.GetComponentInChildren<Text>().text = recipes[i].GetName();
        }
    }

    public void SetupCraft(Recipe currentRecipe)
    {
        this.currentRecipe = currentRecipe;
        Debug.Log("Setting Up Crafting");
        CRAFT.onClick.RemoveAllListeners();
        CRAFT.onClick.AddListener(() => crafting.CraftItem(this.currentRecipe));
    }

}
