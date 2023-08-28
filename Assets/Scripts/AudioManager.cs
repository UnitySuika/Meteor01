using SuikaDev;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    AudioSource audioSource;

    [Serializable]
    public class IDAndClip
    {
        public string ID;
        public AudioClip Clip;
    }

    [SerializeField] IDAndClip[] clips;

    protected override void OverrideAwake()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySE(AudioClip clip, float volume = 0.5f, bool isOverride = false)
    {
        audioSource.volume = volume;
        if (isOverride)
        {
            audioSource.Stop();
        }
        audioSource.PlayOneShot(clip);
    }

    public void PlaySE(string clipID, float volume = 0.5f, bool isOverride = false)
    {
        audioSource.volume = volume;
        if (isOverride)
        {
            audioSource.Stop();
        }
        AudioClip clip = Array.Find(clips, x => x.ID == clipID).Clip;
        audioSource.PlayOneShot(clip);
    }
}
