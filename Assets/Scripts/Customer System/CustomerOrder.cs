using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    public FoodSO requestedFood;
    public RecipeSO requestedRecipe;
    public GameManager gameManager;
    public GameObject customerModel;
    public bool isRecipeOrder = false;

    [SerializeField] private OrderTextBubble orderTextBubble;

    private void Awake()
    {
        if (orderTextBubble == null)
            orderTextBubble = GetComponentInChildren<OrderTextBubble>();
    }

    public void Start()
    {
        if (!gameManager.arcadeMode)
        {
            customerModel.SetActive(false);
        }
    }
    // FOOD ORDER
    public void RequestFood(FoodSO food)
    {
        requestedFood = food;
        requestedRecipe = null;
        isRecipeOrder = false;

        Debug.Log("Customer: I want " + food.name);

        orderTextBubble.ShowFoodOrder(food.name, food.foodPrefab);
    }

    // RECIPE ORDER
    public void RequestRecipe(RecipeSO recipe)
    {
        requestedRecipe = recipe;
        requestedFood = null;
        isRecipeOrder = true;

        Debug.Log("Customer: I want " + recipe.name);

        orderTextBubble.ShowRecipeOrder(recipe.name, recipe.combinationResultGameObject);
    }
}