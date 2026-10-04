using UnityEngine;

public class PattySnap : MonoBehaviour
{
    public Transform snapPoint;
    public PattyCook pattyCook;
    public bool isOnStove = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Patty"))
        {
            other.transform.SetParent(snapPoint);
            other.transform.position = snapPoint.position;
            other.transform.rotation = snapPoint.rotation;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            PattyCook cook = other.GetComponent<PattyCook>();
            if (cook != null)
            {
                cook.isOnPan = true;
                cook.SetHeat(isOnStove);  // This was missing
            }

            Debug.Log("Patty is on da pan");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Patty"))
        {
            other.transform.SetParent(null);

            PattyCook cook = other.GetComponent<PattyCook>();
            if (cook != null)
            {
                cook.isOnPan = false;
                cook.SetHeat(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Patty")) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

}
