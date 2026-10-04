using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Food/Recipe")]
public class RecipeSO : ScriptableObject
{
    public FoodSO[] ingredients;     // [0] = bottom bun, [1] = top bun
    //public FoodSO combinationResult; // final FoodSO
    public GameObject combinationResultGameObject;
    public bool IsMatchOrdered(FoodSO[] input)
    {
        if (input.Length != ingredients.Length)
            return false;

        // Bun check (first & last)
        if (input[0] != ingredients[0]) return false;
        if (input[input.Length - 1] != ingredients[1]) return false;

        // Count ingredients (excluding buns)
        Dictionary<FoodSO, int> inputCounts = new Dictionary<FoodSO, int>();
        Dictionary<FoodSO, int> recipeCounts = new Dictionary<FoodSO, int>();

        // Count input (skip first & last)
        for (int i = 1; i < input.Length - 1; i++)
        {
            if (!inputCounts.ContainsKey(input[i]))
                inputCounts[input[i]] = 0;

            inputCounts[input[i]]++;
        }

        // Count recipe (skip buns: index 0 and 1)
        for (int i = 2; i < ingredients.Length; i++)
        {
            if (!recipeCounts.ContainsKey(ingredients[i]))
                recipeCounts[ingredients[i]] = 0;

            recipeCounts[ingredients[i]]++;
        }

        // Compare counts
        foreach (var kvp in recipeCounts)
        {
            if (!inputCounts.ContainsKey(kvp.Key))
                return false;

            if (inputCounts[kvp.Key] != kvp.Value)
                return false;
        }

        return true;
    }

    private bool Contains(FoodSO[] array, FoodSO item)
    {
        foreach (var a in array)
        {
            if (a == item) return true;
        }
        return false;
    }
}