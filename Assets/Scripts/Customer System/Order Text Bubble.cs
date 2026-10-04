using TMPro;
using UnityEngine;
using System.Collections;

public class OrderTextBubble : MonoBehaviour
{
    [SerializeField] private GameObject textBubble;
    [SerializeField] private Transform modelSpawnPoint;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    public GameManager gameManager;
    private GameObject currentModel;
    public GameObject correctOrderGameObj;
    public GameObject wrongOrderGameObj;

    public void Awake()
    {
        correctOrderGameObj.SetActive(false);
        wrongOrderGameObj.SetActive(false);
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // FOOD (prefab)
    public void ShowFoodOrder(string text, GameObject foodPrefab)
    {
        ClearModel();

        textMeshPro.text = text;
        currentModel = Instantiate(foodPrefab, modelSpawnPoint);
        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.identity;
        currentModel.transform.localScale = Vector3.one * 0.5f;

        CleanObject(currentModel);

        textBubble.SetActive(true);
    }

    // RECIPE
    public void ShowRecipeOrder(string text, GameObject recipePrefab)
    {
        ClearModel();

        textMeshPro.text = text;
        currentModel = Instantiate(recipePrefab, modelSpawnPoint);
        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.identity;
        currentModel.transform.localScale = Vector3.one * 1.5f;
        CleanObject(currentModel);

        textBubble.SetActive(true);
    }

    public void HideOrder()
    {
        textBubble.SetActive(false);
        ClearModel();
    }

    private void ClearModel()
    {
        if (currentModel != null)
        {
            textMeshPro.SetText("");
            Destroy(currentModel);
        }
    }

    // REMOVE ALL INTERACTION COMPONENTS
    private void CleanObject(GameObject obj)
    {
        Collider col = obj.GetComponent<Collider>();
        if (col != null) Destroy(col);

        var grab = obj.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grab != null) Destroy(grab);

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null) Destroy(rb);

        var foodHolder = obj.GetComponent<FoodSOHolder>();
        if (foodHolder != null) Destroy(foodHolder);

        var recipeHolder = obj.GetComponent<FoodRecipeHolder>();
        if (recipeHolder != null) Destroy(recipeHolder);
    }

    public void ShowCorrect()
    {
        StartCoroutine(ShowCorrectTimer(gameManager.correctOrderTimerWait));
    }

    public IEnumerator ShowCorrectTimer(float waitSeconds)
    {
        correctOrderGameObj.SetActive(true);
        yield return new WaitForSeconds(waitSeconds);
        correctOrderGameObj.SetActive(false);
    }
    public void ShowWrong()
    {
        StartCoroutine(ShowWrongTimer(gameManager.wrongOrderTimerWait));
    }
    public IEnumerator ShowWrongTimer(float waitSeconds)
    {
        Debug.Log("Showing wrong order feedback");
        wrongOrderGameObj.SetActive(true);
        yield return new WaitForSeconds(waitSeconds);
        Debug.Log("Hiding wrong order feedback");
        wrongOrderGameObj.SetActive(false);
    }
}