using UnityEngine;

public class CrateSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform spawnPoint;

    private bool playerNearby = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            playerNearby = true;
            Debug.Log("Hand Detected");
            SpawnItem();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            playerNearby = false;
            Debug.Log("hand Removed");
        }
    }

    public void SpawnItem()
    {
        Instantiate(itemPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Spawn an Item");
    }
}
