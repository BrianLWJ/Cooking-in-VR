using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Plate : MonoBehaviour
{
    public Transform snapPoint;
    public float stackHeightOffset = 0.05f;

    private HashSet<GameObject> currentObjects = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        FoodSOHolder holder = other.GetComponent<FoodSOHolder>();
        if (holder == null) return;

        if (currentObjects.Contains(other.gameObject)) return;

        currentObjects.Add(other.gameObject);

        Snap(other.gameObject);

        // ✅ Delay check to avoid Destroy timing issues
        StartCoroutine(CheckPlateNextFrame());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!currentObjects.Contains(other.gameObject)) return;

        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();

        // Only remove if player is grabbing it
        if (grab != null && grab.isSelected)
        {
            currentObjects.Remove(other.gameObject);
        }
    }

    private void Snap(GameObject obj)
    {
        int index = currentObjects.Count - 1;

        Vector3 newPos = snapPoint.position + new Vector3(0, index * stackHeightOffset, 0);

        obj.transform.position = newPos;
        obj.transform.rotation = snapPoint.rotation;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private IEnumerator CheckPlateNextFrame()
    {
        yield return null;
        CheckPlate();
    }

    private void CheckPlate()
    {
        // ✅ Clean destroyed references from HashSet
        currentObjects.RemoveWhere(obj => obj == null);

        // Copy to list
        List<GameObject> sortedObjects = new List<GameObject>(currentObjects);

        // ✅ Extra safety
        sortedObjects.RemoveAll(obj => obj == null);

        // Sort bottom → top
        sortedObjects.Sort((a, b) =>
            a.transform.position.y.CompareTo(b.transform.position.y));

        List<FoodSO> foods = new List<FoodSO>();

        foreach (var obj in sortedObjects)
        {
            FoodSOHolder holder = obj.GetComponent<FoodSOHolder>();
            if (holder != null)
            {
                foods.Add(holder.foodData);
            }
        }

        Debug.Log("Current objects count: " + currentObjects.Count);

        string log = "Plate contains: ";
        foreach (var f in foods)
        {
            log += f.name + " ";
        }
        Debug.Log(log);

        // Check recipes
        foreach (var recipe in GameManager.Instance.recipeSODatabase)
        {
            if (recipe.IsMatchOrdered(foods.ToArray()))
            {
                Debug.Log("Plated: " + recipe.name);

                GameObject newFood = Instantiate(
                    recipe.combinationResultGameObject,
                    snapPoint.position + new Vector3(0, stackHeightOffset * currentObjects.Count, 0),
                    snapPoint.rotation
                );

                FoodRecipeHolder recipeHolder = newFood.GetComponent<FoodRecipeHolder>();
                if (recipeHolder != null)
                {
                    recipeHolder.recipeFoodData = recipe;
                }

                ClearPlate();
                return;
            }
        }

        Debug.Log("No matching recipe");
    }

    private void ClearPlate()
    {
        foreach (var obj in currentObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        currentObjects.Clear();
    }
}