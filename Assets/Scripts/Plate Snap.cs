using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlateSnap : MonoBehaviour
{
    public Transform snapPoint;
    public bool isTopOccupied = false;
    private HashSet<GameObject> activeObjects = new HashSet<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        if (isTopOccupied) return;
        FoodSOHolder holder = collision.gameObject.GetComponent<FoodSOHolder>();
        FoodRecipeHolder recipeHolder = collision.gameObject.GetComponent<FoodRecipeHolder>();
        if (holder == null && recipeHolder == null) return;

        // Prevent duplicate add
        if (activeObjects.Contains(collision.gameObject)) return;
        //activeObjects.Add(gameObject);
        activeObjects.Add(collision.gameObject);

        FoodSO newFood = holder.foodData;

        Debug.Log("Added: " + newFood.name);

        // Snap
        collision.transform.position = snapPoint.position;
        collision.transform.rotation = snapPoint.rotation;

        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        isTopOccupied = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!activeObjects.Contains(collision.gameObject)) return;

        XRGrabInteractable grab = collision.gameObject.GetComponent<XRGrabInteractable>();

        // Only allow exit if the object is being grabbed
        if (grab != null && grab.isSelected)
        {
            activeObjects.Remove(collision.gameObject);
            isTopOccupied = false;
            Debug.Log("Removed: " + collision.gameObject.name);
        }
    }
}
