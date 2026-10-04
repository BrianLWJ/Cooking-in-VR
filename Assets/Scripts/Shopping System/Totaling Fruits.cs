using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TotalingFruits : MonoBehaviour
{
    public List<ShoppingObject> shoppingObjects = new List<ShoppingObject>();
    private HashSet<GameObject> scannedObjects = new HashSet<GameObject>();

    public int totalItems;
    public float totalPrice;

    public BoxCollider boxCollider;
    private void OnCollisionEnter(Collision collision)
    {
        var holder = collision.gameObject.GetComponent<ShoppingObjectInventory>();
        if (holder == null || holder.shoppingObject == null)
            return;

        // Prevent multiple scans of same GameObject
        if (scannedObjects.Contains(collision.gameObject))
            return;

        scannedObjects.Add(collision.gameObject);
        shoppingObjects.Add(holder.shoppingObject);
        CalculateTotalItemsAndPrice();
    }

    public void CalculateTotalItemsAndPrice()
    {
        Dictionary<ShoppingObject, int> itemCounts = new Dictionary<ShoppingObject, int>();

        foreach (var item in shoppingObjects)
        {
            if (!itemCounts.ContainsKey(item))
                itemCounts[item] = 0;

            itemCounts[item]++;
        }

        totalItems = 0;
        totalPrice = 0f;

        foreach (var pair in itemCounts)
        {
            totalItems += pair.Value;
            totalPrice += pair.Key.itemPrice * pair.Value;

            Debug.Log($"Item: {pair.Key.name}, Count: {pair.Value}");
            //Input reciept here
        }
        //Input reciept total here
        Debug.Log($"TOTAL → Items: {totalItems}, Price: {totalPrice}");
    }
}
