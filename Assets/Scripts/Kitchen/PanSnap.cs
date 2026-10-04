using UnityEngine;

public class PanSnap : MonoBehaviour
{
    public Transform snapPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pan"))
        {
            other.transform.position = snapPoint.position;
            other.transform.rotation = snapPoint.rotation;

            PattySnap pattySnap = other.GetComponentInChildren<PattySnap>();
            pattySnap.isOnStove = true;

            // Update any patty already on the pan
            PattyCook pattyOnPan = pattySnap.snapPoint.GetComponentInChildren<PattyCook>();
            if (pattyOnPan != null)
                pattyOnPan.SetHeat(true);

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            Debug.Log("Pan snapped onto the stove");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pan"))
        {
            PattySnap pattySnap = other.GetComponentInChildren<PattySnap>();
            pattySnap.isOnStove = false;
            // Update any patty already on the pan
            PattyCook pattyOnPan = pattySnap.snapPoint.GetComponentInChildren<PattyCook>();
            if (pattyOnPan != null)
                pattyOnPan.SetHeat(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Pan")) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

}
