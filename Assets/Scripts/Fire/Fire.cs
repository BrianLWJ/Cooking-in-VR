using UnityEngine;

public class Fire : MonoBehaviour
{
	float timer = 0.0f;
	float extinguishTimer = 0.0f;

	void Update()
	{
		timer += Time.deltaTime;

		if(timer >= FireEvent.MinimunLifeTimeToSpread && Random.Range(0.0f, 1.0f) < FireEvent.SpreadChancePerFrame) {
			Vector3 newPos = transform.position + new Vector3(
				Random.Range(-FireEvent.Spread.x, FireEvent.Spread.x),
				Random.Range(-FireEvent.Spread.y, FireEvent.Spread.y),
				Random.Range(-FireEvent.Spread.z, FireEvent.Spread.z)
			);
			newPos.x = Mathf.Clamp(newPos.x, FireEvent.MinimunBound.x, FireEvent.MaximunBound.x);
			newPos.y = Mathf.Clamp(newPos.y, FireEvent.MinimunBound.y, FireEvent.MaximunBound.y);
			newPos.z = Mathf.Clamp(newPos.z, FireEvent.MinimunBound.z, FireEvent.MaximunBound.z);
			FireEvent.TriggerFire(newPos);
		}

		if(extinguishTimer >= FireEvent.TimeToExtinguish || timer >= FireEvent.LifeTime) FireEvent.DespawnFire(gameObject);
	}

	void OnTriggerStay(Collider other)
	{
		if(other.CompareTag("Fire Extinguisher") == true) {
			extinguishTimer += Time.fixedDeltaTime;
		}
	}
}
