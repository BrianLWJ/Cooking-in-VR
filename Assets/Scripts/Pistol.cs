using UnityEngine;

public class Pistol : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firepoint;
    public float bulletSpeed = 20f;
    public float bulletLifetime = 5f;

    public AudioClip clip;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>(); 
    }

    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        source.PlayOneShot(clip);

        if (rb != null)
        {
            rb.linearVelocity = firepoint.forward * bulletSpeed;
        }

        Destroy(bullet, bulletLifetime);
    }

}
