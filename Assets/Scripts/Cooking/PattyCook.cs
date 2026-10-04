using UnityEngine;

public class PattyCook : MonoBehaviour
{
    public enum CookState { Raw, Cooking, Cooked, Burnt, BurstIntoFlame }
    public CookState currentState = CookState.Raw;

    public float cookTime = 5f;
    public float burnTime = 10f;
    public float burstIntoFlameTime = 15f;

    private float timer = 0f;
    private bool isOnHeat = false;

   AudioManager audioManager;

    [Header("Mesh States")]
    public Mesh rawMesh;
    public Mesh cookingMesh; // Add Burning effect
    public Mesh cookedMesh;
    public Mesh burntMesh;

    public bool isOnPan = false;
    public FoodSO[] burgerStateList;    // [0]=uncooked, [1]=cooked, [2]=burnt
    public FoodSOHolder foodSOHolder;
    void Start()
    {
        ApplyState(CookState.Raw);

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (!isOnHeat) return;
        if (!isOnPan) return;
        timer += Time.deltaTime;

        if (timer >= burstIntoFlameTime)
        {
            SetState(CookState.BurstIntoFlame);
        }
        if (timer >= burnTime)
        {
            SetState(CookState.Burnt);
        }
        else if (timer >= cookTime)
        {
            SetState(CookState.Cooked);
        }
        else if (currentState == CookState.Raw)
        {
            SetState(CookState.Cooking);
        }
    }

    private void SetState(CookState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        ApplyState(newState);
        UpdateFoodSO();
    }

    private void ApplyState(CookState state)
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf == null) return;

        switch (state)
        {
            case CookState.Raw:
                mf.mesh = rawMesh;
                break;
            case CookState.Cooking:
                mf.mesh = cookingMesh;
                break;
            case CookState.Cooked:
                mf.mesh = cookedMesh;
                audioManager.PlayDingSFX();
                break;
            case CookState.Burnt:
                mf.mesh = burntMesh;
                audioManager.PlayDingSFX();
                break;
            case CookState.BurstIntoFlame:
		        FireEvent.TriggerFire(transform.position);
		        Destroy(gameObject);
                break;
        }
    }

    private void UpdateFoodSO()
    {
        if (foodSOHolder == null || burgerStateList.Length < 3) return;

        switch (currentState)
        {
            case CookState.Raw:
            case CookState.Cooking:
                foodSOHolder.foodData = burgerStateList[0]; // uncooked
                break;

            case CookState.Cooked:
                foodSOHolder.foodData = burgerStateList[1]; // cooked
                break;

            case CookState.Burnt:
                foodSOHolder.foodData = burgerStateList[2]; // burnt
                break;
        }
    }

    public void SetHeat(bool value)
    {
        isOnHeat = value;
    }

    public void ResetCook()
    {
        timer = 0f;
        SetState(CookState.Raw);
    }
}
