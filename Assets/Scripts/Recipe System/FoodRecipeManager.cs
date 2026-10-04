using UnityEngine;

public class FoodRecipeManager : MonoBehaviour
{
    public static FoodRecipeManager Instance;
    public RecipeSO[] recipes;

    private void Awake()
    {
        Instance = this;
    }

    public RecipeSO CheckRecipe(FoodSO[] input)
    {
        foreach (var recipe in recipes)
        {
            if (recipe.IsMatchOrdered(input))
            {
                return recipe;
            }
        }
        return null;
    }

}