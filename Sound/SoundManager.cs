using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioSource[] audioSources;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayOneShot(AudioClip cilp)
    {
        var player = FindAudioSource();
        player.PlayOneShot(cilp);
    }
    AudioSource FindAudioSource()
    {
        AudioSource t = null;
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying)
                continue;
            t = audioSources[i];
            break;
        }
        return t;
    }
}