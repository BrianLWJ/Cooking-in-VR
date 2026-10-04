using System.Collections.Generic;
using UnityEngine;

public class ShoppingCart : MonoBehaviour
{
    public List<ShoppingObject> shoppingObjects = new List<ShoppingObject>();

    public float maxTotalWeightCanCarry;
    [SerializeField]
    private float totalWeightCarrying;

    public float cartMaxSpeed;
    public float cartMinSpeed;
    public float currentSpeed;
    public void CalculateTotalWeight()
    {
        foreach (var item in shoppingObjects)
        {
            if (item != null)
            {
                totalWeightCarrying += item.itemWeight;
            }
        }
        Debug.Log($"Total Weight Carrying: {totalWeightCarrying}");
    }

    public void GetCurrentSpeed()
    {
        CalculateTotalWeight();
        float weightRatio = totalWeightCarrying / maxTotalWeightCanCarry;
        currentSpeed = Mathf.Lerp(cartMinSpeed, cartMaxSpeed, 1 - weightRatio);
    }
}
