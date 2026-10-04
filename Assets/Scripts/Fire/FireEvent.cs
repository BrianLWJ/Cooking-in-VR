using UnityEngine;

public class FireEvent : MonoBehaviour {
	static FireEvent instance;

	public GameObject firePrefab;
	public int maxFires = 20;
	public AudioClip fireSfx;
	public AudioManager audioManager;

	[Header("Fire Properties")]
	public float lifeTime = 20.0f;
	public float minimunLifeTimeToSpread = 5.0f;
	public float spreadChancePerFrame = 0.001f;
	public float timeToExtinguish = 5.0f;
	public Vector3 spread = new Vector3(1, 0.5f, 1);

	[Header("Fire Spawn Bounds")]
	public Vector3 minimunBound = new Vector3(-47, 0, 20);
	public Vector3 maximunBound = new Vector3(-27, 4, 36);

	public static float LifeTime { get => instance.lifeTime; }
	public static float MinimunLifeTimeToSpread { get => instance.minimunLifeTimeToSpread; }
	public static float SpreadChancePerFrame { get => instance.spreadChancePerFrame; }
	public static float TimeToExtinguish { get => instance.timeToExtinguish; }
	public static Vector3 Spread { get => instance.spread; }
	public static Vector3 MinimunBound { get => instance.minimunBound; }
	public static Vector3 MaximunBound { get => instance.maximunBound; }
	public static AudioManager Audio { get => instance.audioManager; }

	int fireCount = 0;

	void Awake() {
		if(instance == null) instance = this;
	}

	void Update() {
		if(fireCount > 0) Audio.PlayLoopingSFX(fireSfx, 0);
		else Audio.StopLoopingSFX(0);
	}

	public static void TriggerFire(Vector3 position) {
		if(instance == null) return;
		if(instance.fireCount >= instance.maxFires) return;
		instance.fireCount++;
		Instantiate(instance.firePrefab, position, Quaternion.identity);
	}

	public static void DespawnFire(GameObject fireGO) {
		Destroy(fireGO);
		instance.fireCount--;
	}
}
