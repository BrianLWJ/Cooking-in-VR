using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public FoodSO[] foodSODatabase;
    public RecipeSO[] recipeSODatabase;

    public CustomerOrder currentCustomer;

    public int maxCustomers = 5;
    private int customerServedCount = 0;

    public int customerLeaveTime = 5;
    private Coroutine leaveCoroutine;

    private bool canServe = true;
    public bool arcadeMode = true;

    public OrderTextBubble orderTextBubble;
    public float correctOrderTimerWait = 1f;
    public float wrongOrderTimerWait = 1f;
    public float orderTimerWait = 1.5f;
    public GameObject[] characterModel;

    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //if (!arcadeMode)
        //{
        //    gameObject.SetActive(false);
        //}
        //else
        //{
        gameObject.SetActive(true);
        StartCustomer();
        //}
    }
    public void buttonEnabbleAracdeMode()
    {
        arcadeMode = true;
        gameObject.SetActive(true);
        StartCustomer();
    }

    public void buttonDisableArcadeMode()
    {
        arcadeMode = false;
        gameObject.SetActive(false);
        StartCustomer();
    }
    void StartCustomer()
    {
        StartCoroutine(MoveCustomerIntoView(orderTimerWait));

        // Disable all models
        for (int i = 0; i < characterModel.Length; i++)
        {
            characterModel[i].SetActive(false);
        }

        // Equal chance character
        int randomIndex = Random.Range(0, characterModel.Length);
        characterModel[randomIndex].SetActive(true);
        Debug.Log("New customer: " + characterModel[randomIndex].name);

        // Equal 50/50 choice
        bool orderRecipe = Random.Range(0, 2) == 0;

        if (orderRecipe && recipeSODatabase.Length > 0 || foodSODatabase == null)
        {
            int recipeIndex = Random.Range(0, recipeSODatabase.Length);
            RecipeSO randomRecipe = recipeSODatabase[recipeIndex];
            currentCustomer.RequestRecipe(randomRecipe);
        }
        else if (foodSODatabase.Length > 0)
        {
            int foodIndex = Random.Range(0, foodSODatabase.Length);
            FoodSO randomFood = foodSODatabase[foodIndex];
            currentCustomer.RequestFood(randomFood);
        }
    }
    private IEnumerator MoveCustomerIntoView(float waitForSeconds)
    {
        Vector3 startPos = new Vector3(-40.6f, 1.6f, 17.9f);
        Vector3 endPos = new Vector3(-37.3199997f, 1.59000003f, 17.8999996f);
        float duration = 2f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            currentCustomer.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        currentCustomer.transform.position = endPos;
        yield return new WaitForSeconds(waitForSeconds);
        currentCustomer.transform.rotation = Quaternion.Euler(0, -90, 0);
    }
    public void FoodCheck(FoodSO servedFood, RecipeSO servedRecipe, GameObject servedObject)
    {
        if (!canServe) return;

        if (!currentCustomer.isRecipeOrder)
        {
            // FOOD CHECK
            Debug.Log("Checking food");
            if (servedFood == currentCustomer.requestedFood)             
            {
                Destroy(servedObject);
                CorrectServe();
            }
            else
            {
                WrongServe();
            }
        }
        else
        {
            // RECIPE CHECK
            if (servedRecipe == currentCustomer.requestedRecipe)
            {
                Destroy(servedObject);
                CorrectServe();
            }
            else
            {
                WrongServe();
            }
        }
    }

    void CorrectServe()
    {
        Debug.Log("Correct food!");
        orderTextBubble.ShowCorrect();
        customerServedCount++;

        if (customerServedCount < maxCustomers)
        {
            CustomerLeave();
        }
        else
        {
            Debug.Log("You WIN! You served ALL the customers");
            // WIN GAME ANIMATION?
        }
    }

    void WrongServe()
    {
        Debug.Log("Wrong food!");
        orderTextBubble.ShowWrong();
    }

    void CustomerLeave()
    {
        if (leaveCoroutine != null)
            return;

        canServe = false;

        currentCustomer.GetComponentInChildren<OrderTextBubble>().HideOrder();
        OrderTextBubble orderBubble = currentCustomer.GetComponentInChildren<OrderTextBubble>();

        // Move the customer out of view
        StartCoroutine(MoveCustomerOutOfView(orderTimerWait));

        leaveCoroutine = StartCoroutine(HandleCustomerLeaving(customerLeaveTime));
    }

    private IEnumerator HandleCustomerLeaving(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);

        leaveCoroutine = null;

        StartCustomer();
        canServe = true;
    }

    public void EndGame()
    {
        Application.Quit();
    }

    private IEnumerator MoveCustomerOutOfView(float waitForSeconds)
    {
        yield return new WaitForSeconds(waitForSeconds);
        currentCustomer.transform.rotation = Quaternion.Euler(0, 0, 0);
        Vector3 startPos = currentCustomer.transform.position; 
        Vector3 endPos = new Vector3(-33, 1.5f, 18f);
        float duration = 2f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            currentCustomer.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        currentCustomer.transform.position = endPos;
    }

}