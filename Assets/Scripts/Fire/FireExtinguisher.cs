using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
	public GameObject foamGO;
	public AudioClip sfx;
	public AudioManager audioManager;

	void Start()
	{

	}

	public void TurnOn() {
		foamGO.SetActive(true);
		audioManager.PlayLoopingSFX(sfx, 1);
	}

	public void TurnOff() {
		foamGO.SetActive(false);	
		audioManager.StopLoopingSFX(1);
	}
}
