using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Class for handling crafting that works with the Inventory UI.
/// </summary>
public class CraftingUI : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Reference to the crafting scripts.
    /// </summary>
    [SerializeField] Crafting crafting;
    /// <summary>
    /// Reference to the UI scroller that is used to display available recipes.
    /// </summary>
    [SerializeField] ScrollRect CraftingList;
    /// <summary>
    /// Template button for making recipes on the scroller.
    /// </summary>
    [SerializeField] Button CraftingRecipe;
    /// <summary>
    /// The button used to craft the selected recipe.
    /// </summary>
    [SerializeField] Button CRAFT;
    /// <summary>
    /// Current recipe to be crafted.
    /// </summary>
    Recipe currentRecipe;
    /// <summary>
    /// A list of all craftable recipes.
    /// </summary>
    List<Recipe> recipes;
    #endregion
    /// <summary>
    /// Sets the recipes, then generates the crafting list.
    /// </summary>
    /// <param name="recipes">Possible crafting recipes.</param>
    public void SetRecipes(List<Recipe> recipes)
    {
        this.recipes = recipes;
        GenerateCraftingList();
    }
    /// <summary>
    /// Generates recipe buttons for the scroller.
    /// </summary>
    public void GenerateCraftingList()
    {
        //Loops over all craftable recipe buttons on the scroller backwards.
        for (int i = CraftingList.content.childCount - 1; i > -1; i--)
        {
            //Gets the child for destruction.
            Transform itemTransform = CraftingList.content.GetChild(i);
            //Destroys the child.
            Destroy(itemTransform.gameObject);
        }
        //Loops over current recipes.
        for (int i = 0; i < recipes.Count; i++)
        {
            //Instantiates the recipe button.
            Button recipeButton = Instantiate(CraftingRecipe, CraftingList.content);
            //caching the index.
            int index = i;
            //Clears active button functions.
            recipeButton.onClick.RemoveAllListeners();
            //Adds the click function. The click function sets the CRAFT button to craft this recipe.
            recipeButton.onClick.AddListener(() => SetupCraft(recipes[index]));
            //Puts a name on the button.
            recipeButton.GetComponentInChildren<Text>().text = recipes[i].GetName();
        }
        //Checks if recipes contains currentRecipe.
        if (recipes.Contains(currentRecipe))
        {
            //Sets the CRAFT button to craft this recipe.
            SetupCraft(currentRecipe);
        }
        //Disables CRAFT button.
        else
        {
            CRAFT.interactable = false;
        }
    }
    /// <summary>
    /// Sets the CRAFT button to craft the provided recipe.
    /// </summary>
    /// <param name="currentRecipe">Recipe to craft</param>
    public void SetupCraft(Recipe currentRecipe)
    {
        //Enables CRAFT button.
        CRAFT.interactable = true;
        //Caches the current recipe.
        this.currentRecipe = currentRecipe;
        //Clears active button functions.
        CRAFT.onClick.RemoveAllListeners();
        //Adds the click function. The click function Crafts the current recipe.
        CRAFT.onClick.AddListener(() => crafting.CraftItem(this.currentRecipe));
    }

}
