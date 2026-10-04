using UnityEngine;

public class Choppable : MonoBehaviour
{
    public int chopRequired = 3;
    private int currentChops = 0;

    public GameObject choppedPrefab;

    private float lastChopTime = 0f;
    public float chopCooldown = 0.3f;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Knife")) return;

        Rigidbody knifeRb = collision.gameObject.GetComponent<Rigidbody>();
        if (knifeRb == null) return;


        if (collision.gameObject.CompareTag("Knife"))
        {
            Chop();
            audioManager.PlaySFX(audioManager.knife);
            Debug.Log("You are being chopped");
        }

        // Detect downward motion
        if (knifeRb.linearVelocity.y < -0.5f)
        {
            // Prevent spam
            if (Time.time - lastChopTime > chopCooldown)
            {
                Chop();
                audioManager.PlaySFX(audioManager.knife);
                Debug.Log("You are being chopped");
                lastChopTime = Time.time;
            }
        }

    }

    

   
void Chop()
    {
        currentChops++;
        Debug.Log("Chop: " + currentChops);

        if (currentChops >= chopRequired)
        {
            CompleteChop();
        }
    }

    void CompleteChop()
    {
        Instantiate(choppedPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
