using UnityEngine;

public class MixingBowl : MonoBehaviour
{
    public float mixProgress = 0f;
    public float mixRequired = 5f;

    public GameObject mixedResult;

    private Rigidbody spoonRb;

    public int requiredIngredients = 2;
    private int currentIngredients = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Add ingredient
        if (other.CompareTag("Ingredient"))
        {
            currentIngredients++;
            Destroy(other.gameObject);
            Debug.Log("Ingredient added: " + currentIngredients);
        }

        // Detect spoon
        if (other.CompareTag("Spoon"))
        {
            spoonRb = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Spoon") && spoonRb != null)
        {
            if (currentIngredients >= requiredIngredients)
            {
                DetectStir();
            }
        }
    }

    void DetectStir()
    {
        Vector3 velocity = spoonRb.linearVelocity;

        float horizontalSpeed = new Vector2(velocity.x, velocity.z).magnitude;

        if (horizontalSpeed > 0.3f)
        {
            mixProgress += Time.deltaTime;
        }

        if (mixProgress >= mixRequired)
        {
            CompleteMix();
        }
    }

    void CompleteMix()
    {
        Instantiate(mixedResult, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
