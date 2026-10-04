using UnityEngine;

public class SnapInPlace : MonoBehaviour
{
    public Transform snapPoint;

    private void OnCollisionEnter(Collision collision)
    {
        var snapObject = collision.gameObject;

        if (snapObject.GetComponent<FoodSOHolder>() == null)
            return;

        // Snap position & rotation
        snapObject.transform.position = snapPoint.position;
        snapObject.transform.rotation = snapPoint.rotation;

        // Freeze physics
        Rigidbody rb = snapObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}