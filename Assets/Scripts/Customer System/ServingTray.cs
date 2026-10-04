using UnityEngine;
using System.Collections;

public class ServingTray : MonoBehaviour
{
    public GameManager gameManager;
    public float triggerTimer = 0.2f;

    private bool canTrigger = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (!canTrigger) return;  
        GameObject obj = collision.gameObject;
        Debug.Log("Collided with: " + obj.name);
        FoodSOHolder foodHolder = obj.GetComponent<FoodSOHolder>();
        FoodRecipeHolder recipeHolder = obj.GetComponent<FoodRecipeHolder>();

        if (foodHolder == null && recipeHolder == null)
            return;

        canTrigger = false;

        // FOOD
        if (foodHolder != null)
        {
            gameManager.FoodCheck(foodHolder.foodData, null, obj.gameObject);
        }
        // RECIPE
        else if (recipeHolder != null)
        {
            gameManager.FoodCheck(null, recipeHolder.recipeFoodData, obj.gameObject);
        }

        StartCoroutine(ResetTrigger());
    }

    IEnumerator ResetTrigger()
    {
        yield return new WaitForSeconds(triggerTimer);
        canTrigger = true;
    }
}