using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager instance;

    public AudioClip audioClipShoot;
    public AudioClip audioClipHit;

    public AudioSource effectsAudioSource;

    public AudioSource backgroundSource;

    private void Awake()
    {
        if (MusicManager.instance == null)
        {
            MusicManager.instance = this;
        }
        else if (MusicManager.instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayShoot()
    {
        PlayAudioClip(audioClipShoot);
    }

    public void PlayHit()
    {
        PlayAudioClip(audioClipHit);
    }

    private void PlayAudioClip(AudioClip audioClip)
    {
        effectsAudioSource.clip = audioClip;
        effectsAudioSource.Play();
    }

    private void OnDestroy()
    {
        if (MusicManager.instance == this)
        {
            MusicManager.instance = null;
        }
    }
}
