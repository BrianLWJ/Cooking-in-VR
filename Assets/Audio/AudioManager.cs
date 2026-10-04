using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [Header(" Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource[] loopingSFXSource;

    [Header(" Audio BGM")]
    public AudioClip menuBGM;
    public AudioClip kitchenBGM;

    [Header(" Audio Clip")]
    public AudioClip knife;
    public AudioClip walk;
    public AudioClip fireBurn;
    public AudioClip fireExtingusher;
    public AudioClip Ding;
    public AudioClip buttonClick;

    private void Start()
    {
        PlayMusic(menuBGM);
    }
    public void PlayMusic(AudioClip newClip)
    {
        if (musicSource.clip == newClip) return;

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    { 
        SFXSource.PlayOneShot(clip);
    }

    public void PlayLoopingSFX(AudioClip clip, int channel)
    { 
        if (loopingSFXSource[channel].clip == clip) return;

        loopingSFXSource[channel].Stop();
        loopingSFXSource[channel].clip = clip;
        loopingSFXSource[channel].Play();
    }

    public void StopLoopingSFX(int channel) {
	loopingSFXSource[channel].Stop();
	loopingSFXSource[channel].clip = null;
    }

    public void PlayButtonSFX()
    {
        //BUTTON Clip used is soft
        SFXSource.PlayOneShot(buttonClick, 5.0f);
    }
    public void PlayDingSFX()
    {
        //Clip used is soft
        SFXSource.PlayOneShot(Ding, 5.0f);
    }
}
